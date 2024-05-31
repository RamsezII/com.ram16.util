using System;
using System.Collections.Generic;
using UnityEngine;

namespace _UTIL_
{
    public class TriggerEnter : MonoBehaviour
    {
#if UNITY_EDITOR
        [Header("~@ Editor @~")]
        [SerializeField] List<Collider> colliders;
#endif
        public Action<Collider> onTriggerEnter;

        //--------------------------------------------------------------------------------------------------------------

#if UNITY_EDITOR
        private void Awake()
        {
            colliders = new();
        }
#endif

        //--------------------------------------------------------------------------------------------------------------

        private void OnTriggerEnter(Collider other)
        {
#if UNITY_EDITOR
            colliders.Add(other);
#endif
            onTriggerEnter?.Invoke(other);
        }

        //--------------------------------------------------------------------------------------------------------------

#if UNITY_EDITOR
        private void OnTriggerExit(Collider other) => colliders.Remove(other);
#endif
    }
}