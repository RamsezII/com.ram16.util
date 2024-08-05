using UnityEngine;

public static partial class Util
{
    public static T LoadResourceByType<T>() where T : Object
    {
        return Resources.Load<T>(typeof(T).FullName);
    }

    public static T InstantiateOrCreate<T>() where T : MonoBehaviour
    {
        string name = typeof(T).FullName;
        T clone = Object.FindObjectOfType<T>();
        if (clone == null)
        {
            T resource = Resources.Load<T>(name);
            if (resource != null)
                clone = Object.Instantiate(resource);
        }
        if (clone == null)
            clone = new GameObject(name).AddComponent<T>();
        return clone;
    }
}