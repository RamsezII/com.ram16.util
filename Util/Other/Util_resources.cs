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

    public static T InstantiateOrCreate<T>() where T : MonoBehaviour
    {
        string name = typeof(T).FullName;
        T resource = Resources.Load<T>(name);

        if (resource == null)
        {
            Debug.Log($"new {name}()".ToSubLog());
            return new GameObject(name).AddComponent<T>();
        }
        else
        {
            Debug.Log($"{nameof(Object.Instantiate)}({name})".ToSubLog());
            T clone = Object.Instantiate(resource);
            clone.name = name;
            return clone;
        }
    }
}