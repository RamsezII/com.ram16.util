using System;
using System.Collections.Generic;

public static partial class Util
{
    public static IEnumerable<T> EMatchChars<T>(this string chars, IEnumerable<T> values)
    {
        foreach (var s in values)
            if (IsMatchChars(chars, s.ToString()))
                yield return s;
    }

    public static bool IsMatchChars(this string chars, in string text)
    {
        int last = 0, ic = 0, matches = 0;
        while (ic < chars.Length)
        {
            int i = text.IndexOf(chars[ic..++ic], last, text.Length - last, StringComparison.OrdinalIgnoreCase);
            if (i >= 0)
            {
                last = i + 1;
                ++matches;
            }
        }
        return matches == chars.Length;
    }
}