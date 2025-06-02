using UnityEngine;

public static partial class Util
{
    public static bool IsNaN(this in Vector2 value) => float.IsNaN(value.x) || float.IsNaN(value.y);
    public static bool IsNaN(this in Vector3 value) => float.IsNaN(value.x) || float.IsNaN(value.y) || float.IsNaN(value.z);
    public static bool IsNaN(this in Quaternion value) => float.IsNaN(value.x) || float.IsNaN(value.y) || float.IsNaN(value.z) || float.IsNaN(value.w);
}