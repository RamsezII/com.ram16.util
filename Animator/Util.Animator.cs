using UnityEngine;

public static partial class Util
{
    public static float GetNormlizedTime01(this Animator animator, in int layerIndex = 0) => Mathf.Clamp01(animator.GetStateInfo(layerIndex).normalizedTime);

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