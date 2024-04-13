using UnityEngine;

namespace _UTIL_
{
    public enum Directions2 { Negative, Positive }
    public enum Directions4 { Up, Right, Down, Left }

    public abstract class OnAxis<T> : OnValue<T> where T : struct
    {
        public T current;
        [Range(0, 1)] public float min, max = 1;

        //----------------------------------------------------------------------------------------------------------

        public OnAxis() : base() { }

        //----------------------------------------------------------------------------------------------------------

        public abstract bool ReadValue(in T value);
        public abstract bool GetButton(in HoldStates state, in float treshold);
        public override bool Update(in T value) => throw new System.NotImplementedException();
        public virtual void Update()
        {
            base.Update(current);
            current = default;
        }
    }

    //----------------------------------------------------------------------------------------------------------

    [System.Serializable]
    public class OnAxis_f : OnAxis<float>
    {
        public override bool ReadValue(in float value)
        {
            if (value > min)
            {
                current += Mathf.InverseLerp(min, max, value);
                return true;
            }
            else
                return false;
        }

        public override void Update()
        {
            if (current > min)
                current = Mathf.InverseLerp(min, max, current);
            else
                current = 0;
            base.Update();
        }

        public override bool GetButton(in HoldStates state, in float treshold)
        {
            switch (state)
            {
                case HoldStates.Down:
                    return _value >= treshold && old < treshold;

                case HoldStates.Hold:
                    return _value >= treshold;

                case HoldStates.Up:
                    return _value < treshold && old >= treshold;

                default:
                    return false;
            }
        }
    }

    //----------------------------------------------------------------------------------------------------------

    [System.Serializable]
    public class OnAxis_v2 : OnAxis<Vector2>
    {
        public override bool ReadValue(in Vector2 value)
        {
            float sqr = value.magnitude;
            if (sqr > min)
            {
                current += value * Mathf.InverseLerp(min, max, sqr);
                return true;
            }
            else
                return false;
        }

        public override void Update()
        {
            float sqr = current.magnitude;
            if (sqr > min)
            {
                float lerp = Mathf.InverseLerp(min, max, sqr);
                current *= lerp;
            }
            else
                current = Vector2.zero;

            base.Update();
        }

        public override bool GetButton(in HoldStates state, in float treshold) => state switch
        {
            HoldStates.Down => _value.sqrMagnitude >= treshold && old.sqrMagnitude < treshold,
            HoldStates.Hold => _value.sqrMagnitude >= treshold,
            HoldStates.Up => _value.sqrMagnitude < treshold && old.sqrMagnitude >= treshold,
            _ => false,
        };

        public bool GetButton(in HoldStates state, in float treshold, in Directions4 axis) => state switch
        {
            HoldStates.Down => axis switch
            {
                Directions4.Up => _value.y >= treshold && old.y < treshold,
                Directions4.Right => _value.x >= treshold && old.x < treshold,
                Directions4.Down => _value.y <= -treshold && old.y >= -treshold,
                Directions4.Left => _value.x <= -treshold && old.x > -treshold,
                _ => false,
            },
            HoldStates.Hold => axis switch
            {
                Directions4.Up => _value.y >= treshold,
                Directions4.Right => _value.x >= treshold,
                Directions4.Down => _value.y <= -treshold,
                Directions4.Left => _value.x <= -treshold,
                _ => false,
            },
            HoldStates.Up => axis switch
            {
                Directions4.Up => _value.y < treshold && old.y >= treshold,
                Directions4.Right => _value.x < treshold && old.x >= treshold,
                Directions4.Down => _value.y > -treshold && old.y <= -treshold,
                Directions4.Left => _value.x > -treshold && old.x <= -treshold,
                _ => false,
            },
            _ => false,
        };

        public Vector2Int InputNav(in float treshold)
        {
            if (GetButton(HoldStates.Down, treshold))
            {
                Vector2Int nav = Vector2Int.zero;

                if (_value.y > treshold)
                    ++nav.y;
                if (_value.x > treshold)
                    ++nav.x;
                if (_value.y < treshold)
                    --nav.y;
                if (_value.x < treshold)
                    --nav.x;

                return nav;
            }
            else
                return Vector2Int.zero;
        }
    }
}