using UnityEngine;

public static partial class Util
{
    public static string SetColor(this string text, in Colors color) => $"<color={color}>{text}</color>";
    public static string SetColor(this string text, in string value) => $"<color={value}>{text}</color>";
    public static string ToSubLog(this object o) => ToSubLog(o.ToString());
    public static string ToSubLog(this string text) => text.SetAttribute(TextB.italic).SetAttribute(TextB.color, "#BBBBBBBB");
    public static string TipToLog(this string tip) => $"<i><color=#DDDDDD>tip:</color> {tip}</i>";
    public static void LogAsTip(this string tip) => Debug.Log(TipToLog(tip));
    public static string Message(this System.Exception e) => $"{e.GetType()} : \"{e.Message.TrimEnd('\n', '\r', '\t')}\"";
}