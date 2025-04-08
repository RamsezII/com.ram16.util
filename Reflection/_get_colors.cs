using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

partial class Util
{
    public static Dictionary<string, Color> GetColorProperties(this object obj)
    {
        Dictionary<string, Color> colorProperties = new(StringComparer.OrdinalIgnoreCase);
        Type type = obj.GetType();

        // Récupérer les propriétés
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (PropertyInfo property in properties)
        {
            if (property.PropertyType == typeof(Color))
            {
                Color value = (Color)property.GetValue(obj);
                colorProperties.Add(property.Name, value);
            }
        }

        // Récupérer les champs
        FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (FieldInfo field in fields)
        {
            if (field.FieldType == typeof(Color))
            {
                Color value = (Color)field.GetValue(obj);
                colorProperties.Add(field.Name, value);
            }
        }

        return colorProperties;
    }
}