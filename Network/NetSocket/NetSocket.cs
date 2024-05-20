using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace _UTIL_
{
    public partial class NetSocket : Socket
    {
        public interface IUser
        {
            bool OnDirectRead(in byte arg0, in StdReader reader);
        }

        public const ushort udp_bufferSize = 3000;

        readonly IUser iuser;
        public readonly StdReader recReader_u = new(udp_bufferSize);
        public readonly Queue<(StdReader, NetConnection)> recReader_queued = new();

        readonly StdWriter stdWriter_u = new(udp_bufferSize);

        EndPoint receiveEnd;
        public NetConnection selfConn;

        public uint 
            send_count, receive_count, skip_count,
            send_size, receive_size;

        public override string ToString() => $"socket[{(selfConn == null ? string.Empty : selfConn.localEnd.Port.ToString())}]";

        public static bool logPaquets;

        //----------------------------------------------------------------------------------------------------------

        public NetSocket(in IUser iuser) : base(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
        {
            this.iuser = iuser;
            ExclusiveAddressUse = false;
        }

        public void Start(ushort portbind = 0)
        {
            if (portbind == 0)
            {
                try
                {
                    SendTo(new byte[] { }, 0, 0, default, Util_net.end_init);
                }
                catch (SocketException e) { Debug.LogError(e); }
                catch (Exception e) { Debug.LogError(e); }
            }
            else
                Bind(new IPEndPoint(IPAddress.Any, portbind));

            receiveEnd = LocalEndPoint;
            ushort port = (ushort)((IPEndPoint)LocalEndPoint).Port;

            selfConn = ToConnection(new IPEndPoint(IPAddress.Loopback, port));
            selfConn.localEnd = new(Util_net.GetLocalIP(), port);

            BeginReceive();
        }

        void BeginReceive()
        {
            try { BeginReceiveFrom(recReader_u.buffer, 0, udp_bufferSize, SocketFlags.None, ref receiveEnd, ReceiveFrom, null); }
            catch (Exception e) { Debug.LogWarning(e.Message); }
        }
    }
}