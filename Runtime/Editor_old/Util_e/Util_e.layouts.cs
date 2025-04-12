#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static partial class Util_e
{
    [MenuItem("CONTEXT/" + nameof(VerticalLayoutGroup) + "/" + nameof(LogPreferredHeight))]
    public static void LogPreferredHeight(MenuCommand command)
    {
        VerticalLayoutGroup verticalLayoutGroup = (VerticalLayoutGroup)command.context;
        Debug.Log($"Preferred Height: {verticalLayoutGroup.preferredHeight}");
    }

    [MenuItem("CONTEXT/" + nameof(LayoutGroup) + "/" + nameof(AutoHeight))]
    public static void AutoHeight(MenuCommand command) => ((LayoutGroup)command.context).AutoSize();

    [MenuItem("CONTEXT/" + nameof(LayoutGroup) + "/" + nameof(AutoHeightParent))]
    public static void AutoHeightParent(MenuCommand command)
    {
        LayoutGroup layoutGroup = (LayoutGroup)command.context;
        RectTransform rT = (RectTransform)layoutGroup.transform.parent;

        Vector2 sizeDelta = rT.sizeDelta;
        sizeDelta.y = layoutGroup.preferredHeight;
        rT.sizeDelta = sizeDelta;
    }
}
#endif