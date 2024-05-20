using System;
using UnityEngine;

namespace _UTIL_
{
    public class AnimatedObject : MonoBehaviour
    {
        public Animator animator;
        public Action onMove;
        public Action<int> onIK;
        public Action<AnimationEvent> onEvent;

        //----------------------------------------------------------------------------------------------------------

        private void Awake() => animator = GetComponent<Animator>();

        //----------------------------------------------------------------------------------------------------------

        private void OnAnimationEvent(in AnimationEvent e) => onEvent(e);
        private void OnAnimatorMove() => onMove?.Invoke();
        private void OnAnimatorIK(int layerIndex) => onIK(layerIndex);
    }
}