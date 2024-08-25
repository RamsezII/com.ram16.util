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
        [SerializeField] List<Collider2D> colliders2D;
#endif
        public Action<Collider> onTriggerEnter;
        public Action<Collider2D> onTriggerEnter2;

        //--------------------------------------------------------------------------------------------------------------

#if UNITY_EDITOR
        private void Awake()
        {
            colliders = new();
            colliders2D = new();
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
#if UNITY_EDITOR
            colliders2D.Add(collision);
#endif
            onTriggerEnter2?.Invoke(collision);
        }

        //--------------------------------------------------------------------------------------------------------------

#if UNITY_EDITOR
        private void OnTriggerExit(Collider other) => colliders.Remove(other);
        private void OnTriggerExit2D(Collider2D other) => colliders2D.Remove(other);
#endif
    }
}