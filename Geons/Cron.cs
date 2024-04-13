using System;

namespace _UTIL_
{
    public abstract class Cron : Geon, IDisposable
    {
        public float cronTime;
        public bool cronZombie;
        public override string ToString() => $"c{base.ToString()}-{cronTime}";
        public virtual void Dispose() => cronZombie = true;
    }
}