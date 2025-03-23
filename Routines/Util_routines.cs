using System;
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

    public static IEnumerator<float> EWaitForFrames(this int frames, Action action)
    {
        for (int i = 0; i < frames; i++)
            yield return (float)i / frames;
        action();
    }

    public static IEnumerator<float> EWaitForSeconds(this float seconds, bool scaled, Action action)
    {
        float timer = seconds;
        while (timer > 0)
        {
            if (scaled)
                timer -= Time.deltaTime;
            else
                timer -= Time.unscaledDeltaTime;
            yield return 1 - timer / seconds;
        }
        action?.Invoke();
    }

    public static IEnumerator<float> EWaitForCondition(this Func<bool> condition, Func<float> progression, Action action)
    {
        while (!condition())
            if (progression == null)
                yield return 0;
            else
                yield return progression();
        action();
    }
}