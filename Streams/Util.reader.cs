using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

public static partial class Util
{
    public static string ReadText(this BinaryReader reader)
    {
        ushort size = reader.ReadUInt16();
        if (size > 0)
            return Encoding.UTF8.GetString(reader.ReadBytes(size));
        else
            return string.Empty;
    }

    public static bool TryReadText(this BinaryReader reader, out string text)
    {
        ushort size = reader.ReadUInt16();
        if (size > 0)
        {
            text = Encoding.UTF8.GetString(reader.ReadBytes(size));
            return true;
        }
        else
        {
            text = string.Empty;
            return false;
        }
    }

    public static bool ReadBool(this BinaryReader reader) => reader.ReadByte() != 0;

    public static float Read_f16(this BinaryReader reader) => Mathf.HalfToFloat(reader.ReadUInt16());

    public static Vector3 ReadV3_3f32(this BinaryReader reader) => new(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

    public static Vector3 ReadV3_3f16(this BinaryReader reader)
    {
        Vector3 v3 = new();
        for (int i = 0; i < 3; ++i)
            v3[i] = reader.Read_f16();
        return v3;
    }

    public static Quaternion ReadQ_3u8(this BinaryReader reader) => Quaternion.Euler(360f / byte.MaxValue * new Vector3(reader.ReadByte(), reader.ReadByte(), reader.ReadByte())).normalized;

    public static Quaternion ReadQ_3f16(this BinaryReader reader) => Quaternion.Euler(new Vector3(reader.Read_f16(), reader.Read_f16(), reader.Read_f16())).normalized;

    public static Quaternion ReadQ_4f16(this BinaryReader reader) => new Quaternion(reader.Read_f16(), reader.Read_f16(), reader.Read_f16(), reader.Read_f16()).normalized;

    public static Quaternion ReadQ_4f32(this BinaryReader reader) => new Quaternion(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()).normalized;

    public static IPEndPoint ReadIPEnd(this BinaryReader reader) => new(reader.ReadUInt32(), reader.ReadUInt16());
}