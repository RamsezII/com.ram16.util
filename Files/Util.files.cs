using System;
using System.IO;

public static partial class Util
{
    public static readonly string
        APP_PATH = Directory.GetCurrentDirectory(),
        HOME_PATH = Path.Combine(APP_PATH, "Home");

    public readonly static DirectoryInfo APP_DIR = new(APP_PATH);
    public static DirectoryInfo HOME_DIR => HOME_PATH.GetDir(true);

    //----------------------------------------------------------------------------------------------------------

    public static long SecondsSinceLastWriteTime(this FileInfo file) => (DateTime.UtcNow - file.LastWriteTimeUtc).Ticks / TimeSpan.TicksPerSecond;

    public static DirectoryInfo GetDir(this string path, in bool force = true)
    {
        DirectoryInfo dir = new(path);
        if (!dir.Exists)
            dir.Create();
        return dir;
    }

    public static string Checked(this string path)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        return path;
    }
}