using System;
using UnityEngine;

public static partial class Util
{
    public static string GetPath(this Transform transform, in bool includeRoot)
    {
        string res = transform.name;

        while (transform.parent && (includeRoot || (transform.parent != transform.root)))
        {
            transform = transform.parent;
            res = transform.name + "/" + res;
        }

        return res;
    }

    public static string GetRelativePath(this Transform transform, in Transform root)
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

    public static bool TryFindTransform(this string path, out Transform transform)
    {
        string[] splits = path.Split('/');

        GameObject root = GameObject.Find(splits[0]);
        if (root == null)
        {
            transform = null;
            return false;
        }
        else
            transform = root.transform;

        if (splits.Length == 1)
            return true;

        transform = transform.Find(string.Join('/', splits[1..]));

        return transform != null;
    }

    public static Transform ForceFindTransform(this string path)
    {
        string[] splits = path.Split('/');
        Transform root;

        GameObject go = GameObject.Find(splits[0]);
        if (go == null)
            root = new GameObject(splits[0]).transform;
        else
            root = go.transform;

        if (splits.Length == 1)
            return root;

        splits = splits[1..];
        return ForceFind(root, splits);
    }

    public static Transform ForceFind(this Transform root, in string path) => ForceFind(root, path.Split('/'));
    static Transform ForceFind(this Transform root, params string[] splits)
    {
        Transform tfm = root.Find(splits[0]);

        if (tfm == null)
        {
            tfm = new GameObject(splits[0]).transform;
            tfm.SetParent(root, false);
        }

        if (splits.Length == 1)
            return tfm;
        else
            return ForceFind(tfm, splits[1..]);
    }

    public static bool TryFindByName(this Transform root, in string name, out Transform transform, in StringComparison stringComparison = StringComparison.Ordinal)
    {
        foreach (Transform t in root.GetComponentsInChildren<Transform>(true))
            if (t.name.Equals(name, stringComparison))
            {
                transform = t;
                return true;
            }
        transform = null;
        return false;
    }

    public static Transform FindByName(this Transform root, in string name, in StringComparison stringComparison = StringComparison.Ordinal)
    {
        foreach (Transform t in root.GetComponentsInChildren<Transform>(true))
            if (t.name.Equals(name, stringComparison))
                return t;
        return null;
    }

    public static void RemoveChildren(this Transform root)
    {
        for (int i = 0; i < root.childCount; ++i)
            UnityEngine.Object.Destroy(root.GetChild(i).gameObject);
    }

    public static void RemoveChildrenImmediate(this Transform root)
    {
        for (int i = 0; i < root.childCount; ++i)
        {
            Transform transform = root.GetChild(i);
            Debug.Log($"Destroying {transform.name} from {root.name} ({transform.GetPath(true)})");
            UnityEngine.Object.DestroyImmediate(transform.gameObject);
        }
    }

    public static void DestroyAllByType<ComponentType>(this GameObject gameObject) where ComponentType : Component
    {
        foreach (ComponentType component in gameObject.GetComponentsInChildren<ComponentType>(true))
            UnityEngine.Object.Destroy(component.gameObject);
    }

    public static Transform CreateParent(this Transform transform, in string name)
    {
        Transform parent = new GameObject(name).transform;
        parent.SetParent(transform.parent, false);
        parent.localPosition = transform.localPosition;
        parent.localRotation = transform.localRotation;
        parent.localScale = transform.localScale;
        transform.SetParent(parent, false);
        return parent;
    }
}