using UnityEngine.UI;
using UnityEngine;

partial class Util
{
    public static void AutoSize(this LayoutGroup layoutGroup)
    {
        RectTransform rT = (RectTransform)layoutGroup.transform;
        Vector2 sizeDelta = rT.sizeDelta;
        sizeDelta.y = layoutGroup.preferredHeight;
        rT.sizeDelta = sizeDelta;
    }
}