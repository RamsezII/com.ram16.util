using System;
using System.Collections.Generic;
using System.Linq;

public static partial class Util
{
    public static string JoinEnums<T>(in T start, in T end, in string separator = " ") where T : Enum => string.Join(separator, EEnumNames(start, end));
    public static IEnumerable<string> EEnumNames<T>(in T start, in T end) where T : Enum
    {
        int a = Convert.ToByte(start);
        int b = Convert.ToByte(end);
        return Enumerable.Range(a, b - a).Select(i => ((T)Enum.ToObject(typeof(T), i)).ToString());
    }

    public static IEnumerable<T> IEnumerables<T>(params IEnumerable<T>[] enumerables)
    {
        foreach (IEnumerable<T> enumerable in enumerables)
            if (enumerable != null)
                foreach (T element in enumerable)
                    yield return element;
    }
}