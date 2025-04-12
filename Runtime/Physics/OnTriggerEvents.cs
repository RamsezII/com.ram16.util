using System;
using System.Collections.Generic;
using UnityEngine;

namespace _UTIL_
{
    public class OnTriggerEvents : MonoBehaviour
    {
        public enum Types
        {
            Enter,
            Stay,
            Exit,
        }

#if UNITY_EDITOR
        [Header("~@ Editor @~")]
        [SerializeField] List<Collider> colliders;
        [SerializeField] List<Collider2D> colliders2D;
#endif
        public Action<Collider, Types> onTriggerEvent;
        public Action<Collider2D, Types> onTriggerEvent2D;

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
            onTriggerEvent?.Invoke(other, Types.Enter);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
#if UNITY_EDITOR
            colliders2D.Add(other);
#endif
            onTriggerEvent2D?.Invoke(other, Types.Enter);
        }

        //--------------------------------------------------------------------------------------------------------------

        private void OnTriggerExit(Collider other)
        {
#if UNITY_EDITOR
            colliders.Remove(other);
#endif
            onTriggerEvent?.Invoke(other, Types.Exit);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
#if UNITY_EDITOR
            colliders2D.Remove(other);
#endif
            onTriggerEvent2D?.Invoke(other, Types.Exit);
        }
    }
}