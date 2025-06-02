using UnityEngine;

public static partial class Util
{
    public static void TargetRotation(this ConfigurableJoint joint, in Quaternion target_q, in float global_w)
    {
        Quaternion
            jnt_q = Quaternion.LookRotation(joint.axis, joint.secondaryAxis) * Quaternion.Euler(0, -90, 0),
            jnt_q_ = Quaternion.Inverse(jnt_q);

        joint.targetRotation = Quaternion.Inverse(jnt_q_ * Quaternion.Slerp(Quaternion.identity, Quaternion.Inverse(joint.connectedBody.rotation), global_w) * target_q * jnt_q);
    }
}