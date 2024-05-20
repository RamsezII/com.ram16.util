using System.IO;
using System.Text;

namespace _UTIL_
{
    public class StdReader : BinaryReader
    {
        public ushort end;
        public byte[] buffer;
        public bool HasNext => BaseStream.Position < end;
        public void Skip() => BaseStream.Position = end;
        public byte[] Buffer => buffer ?? this.GetBuffer();
        public ushort Remaining() => (ushort)(end - BaseStream.Position);

        //----------------------------------------------------------------------------------------------------------

        public StdReader() : base(new MemoryStream(), Encoding.UTF8, true)
        {
        }

        public StdReader(in ushort size) : this(new byte[size])
        {
        }

        public StdReader(in byte[] buffer) : base(new MemoryStream(buffer, 0, buffer.Length, true, true), Encoding.UTF8, false)
        {
            this.buffer = buffer;
            end = (ushort)buffer.Length;
        }

        //----------------------------------------------------------------------------------------------------------

        public byte[] VampireCopy() => VampireCopy(end);
        public byte[] VampireCopy(in ushort stop)
        {
            if (BaseStream.Position < stop)
            {
                ushort start = (ushort)BaseStream.Position;
                BaseStream.Position = stop;
                return buffer[start..stop];
            }
            else
                return null;
        }
    }
}