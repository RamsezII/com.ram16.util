#if UNITY_EDITOR
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public static partial class Util_e
{
    [MenuItem("Assets/" + nameof(_EDITOR_) + "/" + nameof(LogTextB))]
    static void LogTextB()
    {
        StringBuilder log = new();
        foreach (string code in from s in from i in Enumerable.Range(0, (int)TextB._last_) select ((TextB)i).ToString() orderby s select s)
            log.AppendLine(code + ",");
        Debug.Log(log);
    }

    [MenuItem("Assets/" + nameof(_EDITOR_) + "/" + nameof(LogTextF))]
    static void LogTextF()
    {
        StringBuilder log = new();
        for (TextB tb = 0; tb < TextB._last_; ++tb)
            log.AppendLine($"{tb} = 1 << {nameof(TextB)}.{tb},");
        Debug.Log(log);
    }
}
#endif