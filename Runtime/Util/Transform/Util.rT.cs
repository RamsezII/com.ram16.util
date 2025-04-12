using UnityEngine;

public static partial class Util
{
    public static Rect GetScreenRect(this RectTransform rectTransform, ref Vector3[] corners)
    {
        rectTransform.GetWorldCorners(corners);

        Vector2 upL = RectTransformUtility.WorldToScreenPoint(null, corners[0]);
        Vector2 downR = RectTransformUtility.WorldToScreenPoint(null, corners[2]);

        Rect rect = new(upL, downR - upL);
        rect.y = Screen.height - rect.y - rect.height;

        return rect;
    }

    public static Rect GetScreenRect_alloc(this RectTransform rectTransform)
    {
        Vector3[] corners = new Vector3[4];
        return GetScreenRect(rectTransform, ref corners);
    }
}