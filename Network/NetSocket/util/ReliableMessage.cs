using System;
using System.IO;

namespace _UTIL_
{
    [Serializable]
    public class ReliableMessage
    {
        public byte id;
        public readonly byte[] buffer;
        public int offset, size;

        public Action<StdReader> onAck;
        public Action<byte, float> onSuccessPing;
        public Action onFailure;

        //----------------------------------------------------------------------------------------------------------

        public ReliableMessage(in BinaryWriter writer)
        {
            buffer = writer.VampireCopy();
            size = buffer.Length;
        }

        public ReliableMessage(in byte[] buffer) : this(buffer, 0, buffer.Length)
        {
        }

        public ReliableMessage(in byte[] buffer, in int offset, in int size)
        {
            this.buffer = buffer;
            this.offset = offset;
            this.size = size == 0 ? buffer.Length - offset : size;
        }
    }
}