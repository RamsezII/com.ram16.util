using UnityEngine;

namespace _UTIL_._OLD3_
{
    public abstract class Smooth<T> : OnValue<T> where T : struct
    {
        public T velocity;
        public T target, delta;
        public abstract bool isUp { get; }

        //----------------------------------------------------------------------------------------------------------

        public Smooth(in T init = default) : base(init) { target = init; }

        //----------------------------------------------------------------------------------------------------------

        public override void ForceUpdate(in T value)
        {
            base.ForceUpdate(value);
            target = value;
        }
    }

    [System.Serializable]
    public class SmoothFloat : Smooth<float>
    {
        public override bool isUp => target > _value;

        //----------------------------------------------------------------------------------------------------------

        public SmoothFloat(in float init = default) : base(init) { }

        //----------------------------------------------------------------------------------------------------------

        public override bool Update(in float value)
        {
            delta = value - base._value;
            return base.Update(value);
        }

        public override void ForceUpdate(in float value)
        {
            delta = default;
            base.ForceUpdate(value);
        }

        public bool SmoothDamp(in float damp, in float deltaTime) => Update(Mathf.SmoothDamp(_value, target, ref velocity, damp, Mathf.Infinity, deltaTime));

        public bool SmoothDamp(in float up, in float down, in float deltaTime) => Update(Mathf.SmoothDamp(_value, target, ref velocity, target > _value ? up : down, Mathf.Infinity, deltaTime));

        public bool SmoothDamp(in float up, in float down, in float limit, in float deltaTime) => Update(Mathf.SmoothDamp(_value, target, ref velocity, target > _value ? up : down, limit, deltaTime));

        public bool SmoothDampAngle(in float damp, in float deltaTime) => Update(Mathf.SmoothDampAngle(_value, target, ref velocity, damp, Mathf.Infinity, deltaTime));
    }

    public abstract class SmoothVector<T> : Smooth<T> where T : struct
    {
        [Min(0)] public float sqr;

        //----------------------------------------------------------------------------------------------------------

        public SmoothVector(in T init = default) : base(init) { }
    }

    [System.Serializable]
    public class SmoothVector2 : SmoothVector<Vector2>
    {
        public override bool isUp => target.sqrMagnitude > sqr;

        //----------------------------------------------------------------------------------------------------------

        public SmoothVector2(in Vector2 init = default) : base(init) { sqr = init.sqrMagnitude; }

        //----------------------------------------------------------------------------------------------------------

        public override bool Update(in Vector2 value)
        {
            delta = value - this._value;
            return base.Update(value);
        }

        public override void ForceUpdate(in Vector2 value)
        {
            delta = value - this._value;
            base.ForceUpdate(value);
        }

        public bool SmoothLerp(in float damp, in float deltaTime)
        {
            Vector2 val = Vector2.SmoothDamp(_value, target, ref velocity, damp, Mathf.Infinity, deltaTime);
            sqr = val.sqrMagnitude;
            return Update(val);
        }

        public bool SmoothLerp(in float spring, in float damp, in float deltaTime)
        {
            Vector2 val = Vector2.SmoothDamp(_value, target * spring - _value * (spring - 1), ref velocity, damp, Mathf.Infinity, deltaTime);
            sqr = val.sqrMagnitude;
            return Update(val);
        }
    }

    [System.Serializable]
    public class SmoothVector3 : SmoothVector<Vector3>
    {
        public override bool isUp => target.sqrMagnitude > sqr;

        //----------------------------------------------------------------------------------------------------------

        public SmoothVector3(in Vector3 init = default) : base(init) { sqr = init.sqrMagnitude; }

        //----------------------------------------------------------------------------------------------------------

        public override bool Update(in Vector3 value)
        {
            delta = value - this._value;
            return base.Update(value);
        }

        public override void ForceUpdate(in Vector3 value)
        {
            delta = value - this._value;
            base.ForceUpdate(value);
        }

        public bool SmoothLerp(in float damp, in float deltaTime)
        {
            Vector3 val;
            if (damp < .01f)
            {
                val = target;
                velocity = Vector3.zero;
            }
            else
                val = Vector3.SmoothDamp(_value, target, ref velocity, damp, Mathf.Infinity, deltaTime);
            sqr = val.sqrMagnitude;
            return Update(val);
        }

        public bool SmoothLerp(in float spring, in float damp, in float deltaTime)
        {
            Vector3 val = Vector3.SmoothDamp(_value, target * spring - _value * (spring - 1), ref velocity, damp, Mathf.Infinity, deltaTime);
            sqr = val.sqrMagnitude;
            return Update(val);
        }
    }
}