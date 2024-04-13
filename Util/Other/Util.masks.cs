public static partial class Util
{
    public static bool PullFlags<T>(this ref T mask, in object flags, in bool all = true) where T : struct
    {
        int _mask = (int)(object)mask;
        bool yes = all ? PullFlags_all(ref _mask, (int)flags) : PullFlags_any(ref _mask, (int)flags);
        mask = (T)(object)_mask;
        return yes;
    }

    public static bool PullFlags_all(this ref int mask, in int flags)
    {
        bool yes = (mask & flags) == flags;
        mask &= ~flags;
        return yes;
    }

    public static bool PullFlags_any(this ref int mask, in int flags)
    {
        bool yes = (mask & flags) > 0;
        mask &= ~flags;
        return yes;
    }

    public static bool HasFlags_all<T>(this T mask, in T flags) where T : struct => HasFlags_any((int)(object)mask, (int)(object)flags);
    public static bool HasFlags_all(this in int mask, in int flags) => (mask & flags) == flags;

    public static bool HasFlags_any<T>(this T mask, in T flags) where T : struct => HasFlags_any((int)(object)mask, (int)(object)flags);
    public static bool HasFlags_any(this in int mask, in int flags) => (mask & flags) > 0;
}