using System;

partial class Util
{
    public static void SetFlags<T>(ref T flags, T value, bool state) where T : Enum
    {
        if (state)
            flags = (T)Enum.ToObject(typeof(T), Convert.ToInt32(flags) | Convert.ToInt32(value));
        else
            flags = (T)Enum.ToObject(typeof(T), Convert.ToInt32(flags) & ~Convert.ToInt32(value));
    }
}