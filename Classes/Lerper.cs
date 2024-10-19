using UnityEngine;

namespace _UTIL_
{
    public abstract class Lerper<T>
    {
        public float timeA, timeB;
        public T valueA = default, valueB = default;

        //--------------------------------------------------------------------------------------------------------------

        public void Update(in float time, in float lerpTime, in T value)
        {
            valueA = GetLerp(time);
            timeA = time;
            timeB = time + lerpTime;
            valueB = value;
        }

        public T GetLerp(in float time) => GetLerp_(Mathf.InverseLerp(timeA, timeB, time));
        protected abstract T GetLerp_(in float lerp);
    }
}