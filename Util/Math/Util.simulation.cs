using UnityEngine;

public static partial class Util
{
    public static Vector3 SimulateFreeFall(this in Vector3 initPos, in Vector3 initVlc, in Vector3 gravity, in float timelaps) => initPos + timelaps * (initVlc + .5f * timelaps * gravity);
}