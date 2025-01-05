using UnityEngine;

public static partial class Util
{
    public static float Max(this in Vector3 value) => Mathf.Max(value.x, value.y, value.z);
    public static float Manhattan(this in Vector3 a, in Vector3 b) => Mathf.Max(Mathf.Abs(b.x - a.x), Mathf.Abs(b.y - a.y), Mathf.Abs(b.z - a.z));
    public static float ToWeight(this in Vector3 weight) => Mathf.Clamp(weight.x, weight.y, weight.z);

    public static Vector3 SignedEuler_OLD(this Vector3 value)
    {
        for (int i = 0; i < 3; i++)
        {
            if (value[i] > 180)
                value[i] -= 360;
            else if (value[i] < -180)
                value[i] += 360;
        }

        return value;
    }
}