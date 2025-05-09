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
        public readonly EventPropagator<bool> _listeners1 = new();
        public readonly EventPropagator<List<T>> _listeners2 = new(), _oneTimeListeners = new();

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

        void RemoveNulls()
        {
            for (int i = 0; i < _list.Count; i++)
                if (_list[i] == null || _list[i] is UnityEngine.Object ue && ue == null)
                    ModifyList(list => list.RemoveAt(i--));
        }

        public bool IsEmpty
        {
            get
            {
                RemoveNulls();
                lock (this)
                    return _list.Count == 0;
            }
        }

        public bool IsNotEmpty
        {
            get
            {
                RemoveNulls();
                lock (this)
                    return _list.Count > 0;
            }
        }

        public bool IsLast(in T element)
        {
            RemoveNulls();
            lock (this)
                return element != null && _list.Count > 0 && _list[^1].Equals(element);
        }

        public bool IsEmptyOrLast(in T element)
        {
            RemoveNulls();
            lock (this)
                return IsEmpty || element != null && IsLast(element);
        }

        public void AddListener1(in object user, in Action<bool> action)
        {
            RemoveNulls();
            lock (this)
                _listeners1.AddListener(IsNotEmpty, user, action);
        }

        public void AddListener2(in object user, in Action<List<T>> action)
        {
            RemoveNulls();
            lock (this)
                _listeners2.AddListener(_list, user, action);
        }

        public void AddOneTimeListener(in object user, in Action<List<T>> action)
        {
            RemoveNulls();
            lock (this)
                _oneTimeListeners.AddListener(_list, user, action);
        }

        public void ModifyList(in Action<List<T>> onList)
        {
            lock (this)
            {
                int count = _list.Count;

                onList(_list);
                RemoveNulls();

                _listeners2.NotifyListeners(_list);

                if (count != _list.Count)
                    if (count == 0 || _list.Count == 0)
                        _listeners1.NotifyListeners(IsNotEmpty);

                _oneTimeListeners.NotifyListeners(_list);
                _oneTimeListeners._listeners.Clear();
            }
        }

        public bool ToggleElement(T element, bool toggle)
        {
            lock (this)
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
        }

        public bool ToggleElement(T element)
        {
            lock (this)
            {
                if (_list.Contains(element))
                {
                    ModifyList(list => list.Remove(element));
                    return false;
                }
                ModifyList(list => list.Add(element));
                return true;
            }
        }

        public void AddElement(T element)
        {
            lock (this)
            {
                bool empty = IsEmpty;
                if (IsNotEmpty)
                    if (element.Equals(_list[^1]))
                        return;
                    else
                        _list.Remove(element);
                ModifyList(list => list.Add(element));
            }
        }

        public bool RemoveElement(T element)
        {
            lock (this)
            {
                if (_list.Contains(element))
                {
                    ModifyList(list => list.Remove(element));
                    return true;
                }
                return false;
            }
        }

        public void InsertElementAt(int index, T element)
        {
            lock (this)
                ModifyList(list => list.Insert(index, element));
        }

        public void RemoveElementAt(int index)
        {
            lock (this)
                ModifyList(list => list.RemoveAt(index));
        }

        public void ClearList()
        {
            ModifyList(list => list.Clear());
        }

        public void Reset()
        {
            lock (this)
            {
                _listeners1.Dispose();
                _listeners2.Dispose();
                _oneTimeListeners.Dispose();
                _list.Clear();
            }
        }
    }
}