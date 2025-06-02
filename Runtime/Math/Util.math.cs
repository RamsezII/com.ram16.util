using UnityEngine;

public static partial class Util
{
    public static ushort LoopID(this ref byte id) => ++id == 0 ? ++id : id;
    public static ushort LoopID(this ref ushort id) => ++id == 0 ? ++id : id;

    public static void Clamp(ref this float value, in float min, in float max) => Mathf.Clamp(value, min, max);
    public static void Clamp01(ref this float value) => Mathf.Clamp01(value);

    public static void ClampMagnitude(ref this Vector2 value, in float maxLength) => Vector2.ClampMagnitude(value, maxLength);

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
}