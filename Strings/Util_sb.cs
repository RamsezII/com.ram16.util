using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

partial class Util
{
    public static string TroncatedForLog(this StringBuilder sb) => sb == null || sb.Length == 0 ? string.Empty : sb.ToString().TrimEnd('\n', '\r');
    public static void Log(this StringBuilder sb, in Object o = null) => Debug.Log(TroncatedForLog(sb), o);
    public static void LogAndClear(this StringBuilder sb, in Object o = null)
    {
        Log(sb, o);
        sb.Clear();
    }

    public static string PullValue(this StringBuilder sb)
    {
        string value = sb.ToString();
        sb.Clear();
        return value;
    }

    public static string LinesToText(this IEnumerable<object> objects, in bool removeEmptyEntries = false)
    {
        if (objects == null)
            return string.Empty;
        StringBuilder sb = new();
        foreach (object o in objects)
        {
            string line = o switch
            {
                string str => str,
                _ => o.ToString()
            };
            if (!removeEmptyEntries || !string.IsNullOrWhiteSpace(line))
                sb.AppendLine(line);
        }

        return sb.TroncatedForLog();
    }

    public static List<string> TextToLines(this string text, in bool removeEmptyEntries)
    {
        if (string.IsNullOrWhiteSpace(text))
            return new();
        return text.Split(new[] { '\n', '\r' }, removeEmptyEntries ? System.StringSplitOptions.RemoveEmptyEntries : System.StringSplitOptions.None).ToList();
    }
}