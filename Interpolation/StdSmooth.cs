using System;
using UnityEngine;

namespace _UTIL_
{
    public abstract class StdSmooth<T>
    {
        public T value, old;
        public float lastUpdate;
        readonly float speed;

        //----------------------------------------------------------------------------------------------------------

        public StdSmooth(in float speed) => this.speed = speed;

        //----------------------------------------------------------------------------------------------------------

        public void OnValue(in T value, in float time)
        {
            old = Interp(time);
            this.value = value;
            lastUpdate = time;
        }

        //----------------------------------------------------------------------------------------------------------

        public abstract T Lerp(in float lerp);
        public T Interp(in float time)
        {
            float lerp = speed * (time - lastUpdate);
            if (lerp < 1)
                return Lerp(lerp);
            else
                return value;
        }
    }

    [Serializable]
    public class StdSmoothV3 : StdSmooth<Vector3>
    {
        public StdSmoothV3(in float speed) : base(speed) { }
        public override Vector3 Lerp(in float lerp) => Vector3.Lerp(old, value, lerp);
    }

    [Serializable]
    public class StdSmoothRot : StdSmooth<Quaternion>
    {
        public StdSmoothRot(in float speed) : base(speed)
        {
            old = value = Quaternion.identity;
        }
        public override Quaternion Lerp(in float lerp) => Quaternion.Slerp(old, value, lerp);
    }
}