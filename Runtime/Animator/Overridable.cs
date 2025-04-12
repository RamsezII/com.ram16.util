using System.Collections.Generic;
using UnityEngine;

namespace _UTIL_
{
    public partial class Overridable : MonoBehaviour
    {
        [HideInInspector] public Animator animator;
        protected AnimatorOverrideController anim2;
        readonly List<KeyValuePair<AnimationClip, AnimationClip>> ov_default = new();

        //----------------------------------------------------------------------------------------------------------

        public virtual void OnAwake()
        {
            animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = anim2 = new AnimatorOverrideController(animator.runtimeAnimatorController);
            anim2.name = nameof(anim2);
            anim2.GetOverrides(ov_default);
        }

        //----------------------------------------------------------------------------------------------------------

        public void ResetOverrides()
        {
            OnDefaultValues();
            anim2.ApplyOverrides(ov_default);
        }

        protected virtual void OnDefaultValues()
        {

        }
    }
}