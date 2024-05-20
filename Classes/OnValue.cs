using System;

namespace _UTIL_
{
    [Serializable]
    public class OnValue<T>
    {
        public bool changed;
        public T _value;
        public T old;
        public Action<T> onChange, onUpdate, afterChange;

        //------------------------------------------------------------------------------------------------------------------------------

        public OnValue(in T init = default) => _value = old = init;

        //------------------------------------------------------------------------------------------------------------------------------

        public bool PullChanged()
        {
            if (changed)
            {
                changed = false;
                return true;
            }
            else
                return false;
        }

        public T Value
        {
            get
            {
                lock (this)
                    return _value;
            }

            set
            {
                lock (this)
                {
                    changed = !Util.Equals2(value, _value);

                    if (changed)
                        onChange?.Invoke(value);

                    onUpdate?.Invoke(value);
                    old = _value;
                    _value = value;

                    if (changed)
                        afterChange?.Invoke(value);
                }
            }
        }

        [Obsolete("Use " + nameof(Value) + " instead")]
        public virtual bool Update(in T value)
        {
            if (value == null)
                changed = _value != null;
            else if (_value == null)
                changed = true;
            else
                changed = !value.Equals(_value);

            if (changed)
                onChange?.Invoke(value);

            onUpdate?.Invoke(value);
            old = _value;
            _value = value;

            if (changed)
                afterChange?.Invoke(value);

            return changed;
        }

        public virtual void ForceUpdate(in T value)
        {
            changed = true;

            onChange?.Invoke(value);
            onUpdate?.Invoke(value);
            old = _value;
            _value = value;
        }
    }
}