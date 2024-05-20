using System.Collections.Generic;

public static partial class Util
{
    public static string LongestPrefixe(this IEnumerable<string> strings)
    {
        string pref = null;
        foreach (var s in strings)
            if (pref == null)
                pref = s;
            else
            {
                int i = 0;
                while (i < s.Length && i < pref.Length)
                {
                    if (pref[i] != s[i])
                        break;
                    ++i;
                }
                if (i == 0)
                    return string.Empty;
                else
                    pref = s[..i];
            }
        return pref;
    }

    public static string LongestSuffixe(this IEnumerable<string> strings)
    {
        string suff = null;
        foreach (var s in strings)
            if (suff == null)
                suff = s;
            else
            {
                int i = 0;
                while (i < s.Length && i < suff.Length)
                {
                    ++i;
                    if (suff[^i] != s[^i])
                    {
                        --i;
                        break;
                    }
                }
                if (i == 0)
                    return string.Empty;
                else
                    suff = s[^i..];
            }
        return suff;
    }
}