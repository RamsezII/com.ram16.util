using UnityEngine;

public static partial class Util
{
    public static T LoadResourceByType<T>() where T : Object => Resources.Load<T>(typeof(T).FullName);
    public static T InstantiateOrCreateIfAbsent<T>(in FindObjectsInactive findObjectsInactive = FindObjectsInactive.Exclude) where T : MonoBehaviour
    {
        T clone = Object.FindAnyObjectByType<T>(findObjectsInactive);
        if (clone == null)
            return InstantiateOrCreate<T>();
        return clone;
    }

    public static T InstantiateOrCreate<T>(in Transform parent = null) where T : Component => (T)InstantiateOrCreate(typeof(T), parent);
    public static Component InstantiateOrCreate(in System.Type type, in Transform parent = null)
    {
        string name = type.FullName;
        Component resource = (Component)Resources.Load(name, type);
        Component clone;

        if (resource != null)
        {
            Debug.Log($"{nameof(Object.Instantiate)}({name})".ToSubLog());
            clone = Object.Instantiate(resource, parent);
            clone.name = name;
        }
        else
        {
            Debug.Log($"new {name}()".ToSubLog());
            if (parent == null)
                clone = new GameObject(name).AddComponent(type);
            else
                clone = parent.ForceFind(name).gameObject.AddComponent(type);
        }

        return clone;
    }
}