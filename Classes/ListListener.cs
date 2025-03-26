using System;
using System.Collections.Generic;
using System.Linq;

namespace _UTIL_
{
    public class ListListener : ListListener<object>
    {
    }

    public class ListListener<T>
    {
        public readonly List<T> _list;
        public Action<bool> _listeners1;
        public Action<List<T>> _listeners2;
        public bool IsEmpty => _list.Count == 0;
        public bool IsNotEmpty => _list.Count > 0;
        public bool IsLast(in T element) => element != null && _list.Count > 0 && _list[^1].Equals(element);
        public bool IsEmptyOrLast(in T element) => IsEmpty || element != null && IsLast(element);

        //------------------------------------------------------------------------------------------------------------------------------

        public ListListener(in List<T> list)
        {
            _list = list;
        }

        public ListListener() : this(new List<T>())
        {
        }

        public ListListener(params T[] list) : this(list.ToList())
        {
        }

        //------------------------------------------------------------------------------------------------------------------------------

        public void AddListener1(in Action<bool> listener)
        {
            _listeners1 -= listener;
            _listeners1 += listener;
            listener(!IsEmpty);
        }

        public void AddListener2(in Action<List<T>> listener)
        {
            _listeners2 -= listener;
            _listeners2 += listener;
            listener(_list);
        }

        public void ModifyList(in Action<List<T>> onList)
        {
            int count = _list.Count;
            onList(_list);
            if (count != _list.Count)
                _listeners2?.Invoke(_list);
        }

        public bool ToggleElement(in T element, bool toggle)
        {
            bool contained = _list.Contains(element);

            if (contained)
            {
                if (!toggle)
                    RemoveElement(element);
            }
            else
            {
                if (toggle)
                    AddElement(element);
            }

            return contained;
        }

        public bool ToggleElement(in T element)
        {
            if (_list.Contains(element))
            {
                RemoveElement(element);
                return false;
            }
            AddElement(element);
            return true;
        }

        public void AddElement(in T element)
        {
            bool empty = IsEmpty;
            if (!_list.Contains(element))
            {
                _list.Add(element);
                _listeners2?.Invoke(_list);
            }
            if (empty != IsEmpty)
                _listeners1?.Invoke(!IsEmpty);
        }

        public bool RemoveElement(in T element)
        {
            bool res = false;
            bool empty = IsEmpty;
            if (_list.Remove(element))
            {
                _listeners2?.Invoke(_list);
                res = true;
            }
            if (empty != IsEmpty)
                _listeners1?.Invoke(!IsEmpty);
            return res;
        }

        public void Clear()
        {
            bool change = _list.Count > 0;
            _list.Clear();
            if (change)
            {
                _listeners2?.Invoke(_list);
                _listeners1?.Invoke(false);
            }
        }

        public void Reset()
        {
            _list.Clear();
            _listeners2 = null;
            _listeners1 = null;
        }
    }
}