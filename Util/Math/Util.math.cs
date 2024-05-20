using UnityEngine;

public static partial class Util
{
    public static float InverseLerpUnclamped(in float a, in float b, in float value) => a == b ? 0f : (value - a) / (b - a);

    public static byte ToPercent(this in float lerp01) => (byte)Mathf.RoundToInt(lerp01 * 100 / .9f);

    [System.Obsolete]
    public static int Repeat(this int value, int max)
    {
        switch (max)
        {
            case 0:
                Debug.LogWarning(nameof(max) + ": " + max + " == 0");
                return 0;

            case 1:
                return 0;

            default:
                while (value < 0)
                    value += max;
                return value % max;
        }
    }

    [System.Obsolete]
    public static int Repeat(this int val, int min, int max)
    {
        val -= min;
        max -= min;
        return min + Repeat(val, max);
    }

    public static void Repeat0(ref this int value, in int max)
    {
        value %= max;
        while (value < 0)
            value += max;
    }
}