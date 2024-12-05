using System;

namespace _UTIL_
{
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

        public T Value
        {
            get
            {
                lock (this)
                    return _value;
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

        public virtual bool Update(T value)
        {
            lock (this)
            {
                old = _value;

                if (processor != null)
                    value = processor(value);

                changed = !Util.Equals2(value, _value);
                _value = value;

                onUpdate?.Invoke(value);
                if (changed)
                    onChange?.Invoke(value);

                return changed;
            }
        }

        public virtual void ForceUpdate() => ForceUpdate(Value);
        public virtual void ForceUpdate(T value)
        {
            lock (this)
            {
                changed = true;
                old = _value;

                if (processor != null)
                    value = processor(value);

                onUpdate?.Invoke(value);
                onChange?.Invoke(value);
                _value = value;
            }
        }
    }
}