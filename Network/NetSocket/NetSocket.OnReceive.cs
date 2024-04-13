using UnityEngine;

namespace _UTIL_
{
    public partial class NetSocket
    {
        public QOSf recQos_u;
        static readonly byte[] ackBuffer = new byte[(int)QOSi._last_];

        //----------------------------------------------------------------------------------------------------------

        void OnReceive()
        {
            recReader_u.BaseStream.Position = (byte)QOSi._last_;
            recQos_u = (QOSf)recReader_u.buffer[(byte)QOSi.qos];

            byte
                paquet_id = recReader_u.buffer[(byte)QOSi.id],
                attempt = recReader_u.buffer[(byte)QOSi.attempt];

            // send ACK
            if (recQos_u.HasFlag(QOSf.Reliable))
                lock (ackBuffer)
                {
                    PrefixeQOS(QOSf.Ack, ackBuffer);
                    ackBuffer[(int)QOSi.id] = paquet_id;
                    ackBuffer[(int)QOSi.attempt] = attempt;
                    SendTo(rec_netEnd_u, ackBuffer, 0, (int)QOSi._last_);
                }

            // redundancy
            if (recQos_u.HasFlag(QOSf.Ack))
            {
                if (!recConn_u.TryReceiveAck(paquet_id, attempt))
                {
                    Debug.Log($"skipAck: {paquet_id}.{attempt} {{ {rec_netEnd_u} }}".ToSubLog());
                    recReader_u.Skip();
                }
            }
            else if (!recQos_u.HasFlag(QOSf.Eve) && recConn_u.recID_queue.RedundancyCheck(paquet_id))
            {
                recReader_u.Skip();
                Debug.Log($"{this} skipMsg {rec_netEnd_u}".ToSubLog());
            }

            // fragments
            if (recQos_u.HasFlag(QOSf.Fragment))
            {
                if (recQos_u.HasFlag(QOSf.Start))
                    recConn_u.bigWriter.BaseStream.Position = 0;

                int start = (int)recReader_u.BaseStream.Position;
                int length = recReader_u.end - start;

                recConn_u.bigWriter.Write(recReader_u.buffer, start, length);
                recReader_u.Skip();

                if (recQos_u.HasFlag(QOSf.End))
                    Read(new(recConn_u.bigWriter.VampireCopy()), false);
            }
            else
                Read(recReader_u, true);

            void Read(in StdReader reader, in bool copy)
            {
                // direct reads
                while (reader.HasNext)
                    if (!iuser.OnDirectRead(reader.ReadByte(), reader))
                    {
                        --reader.BaseStream.Position;
                        break;
                    }
                // queued reads
                if (reader.HasNext)
                    lock (recReader_queued)
                        if (copy)
                            recReader_queued.Enqueue((new(reader.VampireCopy()), recConn_u));
                        else
                            recReader_queued.Enqueue((reader, recConn_u));
            }
        }
    }
}