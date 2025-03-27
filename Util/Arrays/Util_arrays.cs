using System.IO;

partial class Util
{
    public static byte[] CopyBytes(this BinaryWriter writer) => writer.GetBuffer()[..(int)writer.BaseStream.Position];
}