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
            Vector3 velocity = (Vector3.Max(Vector3.zero, newValue.positive - positive) + Vector3.Min(Vector3.zero, newValue.negative - negative)) / Util.DeltaTime;
            positive = newValue.positive;
            negative = newValue.negative;
            return velocity;
        }
    }
}