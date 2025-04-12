using TMPro;
using UnityEngine;

partial class Util
{
    public static void AutoSize(this TextMeshProUGUI tmp) => tmp.rectTransform.sizeDelta = new Vector2(tmp.preferredWidth, tmp.preferredHeight);

    public static void AutoSize_old(this TextMeshProUGUI tmp)
    {
        tmp.rectTransform.sizeDelta = tmp.textBounds.size;
    }
}