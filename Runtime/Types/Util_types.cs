using System;

partial class Util
{
    public static bool IsOfType(this Type a, in Type b) => b.IsAssignableFrom(a);
}