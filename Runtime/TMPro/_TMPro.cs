using TMPro;
using UnityEngine;

partial class Util
{
    public static void AutoSize(this TextMeshProUGUI tmp) => tmp.rectTransform.sizeDelta = new Vector2(tmp.preferredWidth, tmp.preferredHeight);

    public static TMP_Dropdown.OptionData CurrentData(this TMP_Dropdown dropdown) => dropdown.options[dropdown.value];
    public static string CurrentText(this TMP_Dropdown dropdown) => CurrentData(dropdown).text;
}