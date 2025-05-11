using System;
using UnityEngine;

namespace _UTIL_
{
    public sealed class MonoBehaviourCallbacks : MonoBehaviour
    {
        public Action<MonoBehaviourCallbacks> on_awake, on_start, on_enable, on_disable, on_destroy;
        public Action<MonoBehaviourCallbacks, bool> on_toggle;

        //--------------------------------------------------------------------------------------------------------------

        private void Awake()
        {
            on_awake?.Invoke(this);
        }

        //--------------------------------------------------------------------------------------------------------------

        private void Start()
        {
            on_start?.Invoke(this);
        }

        //--------------------------------------------------------------------------------------------------------------

        private void OnEnable()
        {
            on_enable?.Invoke(this);
            on_toggle?.Invoke(this, true);
        }

        //--------------------------------------------------------------------------------------------------------------

        private void OnDisable()
        {
            on_disable?.Invoke(this);
            on_toggle?.Invoke(this, false);
        }

        //--------------------------------------------------------------------------------------------------------------

        private void OnDestroy()
        {
            on_destroy?.Invoke(this);
        }
    }
}