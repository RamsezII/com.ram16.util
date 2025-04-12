#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static partial class Util_e
{
    static RectTransform GetRect(this MenuCommand command) => (RectTransform)command.context;

    [MenuItem("CONTEXT/" + nameof(RectTransform) + "/" + nameof(_EDITOR_) + "/" + nameof(FillParent))]
    static void FillParent(MenuCommand command) => FillParent(command.GetRect());
    public static void FillParent(this RectTransform rT)
    {
        rT.anchorMin = Vector2.zero;
        rT.anchorMax = Vector2.one;
        rT.sizeDelta = Vector2.zero;
        rT.anchoredPosition = Vector2.zero;
        rT.localScale = Vector3.one;
        rT.pivot = .5f * Vector2.one;
    }
}
#endif