using System.Collections.Generic;
using UnityEngine;

public static partial class Util
{
    public static bool PullValue(this ref bool flag)
    {
        if (flag)
        {
            flag = false;
            return true;
        }
        return false;
    }

    public static bool Equals2<T>(this T a, in T b)
    {
        if (a == null)
            return b == null;
        return a.Equals(b);
    }

    public static void Destroy(this Object obj)
    {
        if (Application.isPlaying)
            Object.Destroy(obj);
        else
            Object.DestroyImmediate(obj);
    }

    public static bool TryReserveKey<T>(this Dictionary<byte, T> dict, ref byte lastKey, in bool nullReservation, in byte excludeBelow = 0)
    {
        for (byte i = 1; i < byte.MaxValue; i++)
        {
            byte key = (byte)(i + lastKey);
            if (key > excludeBelow && !dict.ContainsKey(key))
            {
                if (nullReservation)
                    dict[key] = default;
                lastKey = key;
                return true;
            }
        }
        return false;
    }

    public static bool TryReserveKey<T>(this Dictionary<ushort, T> dict, ref ushort lastKey, in bool nullReservation, in ushort excludeBelow = 0)
    {
        for (ushort i = 1; i < ushort.MaxValue; i++)
        {
            ushort key = (ushort)(i + lastKey);
            if (key > excludeBelow && !dict.ContainsKey(key))
            {
                if (nullReservation)
                    dict[key] = default;
                lastKey = key;
                return true;
            }
        }
        return false;
    }
}