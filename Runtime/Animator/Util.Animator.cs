using UnityEngine;

public static partial class Util
{
    public static float GetNormlizedTime(this Animator animator, in int layerIndex = 0) => animator.GetStateInfo(layerIndex).normalizedTime;

    public static float GetNormlizedTimeClamped(this Animator animator, in int layerIndex = 0) => Mathf.Clamp01(GetNormlizedTime(animator, layerIndex));

    public static float GetNormlizedTimeModulo(this Animator animator, in int layerIndex = 0)
    {
        float ntime = GetNormlizedTime(animator, layerIndex);
        if (ntime > 1)
            ntime %= 1;
        return ntime;
    }

    public static AnimatorStateInfo GetStateInfo(this Animator animator, in int layerIndex) => animator.IsInTransition(layerIndex) ? animator.GetNextAnimatorStateInfo(layerIndex) : animator.GetCurrentAnimatorStateInfo(layerIndex);

    public static Transform GetRootBone(this Animator animator)
    {
        Transform root = animator.GetBoneTransform(HumanBodyBones.Hips);
        if (root != animator.transform)
            do { root = root.parent; }
            while (root != animator.transform);

        return root;
    }
}