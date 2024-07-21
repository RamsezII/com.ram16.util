using UnityEngine;

public static partial class Util
{
    static string sublogColor = "#BBBBBBBB";

    //--------------------------------------------------------------------------------------------------------------

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void InitColors()
    {
        Debug.Log($"{nameof(UnityEditor.EditorGUIUtility.isProSkin)}: {UnityEditor.EditorGUIUtility.isProSkin}");
        if (UnityEditor.EditorGUIUtility.isProSkin)
            sublogColor = "#CCCCCCCC";
        else
            sublogColor = "#EEEEEEEE";
    }

    //--------------------------------------------------------------------------------------------------------------

    public static string SetColor(this string text, in Colors color) => $"<color={color}>{text}</color>";
    public static string SetColor(this string text, in string value) => $"<color={value}>{text}</color>";
    public static string ToSubLog(this object o) => ToSubLog(o.ToString());
    public static string ToSubLog(this string text) => text.SetAttribute(TextB.italic).SetAttribute(TextB.color, sublogColor);
    public static string TipToLog(this string tip) => $"<i><color=#DDDDDD>tip:</color> {tip}</i>";
    public static void LogAsTip(this string tip) => Debug.Log(TipToLog(tip));
    public static string Message(this System.Exception e) => $"{e.GetType()} : \"{e.Message.TrimEnd('\n', '\r', '\t')}\"";
}