using System.IO.Compression;
using System.IO;

public static partial class Util
{
    public static void GZip_Compress(in byte[] buffer, in BinaryWriter writer)
    {
        writer.StartPrefixedWrite(out ushort prefixePos);

        using GZipStream zip = new(writer.BaseStream, CompressionMode.Compress, true);
        zip.Write(buffer, 0, buffer.Length);
        zip.Close();

        writer.EndPrefixedWrite(prefixePos);
    }

    public static byte[] GZip_Decompress(in BinaryReader reader, in byte[] buffer)
    {
        ushort msgLength = reader.ReadUInt16();

        using GZipStream zip = new(reader.BaseStream, CompressionMode.Decompress);
        zip.Read(buffer, 0, msgLength);

        return buffer;
    }
}