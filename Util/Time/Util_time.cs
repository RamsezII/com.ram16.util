using UnityEngine;

partial class Util
{
    public static float AutoTime() => Time.inFixedTimeStep ? Time.fixedTime : Time.time;
    public static float AutoTimeUnscaled() => Time.inFixedTimeStep ? Time.fixedUnscaledTime : Time.unscaledTime;
}