using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    public static bool TryAutosizeLayoutgroup(this Transform transform)
    {
        Debug.LogWarning($"{nameof(TryAutosizeLayoutgroup)} UNTESTED");

        if (!transform.TryGetComponent(out LayoutGroup lgroup))
            return false;

        RectTransform rt = (RectTransform)transform;

        switch (lgroup)
        {
            case HorizontalLayoutGroup hlgroup:
                rt.sizeDelta = new Vector2(hlgroup.preferredWidth, rt.sizeDelta.y);
                break;

            case VerticalLayoutGroup vlgroup:
                rt.sizeDelta = new Vector2(rt.sizeDelta.x, vlgroup.preferredHeight);
                break;

            default:
                return false;
        }

        return true;
    }
}