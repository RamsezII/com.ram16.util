using System.Threading;

public static partial class Util
{
    public static void Lock(this object self) => Monitor.Enter(self);
    public static void Unlock(this object self) => Monitor.Exit(self);
}