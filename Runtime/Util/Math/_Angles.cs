using UnityEngine;

public static partial class Util
{
    public static float SignedAngle(in Vector3 from, in Vector3 to, in Vector3 axis) => Vector3.SignedAngle(Vector3.ProjectOnPlane(from, axis), Vector3.ProjectOnPlane(to, axis), axis);

    public static float SignedAngle(this float angle)
    {
        if (angle > 180)
            angle -= 360;
        else if (angle < -180)
            angle += 360;
        return angle;
    }

    public static float UnsignedAngle(this float angle)
    {
        if (angle < 0)
            angle += 360;
        return angle;
    }

}