using System;
using System.Linq;
using System.Reflection;

partial class Util
{
    public static object ModifyAnonymous<T>(this T target, string property, object value)
    {
        Type type = target.GetType();
        PropertyInfo[] props = type.GetProperties();
        object[] values = props.Select(p => p.Name.Equals(property, StringComparison.Ordinal) ? value : p.GetValue(target)).ToArray();

        return type.GetConstructors().First().Invoke(values);
    }

    public static object GetProp(this object target, string property_name)
    {
        Type type = target.GetType();
        PropertyInfo prop = type.GetProperty(property_name);
        return prop.GetValue(target);
    }
}