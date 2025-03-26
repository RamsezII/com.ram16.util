using UnityEngine;

public static partial class Util
{
    /*
     * angle = arcsin(hauteur / hypotenuse)
     * Al-Kashi theorem
     */

    public static bool SolveIK(in float at, in float ab, in float bc, out float angleA, out float angleB, out float angleC)
    {
        if (at < ab + bc)
        {
            float ab2 = ab * ab;
            float bc2 = bc * bc;
            float at2 = at * at;

            angleA = Mathf.Rad2Deg * Mathf.Acos((at2 + ab2 - bc2) / (2 * at * ab));
            angleB = Mathf.Rad2Deg * Mathf.Acos((ab2 + bc2 - at2) / (2 * ab * bc));
            angleC = 180 - angleA - angleB;

            if (float.IsNaN(angleA) || float.IsNaN(angleB) || float.IsNaN(angleC))
                Debug.LogWarning($"[ALERT_NaN] {typeof(Util).FullName}.{nameof(SolveIK)} {{ {nameof(angleA)}: {angleA}, {nameof(angleB)}: {angleB}, {{ {nameof(angleC)}: {angleC} }}");
            else
                return true;
        }

        angleA = 0;
        angleB = 180;
        angleC = 0;
        return false;
    }

    [System.Obsolete("Use SolveIK instead")]
    public static bool SolveIK1(in float at, in float ab, in float bc, out Vector3 result)
    {
        if (at < ab + bc)
        {
            float a_size2 = ab * ab;
            float b_size2 = bc * bc;

            result = new(0, 0, (at * at - b_size2 + a_size2) / (2 * at));
            result.y = Mathf.Pow(a_size2 - result.z * result.z, .5f);
            if (float.IsNaN(result.y))
                Debug.LogWarning("IK1: NaN");
            return true;
        }
        else
        {
            result = new(0, 0, ab);
            return false;
        }
    }

    [System.Obsolete("Use SolveIK instead")]
    public static bool SolveIK2(in Vector3 a_pos, in Vector3 hint, in Vector3 target, in float ab, in float bc, in bool flipUp, out Quaternion a_rot, out Vector3 b_pos, out Quaternion b_rot)
    {
        Quaternion rot = Quaternion.LookRotation(target - a_pos, hint - a_pos);
        float distance = Vector3.Distance(a_pos, target);

        if (SolveIK1(distance, ab, bc, out Vector3 result))
        {
            a_rot = Quaternion.LookRotation(rot * result, flipUp ? a_pos - hint : hint - a_pos);
            b_pos = a_pos + a_rot * new Vector3(0, 0, ab);
            b_rot = Quaternion.LookRotation(target - b_pos, flipUp ? b_pos - hint : hint - b_pos);
            return true;
        }
        else
        {
            b_pos = a_pos + rot * new Vector3(0, 0, ab);
            a_rot = b_rot = flipUp ? rot * Quaternion.Euler(0, 0, 180) : rot;
            return false;
        }
    }
}