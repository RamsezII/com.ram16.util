using System;
using UnityEngine;

namespace _UTIL_
{
    public class MainThreadWaiter : MonoBehaviour
    {
        static MainThreadWaiter instance;
        Action onMainThread;

        //----------------------------------------------------------------------------------------------------------

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
            {
                Destroy(this);
                return;
            }
        }

        //----------------------------------------------------------------------------------------------------------

        public static void WaitForMainThread(in Action onMainThread)
        {
            lock (instance)
                instance.onMainThread += onMainThread;
        }

        private void Update()
        {
            lock (instance)
                if (onMainThread != null)
                {
                    onMainThread();
                    onMainThread = null;
                }
        }

        //----------------------------------------------------------------------------------------------------------

        private void OnDestroy()
        {
            if (this == instance)
                instance = null;
        }
    }
}