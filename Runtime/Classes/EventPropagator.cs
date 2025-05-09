using System;
using System.Collections.Generic;
using UnityEngine;

namespace _UTIL_
{
    public class EventPropagator : IDisposable
    {
        public interface IUser
        {
            public bool Disposed { get; }
        }

        readonly List<(Action action, object user)> _listeners = new();
        bool disposed;

        //--------------------------------------------------------------------------------------------------------------

        public void ClearListeners() => _listeners.Clear();

        public void AddListener(in Action action, in object user)
        {
            _listeners.Add((action, user ?? new object()));
            action();
        }

        public void RemoveListener_action(in Action action)
        {
            for (int i = 0; i < _listeners.Count; i++)
                if (_listeners[i].action == action)
                {
                    _listeners.RemoveAt(i);
                    break;
                }
        }

        public void RemoveListener_user(in object user)
        {
            for (int i = 0; i < _listeners.Count; i++)
                if (_listeners[i].user == user)
                {
                    _listeners.RemoveAt(i);
                    break;
                }
        }

        public void NotifyListeners()
        {
            for (int i = 0; i < _listeners.Count; ++i)
            {
                (Action action, object user) = _listeners[i];
                if (user == null || user is IUser iuser && iuser.Disposed)
                    _listeners.RemoveAt(i--);
                else if (action == null)
                    Debug.LogWarning($"null action in {GetType().FullName}");
                else
                    action();
            }
        }

        //--------------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            if (disposed)
                return;
            disposed = true;

            _listeners.Clear();
        }
    }

    public class EventPropagator<T> : IDisposable
    {
        public interface IUser
        {
            public bool Disposed { get; }
        }

        public readonly List<(Action<T> action, object user)> _listeners = new();
        public bool _disposed;

        //--------------------------------------------------------------------------------------------------------------

        public void ClearListeners() => _listeners.Clear();

        public void AddListener<U>(in T current_value, in U user, in Action<T> action) where U : class
        {
            if (user != null && user.ToString() == "null")
                throw new ArgumentNullException(nameof(user), "user cannot be 'null'");

            for (int i = 0; i < _listeners.Count; ++i)
            {
                var pair = _listeners[i];
                if (pair.user == user || pair.action == action)
                    _listeners.RemoveAt(i--);
            }

            _listeners.Add((action, user ?? new object()));
            action(current_value);
        }

        public void RemoveListener_action(in Action<T> action)
        {
            for (int i = 0; i < _listeners.Count; i++)
                if (_listeners[i].action == action)
                {
                    _listeners.RemoveAt(i);
                    break;
                }
        }

        public void RemoveListener_user(in object user)
        {
            for (int i = 0; i < _listeners.Count; i++)
                if (_listeners[i].user == user)
                {
                    _listeners.RemoveAt(i);
                    break;
                }
        }

        public void NotifyListeners(in T value)
        {
            for (int i = 0; i < _listeners.Count; ++i)
            {
                (Action<T> action, object user) = _listeners[i];
                if (user == null || user is IUser iuser && iuser.Disposed || user is UnityEngine.Object uo && uo == null)
                    _listeners.RemoveAt(i--);
                else if (action == null)
                    Debug.LogWarning($"null action in {GetType().FullName}");
                else
                    action(value);
            }
        }

        //--------------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;

            _listeners.Clear();
        }
    }
}