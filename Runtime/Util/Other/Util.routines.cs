using System;
using System.Collections;
using System.Threading.Tasks;

public static partial class Util
{
    public static IEnumerator EWait(this Func<bool> predicate)
    {
        while (!predicate())
            yield return null;
    }

    public static IEnumerator EWait(this Task task)
    {
        while (!task.IsCompleted)
            yield return null;
    }
}