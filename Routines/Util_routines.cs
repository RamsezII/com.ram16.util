using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Util
{
    public static IEnumerator<T> ExtractCallback<T>(this IEnumerator<T> eT, Action<T> onT)
    {
        while (eT.MoveNext())
            yield return eT.Current;
        onT(eT.Current);
    }

    public static IEnumerator EWaitForFrames(this int frames, Action action)
    {
        for (int i = 0; i < frames; i++)
            yield return null;
        action();
    }

    public static IEnumerator EWaitForSeconds(this float seconds, bool scaled, Action action)
    {
        while (seconds > 0)
        {
            if (scaled)
                seconds -= Time.deltaTime;
            else
                seconds -= Time.unscaledDeltaTime;
            yield return null;
        }
        action();
    }
}