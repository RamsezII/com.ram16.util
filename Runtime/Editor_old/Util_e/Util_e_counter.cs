#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

partial class Util_e
{
    [MenuItem("Assets/" + nameof(_EDITOR_) + "/" + nameof(CountScriptsAndLinesByNamespace))]
    static void CountScriptsAndLinesByNamespace()
    {
        string[] allScripts = Directory.GetFiles(Application.dataPath, "*.cs", SearchOption.AllDirectories);

        int
            totalScripts_all = 0, totalScripts = 0,
            totalLines_all = 0, totalLines = 0;

        Dictionary<string, (int scriptCount, int lineCount)> namespaceData = new(StringComparer.OrdinalIgnoreCase);
        Regex namespaceRegex = new(@"namespace\s+(_[A-Z0-9_]+_)", RegexOptions.Compiled);

        HashSet<string> namespaceNames = new(StringComparer.OrdinalIgnoreCase);

        foreach (string scriptPath in allScripts)
        {
            int lineCount = File.ReadAllLines(scriptPath).Length;

            ++totalScripts_all;
            totalLines_all += lineCount;

            string fileContent = File.ReadAllText(scriptPath);
            Match match = namespaceRegex.Match(fileContent);

            if (match.Success)
            {
                ++totalScripts;
                totalLines += lineCount;

                string namespaceName = match.Groups[1].Value;
                namespaceNames.Add(namespaceName);

                if (!namespaceData.ContainsKey(namespaceName))
                    namespaceData[namespaceName] = (0, 0);

                namespaceData[namespaceName] = (
                    namespaceData[namespaceName].scriptCount + 1,
                    namespaceData[namespaceName].lineCount + lineCount
                );
            }
        }

        StringBuilder sb = new();
        foreach (string namespaceName in namespaceNames)
            sb.AppendLine(namespaceName);
        sb.Log();

        sb.Clear();

        sb.AppendLine("=== C# Scripts & Lines Count by _NAMESPACE_ ===");
        foreach (var pair in namespaceData.OrderByDescending(k => k.Value.scriptCount))
            sb.AppendLine($"{pair.Key}: {pair.Value.scriptCount} scripts, {pair.Value.lineCount} lines");
        sb.Log();

        sb.Clear();

        sb.AppendLine($"scripts: {totalScripts}");
        sb.AppendLine($"lines: {totalLines}");
        sb.AppendLine($"scripts (including 3rd party): {totalScripts_all}");
        sb.AppendLine($"lines (including 3rd party): {totalLines_all}");
        sb.Log();
    }
}
#endif