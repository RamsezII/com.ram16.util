using UnityEngine;

public static partial class Util
{
    public static void WriteToClipboard(this string text)
    {
        GUIUtility.systemCopyBuffer = text;
        Debug.Log($"(clipboard) \"{text}\"".ToSubLog());
    }

    public static string ReadFromClipboard() => GUIUtility.systemCopyBuffer;
}