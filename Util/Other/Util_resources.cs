using UnityEngine;

public static partial class Util
{
    public static T LoadResourceByType<T>() where T : Object => Resources.Load<T>(typeof(T).FullName);
    public static T InstantiateOrCreateIfAbsent<T>() where T : MonoBehaviour
    {
        string name = typeof(T).FullName;
        T clone = Object.FindObjectOfType<T>();
        if (clone == null)
        {
            T resource = Resources.Load<T>(name);
            if (resource != null)
            {
                Debug.Log($"{nameof(Object.Instantiate)}({name})".ToSubLog());
                clone = Object.Instantiate(resource);
            }
        }
        if (clone == null)
        {
            Debug.Log($"new {name}()".ToSubLog());
            clone = new GameObject(name).AddComponent<T>();
        }
        return clone;
    }
}