using System;
using System.IO;
using UnityEngine;

public static partial class Util
{
    public static readonly string
        app_path = Directory.GetCurrentDirectory(),
        home_path = Path.Combine(app_path, "Home");

    public static DirectoryInfo HOME_DIR => home_path.GetDir(true);

    //----------------------------------------------------------------------------------------------------------

    public static long SecondsSinceLastWriteTime(this FileInfo file) => (DateTime.UtcNow - file.LastWriteTimeUtc).Ticks / TimeSpan.TicksPerSecond;

    public static DirectoryInfo GetDir(this string path, in bool force = true)
    {
        DirectoryInfo dir = new(path);
        if (force && !dir.Exists)
        {
            dir.Create();
            Debug.Log($"Created directory at: \"{dir.FullName}\"".ToSubLog());
        }
        return dir;
    }

    public static string Checked(this string path)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        return path;
    }
}