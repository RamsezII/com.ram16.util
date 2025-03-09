using System.IO;
using System.Text;

public static partial class Util
{
    public static string LogBytes(this BinaryWriter writer)
    {
        lock (writer.BaseStream)
        {
            MemoryStream stream = (MemoryStream)writer.BaseStream;
            byte[] buffer = stream.GetBuffer();
            return LogBytes(buffer, 0, (int)stream.Position);
        }
    }

    public static string LogBytes(this BinaryReader reader)
    {
        lock (reader.BaseStream)
        {
            MemoryStream stream = (MemoryStream)reader.BaseStream;
            byte[] buffer = stream.GetBuffer();
            return LogBytes(buffer, (int)stream.Position, (int)stream.Length);
        }
    }

    public static string LogBytes(this byte[] buffer, in int start, in int end)
    {
        StringBuilder log = new($"{end - start} bytes: ");
        lock (buffer)
            for (int i = start; i < end; i++)
                log.Append($"| {buffer[i],3} ");
        return log.ToString();
    }
}