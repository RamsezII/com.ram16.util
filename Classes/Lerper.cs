using UnityEngine;

namespace _UTIL_
{
    public abstract class Lerper<T>
    {
        public float timeA, timeB;
        public T valueA = default, valueB = default;

        //--------------------------------------------------------------------------------------------------------------

        public void Update(in float nowTime, in float nextTime, in T value)
        {
            valueA = GetLerp(nowTime);
            timeA = nowTime;
            timeB = nextTime;
            valueB = value;
        }

        public T GetLerp(in float time) => GetLerp_(Mathf.InverseLerp(timeA, timeB, time));
        protected abstract T GetLerp_(in float lerp);
    }
}