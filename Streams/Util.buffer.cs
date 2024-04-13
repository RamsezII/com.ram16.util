using System;

public static partial class Util
{
    public static void RelocateData(this byte[] buffer, in ushort sourceIndex, in ushort destIndex, in ushort count)
    {

        if (sourceIndex + count < destIndex || sourceIndex > destIndex + count)
            Array.Copy(buffer, sourceIndex, buffer, destIndex, count);
        else
        {

        }
    }
}