using System;
using System.IO;
using UnityEngine;

public static partial class Util
{
    public static bool TryPullLengthPrefixedData(this BinaryReader reader, out byte[] data, in ushort maxSize = ushort.MaxValue)
    {
        ushort position = (ushort)reader.BaseStream.Position;
        reader.BaseStream.Position = 0;

        data = null;
        ushort total = 0;

        while (reader.BaseStream.Position < reader.BaseStream.Length)
        {
            ushort size = reader.ReadUInt16();
            if (total + size <= maxSize)
            {
                total += size;
                reader.BaseStream.Position += size;
            }
            else
            {
                reader.BaseStream.Position -= 2;
                if (total == 0)
                {
                    Debug.LogError($"Prefixed data ({size} bytes) exceeds maximum size ({maxSize} bytes)");
                    return false;
                }
                else
                    break;
            }
        }

        byte[] buffer = ((MemoryStream)reader.BaseStream).GetBuffer();
        data = buffer[..total];

        reader.BaseStream.Position = position - total;
        Buffer.BlockCopy(buffer, total, buffer, 0, buffer.Length - total);
        reader.BaseStream.SetLength(reader.BaseStream.Length - total);

        return true;
    }
}