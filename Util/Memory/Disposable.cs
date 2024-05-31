using System;

namespace _UTIL_
{
    public class Disposable : IDisposable
    {
        public Action onDispose;
        public readonly ThreadSafe<bool> disposed = new();

        //----------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            lock (disposed)
            {
                if (disposed._value)
                    return;
                disposed._value = true;
            }
            OnDispose();
            onDispose?.Invoke();
            onDispose = null;
        }

        protected virtual void OnDispose()
        {
        }
    }
}