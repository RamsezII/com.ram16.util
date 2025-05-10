using System;

partial class Util
{
    public static void AddAction(ref Action instance, in Action to_add)
    {
        instance -= to_add;
        instance += to_add;
    }

    public static void AddAction<T>(ref Action<T> instance, in Action<T> to_add)
    {
        instance -= to_add;
        instance += to_add;
    }
}