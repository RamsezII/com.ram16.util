public static partial class Util
{
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