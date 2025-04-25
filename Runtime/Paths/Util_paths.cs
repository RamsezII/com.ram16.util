using System;
using _UTIL_;

namespace _UTIL_
{
    public enum FS_Types : byte
    {
        File,
        Dir,
    }

    [Flags]
    public enum FS_TYPES : byte
    {
        FILE = 1 << FS_Types.File,
        DIRECTORY = 1 << FS_Types.Dir,
        BOTH = FILE | DIRECTORY,
    }
}

partial class Util
{
    public static bool HasFlags_any(this FS_TYPES mask, FS_TYPES flags) => (mask & flags) != 0;
    public static string ToLinuxPath(this string path) => path.Replace('\\', '/');
}