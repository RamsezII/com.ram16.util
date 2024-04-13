using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace _UTIL_
{
    public partial class NetSocket
    {
        public IPEndPoint rec_netEnd_u;
        public NetConnection recConn_u;
        [SerializeField] bool _skipFirstSocketException = true;

        //----------------------------------------------------------------------------------------------------------

        void ReceiveFrom(IAsyncResult aResult)
        {
            lock (recReader_u)
            {
                rec_netEnd_u = null;
                try
                {
                    EndPoint remoteEnd = receiveEnd;
                    recReader_u.BaseStream.Position = 0;
                    recReader_u.end = (ushort)EndReceiveFrom(aResult, ref remoteEnd);

                    rec_netEnd_u = (IPEndPoint)remoteEnd;
                    recConn_u = ToConnection(rec_netEnd_u);
                    recConn_u.last_rec = Util.TotalMilliseconds;

                    ++receive_count;
                    receive_size += recReader_u.end;

                    if (recReader_u.end >= (byte)QOSi._last_)
                        OnReceive();
                    else
                        Debug.LogWarning($"{recReader_u.end} bytes from {remoteEnd}");
                }
                catch (SocketException e)
                {
                    if (_skipFirstSocketException)
                        _skipFirstSocketException = false;
                    else
                        Debug.LogWarning(e);
                }
                catch (ObjectDisposedException e) { Debug.LogWarning(e); }
                catch (Exception e) { Debug.LogException(e); }
            }
            BeginReceive();
        }
    }
}