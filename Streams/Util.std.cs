using System.IO;

namespace _UTIL_
{
    public interface IBytes
    {
        void WriteBytes(in BinaryWriter writer);
        void ReadBytes(in BinaryReader reader);
    }
}