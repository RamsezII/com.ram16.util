using UnityEngine;

public static partial class Util
{
    public static void WriteToClipboard(this string text) => GUIUtility.systemCopyBuffer = text;
    public static string ReadFromClipboard() => GUIUtility.systemCopyBuffer;
}