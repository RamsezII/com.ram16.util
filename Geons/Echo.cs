using System;

namespace _UTIL_
{
    public class Echo : Cron
    {
        readonly Action onEcho;
        public override void OnGeon() => onEcho();
        public Echo(in Action onEcho) => this.onEcho = onEcho;
    }
}