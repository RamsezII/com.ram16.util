using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace _UTIL_
{
    public partial class NetSocket
    {
        public void Sends()
        {
            double time = Util.TotalMilliseconds;
            lock (connections)
                foreach (var conn in connections.Values)
                    lock (conn.pings)
                    {
                        int sendRate, attempts = conn.pings.Count;

                        if (attempts >= 7)
                            sendRate = 1000;
                        else if (attempts >= 5)
                            sendRate = 500;
                        else if (attempts >= 2)
                            sendRate = 300;
                        else
                            sendRate = 100;

                        if (time - conn.last_send > sendRate)
                            conn.Send(time);
                    }
        }

        public bool SendTo(in IPEndPoint targetEnd, in byte[] buffer, in int offset, in int size)
        {
            try
            {
                SendTo(buffer, offset, size, SocketFlags.None, targetEnd);
                ++send_count;
                send_size += (ushort)(size - offset);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"{receiveEnd} could not send to {targetEnd}, exception:\n{e}");
                return false;
            }
        }
    }
}