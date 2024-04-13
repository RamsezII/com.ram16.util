namespace _UTIL_
{
    public delegate T TFromUV<T, U, V>(in U a, in V b);
    public delegate T TFromUrVo<T, U, V>(ref U a, out V b);
}