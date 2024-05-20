using System;

namespace _UTIL_
{
    public class Disposable : IDisposable
    {
        public Action onDispose;
        bool disposed;

        //----------------------------------------------------------------------------------------------------------

        public Disposable(in Action onDispose)
        {
            this.onDispose = onDispose;
            disposed = false;
        }

        //----------------------------------------------------------------------------------------------------------

        public bool Disposed()
        {
            lock (this)
                return disposed;
        }

        //----------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            lock (this)
                if (!disposed)
                {
                    disposed = true;
                    onDispose?.Invoke();
                    onDispose = null;
                }
        }
    }
}