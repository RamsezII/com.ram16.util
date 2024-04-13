using System.Collections.Generic;

public static partial class Util
{
    public static byte GetFreeKey<T>(this Dictionary<byte, T> dict)
    {
        byte key = 0;
        while (dict.ContainsKey(key))
            key++;
        return key;
    }
}