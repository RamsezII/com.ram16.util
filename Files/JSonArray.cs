using System;
using System.IO;
using _UTIL_;

[Serializable]
public class JSonArray<T> : JSon where T : IBytes
{
    public T[] array;

    //----------------------------------------------------------------------------------------------------------

    public override void WriteBytes(in BinaryWriter writer)
    {
        base.WriteBytes(writer);

        writer.Write((ushort)array.Length);

        for (int i = 0; i < array.Length; ++i)
            array[i].WriteBytes(writer);
    }

    public override void ReadBytes(in BinaryReader reader)
    {
        base.ReadBytes(reader);

        ushort count = reader.ReadUInt16();
        array = new T[count];

        for (int i = 0; i < count; ++i)
            array[i].ReadBytes(reader);
    }
}