using System;
using System.IO;
using UnityEngine;

public static partial class Util
{
    public static readonly string
        app_path = Directory.GetParent(Application.dataPath).FullName,
        home_path = Path.Combine(app_path, "Home");

    public static DirectoryInfo HOME_DIR => home_path.ForceDir();
    public static DirectoryInfo STREAM_DIR => Application.streamingAssetsPath.ForceDir();

    //----------------------------------------------------------------------------------------------------------

    public static long SecondsSinceLastWriteTime(this FileInfo file) => (DateTime.UtcNow - file.LastWriteTimeUtc).Ticks / TimeSpan.TicksPerSecond;

    public static DirectoryInfo ForceDir(this string path)
    {
        DirectoryInfo dir = new(path);
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