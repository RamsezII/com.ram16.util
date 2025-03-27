using System;
using System.Collections.Generic;
using System.IO;

partial class Util
{
    public static IList<T> SegmentF<T>(this T[] array, in Range range) => range.End.IsFromEnd ? SegmentFE(array, range) : SegmentFS(array, range);
    public static IList<T> SegmentFS<T>(this T[] array, in Range range) => new ArraySegment<T>(array, range.Start.Value, range.End.Value - range.Start.Value);
    public static IList<T> SegmentFE<T>(this T[] array, in Range range) => new ArraySegment<T>(array, range.Start.Value, array.Length - range.End.Value - range.Start.Value);
    public static byte[] CopyBytes(this BinaryWriter writer) => writer.GetBuffer()[..(int)writer.BaseStream.Position];
}