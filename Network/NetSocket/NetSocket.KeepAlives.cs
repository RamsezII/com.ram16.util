using UnityEngine;

namespace _UTIL_
{
    public partial class NetSocket
    {
        [SerializeField] float next_keepAlives;
        public bool NeedsKeepAlives => Time.unscaledTime > next_keepAlives;

        //----------------------------------------------------------------------------------------------------------

        public void KeepAlives()
        {
            next_keepAlives = Time.unscaledTime + 1;
            lock (connections)
            {
                double time = Util.TotalMilliseconds;
                foreach (var connection in connections.Values)
                    if (connection.keepAlive && time - connection.last_send > 1000)
                        connection.SendKeepAlive();
            }
        }
    }
}