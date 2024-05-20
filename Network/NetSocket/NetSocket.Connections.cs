using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

namespace _UTIL_
{
    public partial class NetSocket
    {
        public readonly Dictionary<IPEndPoint, NetConnection> connections = new();

        //----------------------------------------------------------------------------------------------------------

        public NetConnection ToConnection(in IPEndPoint netEnd)
        {
            lock (connections)
            {
                if (!connections.TryGetValue(netEnd, out NetConnection connection))
                    connections[netEnd] = connection = new(this, netEnd);
                return connection;
            }
        }

        public NetConnection ToConnection(in BinaryReader reader, in bool keepAlive)
        {
            IPEndPoint 
                localEnd = reader.ReadIpEnd(),
                publicEnd = reader.ReadIpEnd(),
                localizedEnd;

            if (selfConn.publicEnd == null || publicEnd.Address.Equals(selfConn.publicEnd.Address))
                if (localEnd.Address.Equals(selfConn.localEnd.Address))
                    localizedEnd = new(IPAddress.Loopback, localEnd.Port);
                else
                    localizedEnd = localEnd;
            else
                localizedEnd = publicEnd;

            NetConnection conn = ToConnection(localizedEnd);

            conn.localEnd = localEnd;
            conn.publicEnd = publicEnd;
            conn.keepAlive = keepAlive;

            Debug.Log($"{this} read netPoint {localEnd} {publicEnd} -> {localizedEnd}".ToSubLog());
            return conn;
        }
    }
}