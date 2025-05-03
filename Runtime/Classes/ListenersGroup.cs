using System;
using System.Collections.Generic;
using UnityEngine;

namespace _UTIL_
{
    public class ListenersGroup<T> : IDisposable
    {
        public interface IUser
        {
            public bool Disposed { get; }
        }

        readonly List<(Action<T> action, object user)> listeners = new();
        bool disposed;

        //--------------------------------------------------------------------------------------------------------------

        public void AddListener(in Action<T> action, in object user)
        {
            listeners.Add((action, user));
        }

        public void RemoveListener_action(in Action<T> action)
        {
            for (int i = 0; i < listeners.Count; i++)
                if (listeners[i].action == action)
                {
                    listeners.RemoveAt(i);
                    break;
                }
        }

        public void RemoveListener_user(in object user)
        {
            for (int i = 0; i < listeners.Count; i++)
                if (listeners[i].user == user)
                {
                    listeners.RemoveAt(i);
                    break;
                }
        }

        public void NotifyListeners(in T value)
        {
            for (int i = 0; i < listeners.Count; ++i)
            {
                (Action<T> action, object user) = listeners[i];
                if (user == null || user is IUser iuser && iuser.Disposed)
                    listeners.RemoveAt(i--);
                else if (action == null)
                    Debug.LogWarning($"null action in {GetType().FullName}");
                else
                    action(value);
            }
        }

        //--------------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            if (disposed)
                return;
            disposed = true;

            listeners.Clear();
        }
    }
}