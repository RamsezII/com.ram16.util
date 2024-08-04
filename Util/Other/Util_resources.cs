using UnityEngine;

public static partial class Util
{
    public static T LoadResourceByType<T>() where T : Object
    {
        return Resources.Load<T>(typeof(T).FullName);
    }
}