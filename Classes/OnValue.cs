using System;

namespace _UTIL_
{
    [Serializable]
    public class OnValue : OnValue<object>
    {
    }

    [Serializable]
    public class OnValue<T>
    {
        public bool changed;
        public T _value, old;
        public Action<T> onChange;
        public Func<T, T> processor;
        [Obsolete] public Action<T> onUpdate;

        //------------------------------------------------------------------------------------------------------------------------------

        public OnValue(in T init = default) => _value = old = init;

        //------------------------------------------------------------------------------------------------------------------------------

        public void Reset(in T value = default)
        {
            changed = false;
            _value = default;
            old = default;
            onChange = null;
            processor = null;
            onUpdate = null;
            Update(value);
        }

        public T Value
        {
            get
            {
                lock (this)
                    return _value;
            }
        }

        public T PullValue
        {
            get
            {
                lock (this)
                {
                    T temp = _value;
                    Update(default);
                    return temp;
                }
            }
        }

        public bool PullChanged()
        {
            lock (this)
                if (changed)
                {
                    changed = false;
                    return true;
                }
                else
                    return false;
        }

        public void AddListener(in Action<T> action)
        {
            lock (this)
            {
                onChange -= action;
                onChange += action;
                action(Value);
            }
        }

        public void RemoveListener(in Action<T> action)
        {
            lock (this)
                onChange -= action;
        }

        public void AddProcessor(in Func<T, T> processor)
        {
            lock (this)
            {
                this.processor += processor;
                Update(_value);
            }
        }

        public virtual void ForceUpdate() => Update(Value, true);
        public virtual bool Update(T value, in bool force = false)
        {
            lock (this)
            {
                old = _value;

                if (processor != null)
                    value = processor(value);

                changed = force || !Util.Equals2(value, _value);
                _value = value;

                onUpdate?.Invoke(value);
                if (changed)
                    onChange?.Invoke(value);

                return changed;
            }
        }
    }
}