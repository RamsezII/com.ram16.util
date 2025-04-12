#if UNITY_EDITOR
using _UTIL_;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

static partial class Util_e
{
    [MenuItem("CONTEXT/" + nameof(EventSystem) + "/" + nameof(LogSelected))]
    static void LogSelected(MenuCommand command)
    {
        EventSystem es = ((EventSystem)command.context);
        Debug.Log(es.currentSelectedGameObject, es);
    }

    [MenuItem("CONTEXT/" + nameof(TextMeshProUGUI) + "/" + nameof(GetTextBounds))]
    static void GetTextBounds(MenuCommand command)
    {
        Vector2 size = ((TextMeshProUGUI)command.context).textBounds.size;
        Debug.Log("(" + size.x + ", " + size.y + ")");
    }

    [MenuItem("CONTEXT/" + nameof(TextMeshProUGUI) + "/" + nameof(AutoBounds))]
    static void AutoBounds(MenuCommand command) => ((TextMeshProUGUI)command.context).AutoSize();

    [MenuItem("CONTEXT/" + nameof(TextMeshProUGUI) + "/" + nameof(AutoBoundsParent))]
    static void AutoBoundsParent(MenuCommand command)
    {
        TextMeshProUGUI tmp = ((TextMeshProUGUI)command.context);
        Vector2 size = tmp.textBounds.size;
        ((RectTransform)tmp.rectTransform.parent).sizeDelta = size;
    }

    [MenuItem("CONTEXT/" + nameof(TextMeshProUGUI) + "/" + nameof(AddTraductable))]
    static void AddTraductable(MenuCommand command) => ((TextMeshProUGUI)command.context).gameObject.AddComponent<Traductable>();

    [MenuItem("CONTEXT/" + nameof(RectTransform) + "/" + nameof(CullAllTransparentMeshes))]
    static void CullAllTransparentMeshes(MenuCommand command)
    {
        foreach (CanvasRenderer canvas in ((RectTransform)command.context).GetComponentsInChildren<CanvasRenderer>(true))
            canvas.cullTransparentMesh = true;
    }

    [MenuItem("CONTEXT/" + nameof(RectTransform) + "/" + nameof(UncullAllTransparentMeshes))]
    static void UncullAllTransparentMeshes(MenuCommand command)
    {
        foreach (CanvasRenderer canvas in ((RectTransform)command.context).GetComponentsInChildren<CanvasRenderer>(true))
            canvas.cullTransparentMesh = false;
    }

    [MenuItem("CONTEXT/" + nameof(Image) + "/" + nameof(_EDITOR_) + "/" + nameof(SetNativeSize))]
    static void SetNativeSize(MenuCommand command) => ((Image)command.context).SetNativeSize();
}
#endif