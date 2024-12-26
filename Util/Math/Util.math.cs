using UnityEngine;

public static partial class Util
{
    public static void Clamp(ref this float value, in float min, in float max) => Mathf.Clamp(value, min, max);
    public static void Clamp01(ref this float value) => Mathf.Clamp01(value);

    public static void ClampMagnitude(ref this Vector2 value, in float maxLength) => Vector2.ClampMagnitude(value, maxLength);
    public static void ClampMagnitude(ref this Vector3 value, in float maxLength) => Vector3.ClampMagnitude(value, maxLength);

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

    public static void Repeat0(ref this int value, in int max)
    {
        value %= max;
        while (value < 0)
            value += max;
    }
}