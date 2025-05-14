﻿using System;
using System.IO;
using UnityEngine;

public static partial class Util
{
    public static DirectoryInfo GetSreamingAssetsDir() => Application.streamingAssetsPath.GetDir(true);

    //----------------------------------------------------------------------------------------------------------

    public static bool Equals_path(this string path1, string path2) => Path.GetFullPath(path1).Equals(Path.GetFullPath(path2), StringComparison.OrdinalIgnoreCase);

    public static DirectoryInfo GetDir(this string path, in bool force)
    {
        DirectoryInfo dir = new(path);
        if (force && !dir.Exists)
        {
            dir.Create();
            Debug.Log($"Created directory: \"{dir.FullName}\"".ToSubLog());
        }
        return dir;
    }

    public static string TypeToPath(this Type type) => type.FullName.Replace('.', Path.PathSeparator);
}