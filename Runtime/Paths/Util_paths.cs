partial class Util
{
    public static string ToLinuxPath(this string path) => path.Replace('\\', '/');
}