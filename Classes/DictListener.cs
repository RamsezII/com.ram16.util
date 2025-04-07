using System;
using System.Collections.Generic;

namespace _UTIL_
{
    public class DictListener<KeyType, ValueType> : IDisposable
    {
        public readonly Dictionary<KeyType, ValueType> _dict;

        public Type key_type = typeof(KeyType), value_type = typeof(ValueType);

        public Action<bool> _listeners1;
        public Action<Dictionary<KeyType, ValueType>> _listeners2, _oneTimeListeners;

        //--------------------------------------------------------------------------------------------------------------

        public DictListener() : this(new())
        {
        }

        public DictListener(in Dictionary<KeyType, ValueType> dict)
        {
            _dict = dict;
        }

        //--------------------------------------------------------------------------------------------------------------

        public bool IsEmpty
        {
            get
            {
                lock (this)
                    return _dict.Count == 0;
            }
        }

        public bool IsNotEmpty
        {
            get
            {
                lock (this)
                    return _dict.Count > 0;
            }
        }

        public void AddListener1(in Action<bool> listener)
        {
            lock (this)
            {
                _listeners1 -= listener;
                _listeners1 += listener;
                listener(IsNotEmpty);
            }
        }

        public void AddListener2(in Action<Dictionary<KeyType, ValueType>> listener)
        {
            lock (this)
            {
                _listeners2 -= listener;
                _listeners2 += listener;
                listener(_dict);
            }
        }

        public void AddElement(KeyType key, ValueType value)
        {
            ModifyDict(dict => dict.Add(key, value));
        }

        public bool RemoveElement(ValueType value) => RemoveElement(value, out _);
        public bool RemoveElement(ValueType value, out HashSet<KeyType> keys)
        {
            bool res = false;
            HashSet<KeyType> _keys = new();

            ModifyDict(dict =>
            {
                foreach (var pair in dict)
                    if (pair.Value.Equals(value))
                    {
                        _keys.Add(pair.Key);
                        break;
                    }
                foreach (KeyType key in _keys)
                    dict.Remove(key);
            });

            keys = _keys;
            res = keys.Count > 0;
            return res;
        }

        public bool RemoveElement(KeyType key) => RemoveElement(key, out _);
        public bool RemoveElement(KeyType key, out ValueType value)
        {
            bool res = false;
            ValueType _out1 = default;
            ModifyDict(dict => res = dict.Remove(key, out _out1));
            value = _out1;
            return res;
        }

        public void ModifyDict(in Action<Dictionary<KeyType, ValueType>> onDict)
        {
            lock (this)
            {
                int count1 = _dict.Count;
                onDict(_dict);
                int count2 = _dict.Count;

                if (count1 != count2)
                {
                    if (count1 == 0 || count2 == 0)
                        _listeners1?.Invoke(count2 >= 0);
                    _listeners2?.Invoke(_dict);
                    _oneTimeListeners?.Invoke(_dict);
                    _oneTimeListeners = null;
                }
            }
        }

        //--------------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            ModifyDict(dict => dict.Clear());
            Reset();
        }

        public void Reset()
        {
            lock (this)
            {
                _listeners1 = null;
                _listeners2 = null;
                _oneTimeListeners = null;
                _dict.Clear();
            }
        }
    }
}