using UnityEngine;

public static partial class Util
{
    public static T LoadResourceByType<T>() where T : Object => Resources.Load<T>(typeof(T).FullName);
    public static T InstantiateOrCreateIfAbsent<T>(in bool includeInactive = false) where T : MonoBehaviour
    {
        T clone = Object.FindObjectOfType<T>(includeInactive);
        if (clone == null)
            return InstantiateOrCreate<T>();
        return clone;
    }

    public static T InstantiateOrCreate<T>(in Transform parent = null) where T : MonoBehaviour
    {
        string name = typeof(T).FullName;
        T resource = Resources.Load<T>(name);
        T clone;

        if (resource == null)
        {
            Debug.Log($"new {name}()".ToSubLog());
            if (parent == null)
                clone = new GameObject(name).AddComponent<T>();
            else
                clone = parent.ForceFind(name).gameObject.AddComponent<T>();
        }
        else
        {
            Debug.Log($"{nameof(Object.Instantiate)}({name})".ToSubLog());
            clone = Object.Instantiate(resource, parent);
            clone.name = name;
        }

        return clone;
    }
}