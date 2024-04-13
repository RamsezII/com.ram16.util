using System;
using System.IO;

public static partial class Util
{
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