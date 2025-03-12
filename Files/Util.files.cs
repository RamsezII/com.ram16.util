using System;
using System.IO;
using UnityEngine;

public static partial class Util
{
    public static DirectoryInfo GetSreamingAssetsDir() => Application.streamingAssetsPath.ForceDir();

    //----------------------------------------------------------------------------------------------------------

    public static bool Equals_path(this string path1, string path2) => Path.GetFullPath(path1).Equals(Path.GetFullPath(path2), StringComparison.OrdinalIgnoreCase);

    public static long SecondsSinceLastWriteTime(this FileInfo file) => (DateTime.UtcNow - file.LastWriteTimeUtc).Ticks / TimeSpan.TicksPerSecond;

    public static DirectoryInfo ForceDir(this string path, in bool force = true)
    {
        DirectoryInfo dir = new(path);
        if (force)
            if (!dir.Exists)
            {
                dir.Create();
                Debug.Log($"Created directory at: \"{dir.FullName}\"".ToSubLog());
            }
        return dir;
    }

    [Obsolete] public static string TypeToExtension_OLD(this Type type) => type.FullName.Replace(".", string.Empty).Replace('+', '_');
    public static string TypeToExtension(this Type type) => "." + type.FullName.Replace(".", string.Empty).Replace('+', '_');
    public static string TypeToPath(this Type type) => type.FullName.Replace('.', Path.PathSeparator).Replace('+', '_');
}