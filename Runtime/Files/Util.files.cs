using System;
using System.IO;
using UnityEngine;

public static partial class Util
{
    public static DirectoryInfo GetSreamingAssetsDir() => Application.streamingAssetsPath.ForceDir();

    //----------------------------------------------------------------------------------------------------------

    public static bool Equals_path(this string path1, string path2) => Path.GetFullPath(path1).Equals(Path.GetFullPath(path2), StringComparison.OrdinalIgnoreCase);

    public static string ForceDirPath(this string path) => path.ForceDir().FullName;
    public static DirectoryInfo ForceDir(this string path)
    {
        DirectoryInfo dir = new(path);
        if (!dir.Exists)
        {
            dir.Create();
            Debug.Log($"Created directory: \"{dir.FullName}\"".ToSubLog());
        }
        return dir;
    }

    [Obsolete] public static string TypeToExtension_OLD(this Type type) => type.FullName.Replace(".", string.Empty).Replace('+', '_');
    public static string TypeToFileName(this Type type) => type.FullName.Replace(".", string.Empty).Replace('+', '_');
    public static string TypeToExtension(this Type type) => "." + TypeToFileName(type);
    public static string TypeToPath(this Type type) => type.FullName.Replace('.', Path.PathSeparator).Replace('+', '_');
}