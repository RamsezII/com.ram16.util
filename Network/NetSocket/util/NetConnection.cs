using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

namespace _UTIL_
{
    [Serializable]
    public class NetConnection
    {
        static readonly byte[] holepunchBuffer = new byte[(int)QOSi._last_];

        readonly NetSocket socket;
        public readonly IPEndPoint netEnd;
        public IPEndPoint localEnd, publicEnd;
        public override string ToString() => $"netPoint[{netEnd}]";

        public bool disposed;
        public byte send_id;
        public double last_send = -10, last_rec;

        bool holepunch;
        public bool keepAlive;

        public readonly RedundancyQueue<byte> recID_queue = new(50);

        public float rawPing, lerpedPing;
        public readonly List<float> pings = new();

        public Action<bool> onHolePunch;
        readonly Queue<ReliableMessage> messages = new();
        public readonly StdWriter bigWriter = new();

        bool firstack;

        //----------------------------------------------------------------------------------------------------------

        public NetConnection(in NetSocket socket, in IPEndPoint netEnd)
        {
            firstack = true;
            last_send = last_rec = Util.TotalMilliseconds;
            this.socket = socket;
            this.netEnd = netEnd;
            Debug.Log($"{socket} add netpoint {netEnd}".ToSubLog());
        }

        //----------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            if (!disposed)
            {
                messages.Clear();
                keepAlive = false;
                disposed = true;
                Debug.Log($"{socket} disposed netpoint {netEnd}".ToSubLog());

                if (socket != null)
                    lock (socket.connections)
                        socket.connections.Remove(netEnd);
            }
        }

        //----------------------------------------------------------------------------------------------------------

        public void Write(in BinaryWriter writer)
        {
            writer.Write(localEnd);
            writer.Write(publicEnd);
        }

        public ReliableMessage AddMessage(in byte[] buffer) => AddMessage(buffer, 0, buffer.Length);
        public ReliableMessage AddMessage(in byte[] buffer, in int offset, in int size)
        {
            ReliableMessage message = new(buffer, offset, size);
            AddMessage(message);
            return message;
        }

        public void AddMessage(in ReliableMessage message)
        {
            lock (this)
            {
                message.id = ++send_id;
                lock (messages)
                    messages.Enqueue(message);
            }
        }

        public ReliableMessage AddMessage(in BinaryWriter writer) => AddMessage((StdWriter)writer);
        public ReliableMessage AddMessage(in StdWriter writer)
        {
            if (writer.BaseStream.Position < NetSocket.udp_bufferSize)
                if (writer.buffer == null)
                    return AddMessage(writer.Buffer, 0, (int)writer.BaseStream.Position);
                else
                    return AddMessage(writer.VampireCopy());
            else
                lock (messages)
                {
                    byte[] buffer = writer.Buffer;

                    // start
                    buffer[(int)QOSi._last_] |= (byte)QOSf.StartFragment;
                    var msg = AddMessage(buffer[0..NetSocket.udp_bufferSize]);

                    // remaining fragments
                    int pos = NetSocket.udp_bufferSize;
                    while (true)
                    {
                        int start = pos - (int)QOSi._last_;
                        pos = start + NetSocket.udp_bufferSize;
                        bool end = false;

                        if (pos >= writer.BaseStream.Position)
                        {
                            end = true;
                            pos = (int)writer.BaseStream.Position;
                        }

                        byte[] clone = buffer[start..pos];

                        NetSocket.PrefixeQOS(QOSf.Fragment, clone);
                        if (end)
                            clone[(int)QOSi.qos] |= (byte)QOSf.End;

                        msg = AddMessage(clone);

                        if (end)
                            break;
                    }

                    return msg;
                }
        }

        public void Send(in double time)
        {
            lock (messages)
                if (messages.Count != 0)
                {
                    last_send = time;
                    var message = messages.Peek();

                    lock (message.buffer)
                    {
                        if (!holepunch || pings.Count > 0 && pings.Count % 5 == 0)
                            Debug.Log($"msg[{message.id}.{pings.Count}] to {netEnd} (qos:{(QOSf)message.buffer[message.offset + (int)QOSi.qos]})".ToSubLog());

                        message.buffer[message.offset + (int)QOSi.id] = message.id;
                        lock (pings)
                        {
                            message.buffer[message.offset + (int)QOSi.attempt] = (byte)pings.Count;
                            pings.Add((float)time);
                        }
                        socket.SendTo(netEnd, message.buffer, message.offset, message.size);
                    }
                }
        }

        public bool TryReceiveAck(in byte id, in byte attempt)
        {
            if (!holepunch)
            {
                Debug.Log($"holepunch established with {netEnd}".ToSubLog());
                rawPing = lerpedPing = 0;
            }

            holepunch = true;

            ReliableMessage message = null;
            bool yes = false;

            lock (messages)
            {
                if (yes = messages.TryPeek(out message) && message.id == id)
                    messages.Dequeue();
            }

            if (yes)
            {
                onHolePunch?.Invoke(true);
                onHolePunch = null;
                message.onAck?.Invoke(socket.recReader_u);

                lock (pings)
                {
                    float ping = (float)last_rec - pings[attempt];

                    if (firstack)
                    {
                        firstack = false;
                        Debug.Log($"{{ {netEnd} }} ping: {(int)ping}ms".ToSubLog());
                    }

                    rawPing = pings[attempt] = ping;
                    message.onSuccessPing?.Invoke(attempt, rawPing);
                    pings.Clear();
                    lerpedPing = Mathf.Lerp(lerpedPing, rawPing, .65f);
                }

                Send(last_rec);
                return true;
            }
            return false;
        }

        public void SendKeepAlive()
        {
            lock (messages)
                if (!holepunch)
                    Debug.Log($"tentative de holepunch {netEnd}".ToSubLog());

            lock (holepunchBuffer)
            {
                NetSocket.PrefixeQOS(QOSf.Reliable, holepunchBuffer);
                AddMessage(holepunchBuffer);
            }
        }

        public void DeclareTimeout()
        {
            lock (messages)
            {
                if (holepunch)
                    Debug.LogWarning("holepunch interruption: " + netEnd);

                holepunch = keepAlive = false;
                onHolePunch?.Invoke(false);
                onHolePunch = null;

                while (messages.TryDequeue(out var message))
                    message.onFailure?.Invoke();
            }
            Dispose();
        }
    }
}