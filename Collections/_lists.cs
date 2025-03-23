using System.Collections.Generic;

partial class Util
{
    public static bool ToggleElement<T>(this ICollection<T> list, in T element)
    {
        if (list.Contains(element))
        {
            list.Remove(element);
            return false;
        }
        else
        {
            list.Add(element);
            return true;
        }
    }
}