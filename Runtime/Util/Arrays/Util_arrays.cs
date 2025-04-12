using System.IO;

partial class Util
{
    public static byte[] CopyBuffer(this BinaryWriter writer) => writer.GetBuffer()[..(int)writer.BaseStream.Position];
}