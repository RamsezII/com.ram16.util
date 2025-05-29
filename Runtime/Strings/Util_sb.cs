using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

partial class Util
{
    public static string TroncatedForLog(this StringBuilder sb) => sb == null || sb.Length == 0 ? string.Empty : sb.ToString()[..^1];
    public static void Log(this StringBuilder sb, in UnityEngine.Object o = null) => Debug.Log(TroncatedForLog(sb), o);

    public static string PullValue(this StringBuilder sb)
    {
        string value = sb.ToString();
        sb.Clear();
        return value;
    }

    public static string[] TextToLines(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return new string[0];
        return text.Split(new[] { "\r\n", "\r", "\n", }, StringSplitOptions.RemoveEmptyEntries);
    }

    public static object[] ExtractDataArray(this object data) => data switch
    {
        string str => str.TextToLines(),
        object[] array => array,
        IEnumerable<object> o => o.ToArray(),
        IEnumerable o => o.Cast<object>().ToArray(),
        _ => new[] { data, },
    };

    public static IEnumerable<object> IterateThroughData(this object data) => data switch
    {
        string str => str.TextToLines(),
        IEnumerable<object> o => o,
        IEnumerable o => o.Cast<object>(),
        _ => new[] { data, },
    };

    public static IEnumerable<string> IterateThroughData_str(this object data) => data switch
    {
        string str => str.TextToLines(),
        IEnumerable<object> o => o.Select(x => x?.ToString()),
        IEnumerable o => o.Cast<object>().Select(x => x?.ToString()),
        _ => new[] { data?.ToString(), },
    };
}