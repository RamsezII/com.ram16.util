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
            onChange += action;
            action(Value);
        }

        public void AddProcessor(in Func<T, T> processor)
        {
            this.processor += processor;
            T value = processor(_value);
            Update(value);
        }

        public virtual bool Update(T value)
        {
            if (processor != null)
                value = processor(value);

            lock (this)
            {
                changed = !Util.Equals2(value, _value);
                onUpdate?.Invoke(value);
                old = _value;
                _value = value;
            }

            if (changed)
                onChange?.Invoke(value);

            return changed;
        }

        public virtual void ForceUpdate() => ForceUpdate(Value);
        public virtual void ForceUpdate(T value)
        {
            changed = true;

            if (processor != null)
                value = processor(value);

            onChange?.Invoke(value);
            onUpdate?.Invoke(value);
            old = _value;
            _value = value;
        }
    }
}