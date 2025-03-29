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
        public Action<List<T>> _listeners2, _oneTimeListeners;
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

        public void AddOneTimeListener(in Action<List<T>> listener)
        {
            _oneTimeListeners -= listener;
            _oneTimeListeners += listener;
        }

        public void ModifyList(in Action<List<T>> onList)
        {
            int count = _list.Count;

            onList(_list);
            _listeners2?.Invoke(_list);

            if (count != _list.Count)
                if (count == 0 || _list.Count == 0)
                    _listeners1?.Invoke(!IsEmpty);

            _oneTimeListeners?.Invoke(_list);
            _oneTimeListeners = null;
        }

        public bool ToggleElement(T element, bool toggle)
        {
            bool contained = _list.Contains(element);
            if (contained)
            {
                if (!toggle)
                    ModifyList(list => list.Remove(element));
            }
            else
            {
                if (toggle)
                    ModifyList(list => list.Add(element));
            }
            return contained;
        }

        public bool ToggleElement(T element)
        {
            if (_list.Contains(element))
            {
                ModifyList(list => list.Remove(element));
                return false;
            }
            ModifyList(list => list.Add(element));
            return true;
        }

        public void AddElement(T element)
        {
            bool empty = IsEmpty;
            if (IsNotEmpty)
                if (element.Equals(_list[^1]))
                    return;
                else
                    _list.Remove(element);
            ModifyList(list => list.Add(element));
        }

        public bool RemoveElement(T element)
        {
            if (_list.Contains(element))
            {
                ModifyList(list => list.Remove(element));
                return true;
            }
            return false;
        }

        public void ClearList()
        {
            ModifyList(list => list.Clear());
        }

        public void Reset()
        {
            _list.Clear();
            _listeners2 = null;
            _listeners1 = null;
        }
    }
}