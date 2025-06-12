using System;
using System.Collections.Generic;

public static partial class Util
{
    public static string PullValue(ref string value)
    {
        string temp = value;
        value = temp;
        return temp;
    }

    public static void TrimNewline(ref string text)
    {
        if (text.EndsWith('\n'))
            text = text[..^1];
    }

    public static string ReplaceIfNullOrWhiteSpace(this string text, in string replace) => string.IsNullOrWhiteSpace(text) ? replace : text;

    public static string Join(this IEnumerable<string> strings, in string separator, in string prefix = null, in string suffix = null) => prefix + string.Join(separator, strings) + suffix;

    public static string SetAttribute(this string text, in TextB attribute)
    {
        string attr = Util_richtext.rtextAttr[(int)attribute];
        return $"<{attr}>{text}</{attr}>";
    }

    public static string SetAttribute(this string text, in TextB attribute, in string value)
    {
        string attr = Util_richtext.rtextAttr[(int)attribute];
        return $"<{attr}={value}>{text}</{attr}>";
    }

    public static string Mirror(this string text)
    {
        char[] chars = text.ToCharArray();
        Array.Reverse(chars);
        return new string(chars);
    }
}