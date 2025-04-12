using UnityEngine;
using UnityEngine.EventSystems;

partial class Util
{
    public static bool IsSelectedByEventSystem(this GameObject go)
    {
        if (EventSystem.current == null)
            return false;
        if (EventSystem.current.currentSelectedGameObject == null)
            return false;
        return EventSystem.current.currentSelectedGameObject == go;
    }
}