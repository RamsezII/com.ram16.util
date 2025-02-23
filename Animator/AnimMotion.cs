using System;
using UnityEngine;

namespace _UTIL_
{
    [Serializable]
    public struct AnimMotion
    {
        public Vector3 positive, negative;

        //-----------------------------------------------------------------------------------------

        public Vector3 ProcessAndUpdate(in AnimMotion newValue)
        {
            Vector3 velocity = (Vector3.Max(default, newValue.positive - positive) + Vector3.Min(default, newValue.negative - negative)) / Time.deltaTime;
            positive = newValue.positive;
            negative = newValue.negative;
            return velocity;
        }
    }
}