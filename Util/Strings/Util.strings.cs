using System.Collections.Generic;

public static partial class Util
{
    public static string ReplaceIfEmpty(this string text, in string replace) => string.IsNullOrEmpty(text) ? replace : text;
    public static string ReplaceIfNullOrWhiteSpace(this string text, in string replace) => string.IsNullOrWhiteSpace(text) ? replace : text;

    public static string Join(this IEnumerable<string> strings, in string separator, in string prefix = null, in string suffix = null) => prefix + string.Join(separator, strings) + suffix;

    public static bool IsNull(this string text, in bool whitSpaces = true)
    {
        if (whitSpaces)
            return string.IsNullOrWhiteSpace(text);
        else
            return string.IsNullOrEmpty(text);
    }

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
}