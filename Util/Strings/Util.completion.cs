using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public static partial class Util
{
    public static IEnumerable<string> Matches1(this IEnumerable<string> completions, string prefix) => completions.Where(completion => completion.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));

    public static IEnumerable<string> Matches2(this IEnumerable<string> completions, string prefix)
    {
        foreach (string completion in completions)
            if (completion.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                yield return completion;
            else
            {
                string p = prefix;

                char pc;
                int ci = 0;
                while (!p.IsNull())
                {
                    pc = char.ToLower(p[0]);
                    char cc = char.ToLower(completion[ci]);

                    if (pc == cc)
                        p = p[1..];

                    if (++ci >= completion.Length)
                        break;
                }

                if (p.IsNull())
                    yield return completion;
            }
    }

    public static string LonguestCommonPrefixe(this IEnumerable<string> strings)
    {
        if (!strings.Any())
            return string.Empty;

        strings = strings.OrderBy(word => word, StringComparer.OrdinalIgnoreCase);
        string first = strings.First();
        string last = strings.Last();
        int length = Mathf.Min(first.Length, last.Length);

        for (int i = 0; i < length; i++)
            if (first[i] != last[i])
                return first[..i];
        return first[..length];
    }

    public static bool TryComplete(this string input, in IEnumerable<string> values, out string completion)
    {
        completion = LonguestCommonPrefixe(values.ToArray());
        return !string.IsNullOrWhiteSpace(completion) && !completion.Equals(input, StringComparison.OrdinalIgnoreCase);
    }
}
