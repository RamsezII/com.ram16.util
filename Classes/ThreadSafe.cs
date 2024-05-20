using _UTIL_;

namespace _UTIL_
{
    public class ThreadSafe<T>
    {
        public T _value;

        //----------------------------------------------------------------------------------------------------------

        public ThreadSafe(in T value = default) => Value = value;

        //----------------------------------------------------------------------------------------------------------

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
                    _value = value;
            }
        }

        public bool TryGetValue(out T value)
        {
            lock (this)
                value = _value;
            return value != null;
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

public static partial class Util
{
    public static bool PullValue(this ThreadSafe<bool> threadSafe)
    {
        lock (threadSafe)
            return threadSafe._value.PullValue();
    }
}