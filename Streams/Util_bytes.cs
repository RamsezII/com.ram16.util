using System.IO;
using System.Text;

public static partial class Util
{
    public static string LogBytes(this BinaryWriter writer) => LogBytes(writer.BaseStream);
    public static string LogBytes(this BinaryReader reader) => LogBytes(reader.BaseStream);
    public static string LogBytes(this Stream stream)
    {
        StringBuilder log = new();

        lock (stream)
        {
            int pos = (int)stream.Position;
            stream.Position = 0;

            for (int i = 0; i < stream.Length; i++)
            {
                if (i == pos)
                    log.Append("| ");
                log.Append($"[{i}]:{stream.ReadByte()} ");
            }
            stream.Position = pos;
        }

        return log.ToString();
    }

    public static string LogBytes(this byte[] buffer)
    {
        StringBuilder log = new();

        lock (buffer)
            for (int i = 0; i < buffer.Length; i++)
                log.Append($"[{i}]:{buffer[i]} ");

        return log.ToString();
    }
}