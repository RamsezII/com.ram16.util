using System.IO;
using System.Text;

namespace _UTIL_
{
    public class StdWriter : BinaryWriter
    {
        public readonly byte[] buffer;
        public byte[] Buffer => buffer ?? this.GetBuffer();
        public ushort Remaining() => (ushort)(buffer.Length - BaseStream.Position);

        //----------------------------------------------------------------------------------------------------------

        public StdWriter() : base(new MemoryStream(), Encoding.UTF8, false)
        {
        }

        public StdWriter(in ushort size) : this(new byte[size])
        {
        }

        public StdWriter(in byte[] buffer) : base(new MemoryStream(buffer, 0, buffer.Length, true, true), Encoding.UTF8, false)
        {
            this.buffer = buffer;
        }
    }
}