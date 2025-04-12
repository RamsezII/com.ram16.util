using UnityEngine;

partial class Util
{
    public static float EvaluateRecoilCurve(this in float treshold, in float time)
    {
        float lerp;
        if (time <= treshold)
            lerp = Mathf.SmoothStep(-1, 1, Mathf.InverseLerp(-treshold, treshold, time));
        else
            lerp = Mathf.SmoothStep(0, 1, Mathf.InverseLerp(1, treshold, time));
        return lerp;
    }
}