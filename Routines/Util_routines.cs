using System;
using System.Collections.Generic;

public static partial class Util
{
    public static IEnumerator<T> ExtractCallback<T>(this IEnumerator<T> eT, Action<T> onT)
    {
        while (eT.MoveNext())
            yield return eT.Current;
        onT(eT.Current);
    }
}