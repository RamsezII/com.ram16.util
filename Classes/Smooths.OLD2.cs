using UnityEngine;

namespace _UTIL_._OLD2_
{
    public abstract class Smooth<T> : OnValue<T> where T : struct
    {
        public T velocity;
        public T target, delta;
        public abstract bool isUp { get; }
        protected bool noNeed => target.Equals(_value) && velocity.Equals(default);

        //----------------------------------------------------------------------------------------------------------

        public Smooth(in T init = default) : base(init) { target = init; }

        //----------------------------------------------------------------------------------------------------------

        public override void ForceUpdate(in T value)
        {
            base.ForceUpdate(value);
            target = value;
        }

        protected abstract bool CheckNaN();
    }

    [System.Serializable]
    public class SmoothFloat : Smooth<float>
    {
        public override bool isUp => target > _value;

        //----------------------------------------------------------------------------------------------------------

        public SmoothFloat(in float init = default) : base(init) { }

        //----------------------------------------------------------------------------------------------------------

        protected override bool CheckNaN()
        {
            bool nanFlag = false;

            if(float.IsNaN(_value))
            {
                Debug.LogError($"putain de NaN: {nameof(_value)} ({_value})");
                _value = default;
                nanFlag = true;
            }
            if (float.IsNaN(velocity))
            {
                Debug.LogError($"putain de NaN: {nameof(velocity)} ({velocity})");
                velocity = default;
                nanFlag = true;
            }
            if (float.IsNaN(target))
            {
                Debug.LogError($"putain de NaN: {nameof(target)} ({target})");
                target = default;
                nanFlag = true;
            }
            if (float.IsNaN(delta))
            {
                Debug.LogError($"putain de NaN: {nameof(delta)} ({delta})");
                delta = default;
                nanFlag = true;
            }

            return nanFlag;
        }

        public override bool Update(in float value)
        {
            delta = value - base._value;
            return base.Update(value);
        }

        public override void ForceUpdate(in float value)
        {
            delta = value - base._value;
            base.ForceUpdate(value);
        }

        public bool SmoothDamp(in float damp, in float deltaTime)
        {
            if (!noNeed)
            {
                CheckNaN();
                bool upd = Update(Mathf.SmoothDamp(_value, target, ref velocity, damp, Mathf.Infinity, deltaTime));
                CheckNaN();
                return upd;
            }
            else
                return false;
        }

        public bool SmoothDamp(in float up, in float down, in float deltaTime)
        {
            if (!noNeed)
            {
                CheckNaN();
                bool upd = Update(Mathf.SmoothDamp(_value, target, ref velocity, target > _value ? up : down, Mathf.Infinity, deltaTime));
                CheckNaN();
                return upd;
            }
            else
                return false;
        }

        public bool SmoothDamp(in float up, in float down, in float limit, in float deltaTime)
        {
            if (!noNeed)
            {
                CheckNaN();
                bool upd = Update(Mathf.SmoothDamp(_value, target, ref velocity, target > _value ? up : down, limit, deltaTime));
                CheckNaN();
                return upd;
            }
            else
                return false;
        }

        public bool SmoothDampAngle(in float damp, in float deltaTime)
        {
            if (!noNeed)
            {
                CheckNaN();
                bool upd = Update(Mathf.SmoothDampAngle(_value, target, ref velocity, damp, Mathf.Infinity, deltaTime));
                CheckNaN();
                return upd;
            }
            else
                return false;
        }
    }

    public abstract class SmoothVector<T> : Smooth<T> where T : struct
    {
        [Min(0)] public float sqr;

        //----------------------------------------------------------------------------------------------------------

        public SmoothVector(in T init = default) : base(init) { }

        protected override bool CheckNaN()
        {
            bool nanFlag = false;

            if (float.IsNaN(sqr))
            {
                Debug.LogError($"putain de NaN: {nameof(sqr)} ({sqr})");
                sqr = 0;
                nanFlag = true;
            }

            return nanFlag;
        }
    }

    [System.Serializable]
    public class SmoothVector2 : SmoothVector<Vector2>
    {
        public override bool isUp => target.sqrMagnitude > sqr;

        //----------------------------------------------------------------------------------------------------------

        public SmoothVector2(in Vector2 init = default) : base(init) { sqr = init.sqrMagnitude; }

        //----------------------------------------------------------------------------------------------------------

        protected override bool CheckNaN()
        {
            bool nanFlag = false;

            if (base.CheckNaN())
                nanFlag = true;
            if (Util.IsNaN(_value))
            {
                Debug.LogError($"putain de NaN: {nameof(_value)} ({_value})");
                _value = default;
                nanFlag = true;
            }
            if (Util.IsNaN(velocity))
            {
                Debug.LogError($"putain de NaN: {nameof(velocity)} ({velocity})");
                velocity = default;
                nanFlag = true;
            }
            if (Util.IsNaN(target))
            {
                Debug.LogError($"putain de NaN: {nameof(target)} ({target})");
                target = default;
                nanFlag = true;
            }
            if (Util.IsNaN(delta))
            {
                Debug.LogError($"putain de NaN: {nameof(delta)} ({delta})");
                target = default;
                nanFlag = true;
            }

            return nanFlag;
        }

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
            if (!noNeed)
            {
                CheckNaN();
                Vector2 val = Vector2.SmoothDamp(_value, target, ref velocity, damp, Mathf.Infinity, deltaTime);
                if (CheckNaN())
                    val = default;
                sqr = val.sqrMagnitude;
                return Update(val);
            }
            else
                return false;
        }

        public bool SmoothLerp(in float spring, in float damp, in float deltaTime)
        {
            if (!noNeed)
            {
                CheckNaN();
                Vector2 val = Vector2.SmoothDamp(_value, target * spring - _value * (spring - 1), ref velocity, damp, Mathf.Infinity, deltaTime);
                if (CheckNaN())
                    val = default;
                sqr = val.sqrMagnitude;
                return Update(val);
            }
            else
                return false;
        }
    }

    [System.Serializable]
    public class SmoothVector3 : SmoothVector<Vector3>
    {
        public override bool isUp => target.sqrMagnitude > sqr;

        //----------------------------------------------------------------------------------------------------------

        public SmoothVector3(in Vector3 init = default) : base(init) { sqr = init.sqrMagnitude; }

        //----------------------------------------------------------------------------------------------------------

        protected override bool CheckNaN()
        {
            bool nanFlag = false;

            if (base.CheckNaN())
                nanFlag = true;
            if (Util.IsNaN(_value))
            {
                Debug.LogError($"putain de NaN: {nameof(_value)} ({_value})");
                _value = default;
                nanFlag = true;
            }
            if (Util.IsNaN(velocity))
            {
                Debug.LogError($"putain de NaN: {nameof(velocity)} ({velocity})");
                velocity = default;
                nanFlag = true;
            }
            if (Util.IsNaN(target))
            {
                Debug.LogError($"putain de NaN: {nameof(target)} ({target})");
                target = default;
                nanFlag = true;
            }
            if (Util.IsNaN(delta))
            {
                Debug.LogError($"putain de NaN: {nameof(delta)} ({delta})");
                target = default;
                nanFlag = true;
            }

            return nanFlag;
        }

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
            if (!noNeed)
            {
                CheckNaN();
                Vector3 val = Vector3.SmoothDamp(_value, target, ref velocity, damp, Mathf.Infinity, deltaTime);
                if (CheckNaN())
                    val = default;
                sqr = val.sqrMagnitude;
                return Update(val);
            }
            else
                return false;
        }

        public bool SmoothLerp(in float spring, in float damp, in float deltaTime)
        {
            if (!noNeed)
            {
                CheckNaN();
                Vector3 val = Vector3.SmoothDamp(_value, target * spring - _value * (spring - 1), ref velocity, damp, Mathf.Infinity, deltaTime);
                if (CheckNaN())
                    val = default;
                sqr = val.sqrMagnitude;
                return Update(val);
            }
            else
                return false;
        }
    }
}