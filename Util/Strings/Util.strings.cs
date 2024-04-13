using System.Collections.Generic;

public static partial class Util
{
    public static string Join(this IEnumerable<string> strings, in string separator, in string prefix = null, in string suffix = null) => prefix + string.Join(separator, strings) + suffix;
    public static string Join(this string separator, params string[] strings) => string.Join(separator, strings);

    public static string PullValue(ref string value)
    {
        string output = value;
        value = null;
        return output;
    }

    public static bool TryPullValue(ref string value, out string output)
    {
        if (value == null)
        {
            output = null;
            return false;
        }
        output = value;
        value = null;
        return true;
    }

    public static bool IsNull(this string text, in bool whitSpaces = true)
    {
        if (whitSpaces)
            return string.IsNullOrWhiteSpace(text);
        else
            return string.IsNullOrEmpty(text);
    }

    public static bool TryReadUrlParameter(this string url, in string parameter, out string value)
    {
        int start = url.IndexOf(parameter);
        if (start > 0)
        {
            int endOfValue = url.IndexOf('&', start);
            start += parameter.Length + 1;

            if (endOfValue > start)
            {
                value = url[start..endOfValue];
                return true;
            }
            else
            {
                value = string.Empty;
                return true;
            }
        }
        value = string.Empty;
        return false;
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

    public static string SetAttributes(this string text, in TextF attributes)
    {
        for (TextB tb = 0; tb < TextB._last_; ++tb)
            if (attributes.HasFlag((TextF)(1 << (int)tb)))
                text = text.SetAttribute(tb);
        return text;
    }
}