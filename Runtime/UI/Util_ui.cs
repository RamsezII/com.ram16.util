using UnityEngine;
using UnityEngine.EventSystems;

partial class Util
{
    static readonly Vector3[] rt_corners = new Vector3[4];

    //--------------------------------------------------------------------------------------------------------------

    public static bool IsSelectedByEventSystem(this GameObject go)
    {
        if (EventSystem.current == null)
            return false;
        if (EventSystem.current.currentSelectedGameObject == null)
            return false;
        return EventSystem.current.currentSelectedGameObject == go;
    }

    public static (Vector2 min, Vector2 max) GetWorldCorners(this RectTransform rT)
    {
        lock (rt_corners)
        {
            rT.GetWorldCorners(rt_corners);
            return (rt_corners[0], rt_corners[2]);
        }
    }

    public static Vector2 GetWorldSize(this RectTransform rT)
    {
        (Vector2 min, Vector2 max) = rT.GetWorldCorners();
        return max - min;
    }
}