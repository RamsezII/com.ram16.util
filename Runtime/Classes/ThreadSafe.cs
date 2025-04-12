using System;

namespace _UTIL_
{
    public abstract class ThreadSafe<T>
    {
        public T _value;

        //----------------------------------------------------------------------------------------------------------

        protected ThreadSafe(in T value = default) => Value = value;

        //----------------------------------------------------------------------------------------------------------

        public virtual T Value
        {
            get
            {
                lock (this)
                    return _value;
            }
            set
            {
                lock (this)
                    _value = value;
            }
        }

        public T PullValue()
        {
            lock (this)
            {
                T value = _value;
                _value = default;
                return value;
            }
        }
    }

    [Serializable]
    public class ThreadSafe_struct<T> : ThreadSafe<T> where T : struct
    {
        public ThreadSafe_struct(in T value = default) : base(value)
        {
        }
    }

    [Serializable]
    public class ThreadSafe_class<T> : ThreadSafe<T> where T : class
    {
        public ThreadSafe_class(in T value = default) : base(value)
        {
        }

        //----------------------------------------------------------------------------------------------------------

        public bool TryGetValue(out T value)
        {
            lock (this)
                value = _value;
            return value != null;
        }

        public bool TryPullValue(out T value)
        {
            lock (this)
            {
                value = _value;
                _value = default;
            }
            return value != null;
        }
    }
}