using UnityEngine;

public static partial class Util
{
    // Y, X, Z
    public static void Check(this ref Quaternion rot)
    {
        if (rot == new Quaternion())
            rot = Quaternion.identity;
    }

    public static Vector3 AngularVelocity(this in Quaternion A, in Quaternion B)
    {
        Quaternion deltaRotation = B * Quaternion.Inverse(A);
        deltaRotation.ToAngleAxis(out float angle, out Vector3 axis);
        if (angle > 180f)
            angle -= 360f;
        return angle * axis;
    }
}