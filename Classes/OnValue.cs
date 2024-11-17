using System;

namespace _UTIL_
{
    [Serializable]
    public class OnValue<T>
    {
        public bool changed;
        public T _value, old;
        protected Action<T> onChange;
        public Action<T> onUpdate, afterChange;

        //------------------------------------------------------------------------------------------------------------------------------

        public OnValue(in T init = default) => _value = old = init;

        //------------------------------------------------------------------------------------------------------------------------------

        public void AddOnChange(in Action<T> action)
        {
            onChange += action;
            if (changed)
                action(Value);
        }

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
        }

        public virtual bool Update(in T value)
        {
            lock (this)
            {
                changed = !Util.Equals2(value, _value);

                if (changed)
                    onChange?.Invoke(value);

                onUpdate?.Invoke(value);
                old = _value;
                _value = value;
            }
            if (changed)
                afterChange?.Invoke(value);
            return changed;
        }

        public virtual void ForceUpdate() => ForceUpdate(Value);
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