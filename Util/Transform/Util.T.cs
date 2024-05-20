using UnityEngine;

public static partial class Util
{
    public static string GetPath(this Transform transform) => GetPath(transform, transform.root);
    public static string GetPath(this Transform transform, in Transform root)
    {
        string res = transform.name;

        while (transform.parent && transform.parent != root)
        {
            transform = transform.parent;
            res = transform.name + "/" + res;
        }

        return res;
    }

    public static void NormalizeChildrenScales(this Transform transform)
    {
        foreach (Transform t in transform.GetComponentsInChildren<Transform>(true))
            t.localScale = Vector3.one;
    }

    public static void CleanAll(this Transform transform)
    {
        for (int i = 0; i < transform.childCount; ++i)
            Destroy(transform.GetChild(i).gameObject);
    }

    public static void Clean(this Transform transform, in string path)
    {
        Transform T = transform.Find(path);
        if (T != null)
            Destroy(T.gameObject);
    }

    public static Transform ForceFind(this Transform parent, in string path)
    {
        Transform tfm = parent.Find(path);
        if (tfm == null)
            return ForceFind(parent, path.Split('/'));
        else
            return tfm;
    }

    static Transform ForceFind(this Transform parent, in string[] pathSplits) => ForceFind(parent, 0, pathSplits);
    static Transform ForceFind(this Transform parent, int depth, in string[] pathSplits)
    {
        Transform child = null;

        while (depth < pathSplits.Length)
        {
            child = parent.Find(pathSplits[depth]);

            if (child == null)
                while (depth < pathSplits.Length)
                {
                    child = new GameObject(pathSplits[depth]).transform;
                    child.SetParent(parent, false);
                    parent = child;
                    ++depth;
                }
            else
            {
                parent = child;
                ++depth;
            }
        }

        return child;
    }

    public static void DestroyAllByType<ComponentType>(this GameObject gameObject) where ComponentType : Component
    {
        foreach (ComponentType component in gameObject.GetComponentsInChildren<ComponentType>(true))
            Object.Destroy(component.gameObject);
    }
}