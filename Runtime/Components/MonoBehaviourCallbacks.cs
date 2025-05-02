using System;
using UnityEngine;

namespace _UTIL_
{
    public sealed class MonoBehaviourCallbacks : MonoBehaviour
    {
        public Action on_awake, on_start, on_enable, on_disable, on_destroy;
        public Action<bool> on_toggle;

        //--------------------------------------------------------------------------------------------------------------

        private void Awake()
        {
            on_awake?.Invoke();
        }

        //--------------------------------------------------------------------------------------------------------------

        private void Start()
        {
            on_start?.Invoke();
        }

        //--------------------------------------------------------------------------------------------------------------

        private void OnEnable()
        {
            on_enable?.Invoke();
            on_toggle?.Invoke(true);
        }

        //--------------------------------------------------------------------------------------------------------------

        private void OnDisable()
        {
            on_disable?.Invoke();
            on_toggle?.Invoke(false);
        }

        //--------------------------------------------------------------------------------------------------------------

        private void OnDestroy()
        {
            on_destroy?.Invoke();
        }
    }
}