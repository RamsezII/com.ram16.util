using System.Collections.Generic;
using UnityEngine;

public static partial class Util
{
    public static bool TryGetResourceByName<T>(this string name, out T prefab) where T : Object
    {
        prefab = Resources.Load<T>(name);
        return prefab != null;
    }

    public static IEnumerator<T> ETryGetResourceByName<T>(this string name) where T : Object
    {
        ResourceRequest load = Resources.LoadAsync<T>(name);
        while (!load.isDone)
            yield return null;
        yield return (T)load.asset;
    }
}