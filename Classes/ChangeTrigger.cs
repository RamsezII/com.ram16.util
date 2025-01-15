using System;

namespace _UTIL_
{
    public class ChangeTrigger<T>
    {
        public T _value;
        public Action<T> onChange;

        //------------------------------------------------------------------------------------------------------------------------------

        public ChangeTrigger(in T init)
        {
            _value = init;
        }

        //------------------------------------------------------------------------------------------------------------------------------

        public void AddListener(in Action<T> listener)
        {
            onChange -= listener;
            onChange += listener;
            listener(_value);
        }

        public void GetAndTrigger(in Action<T> onGet)
        {
            onGet(_value);
            onChange?.Invoke(_value);
        }
    }
}