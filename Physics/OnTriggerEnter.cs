using System;
using UnityEngine;

namespace _UTIL_
{
    public class TriggerEnter : MonoBehaviour
    {
        public Action<Collider> onTriggerEnter;
        private void OnTriggerEnter(Collider other) => onTriggerEnter?.Invoke(other);
    }
}