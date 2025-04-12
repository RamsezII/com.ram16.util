using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;

partial class Util
{
    public static IEnumerable<Type> EGetAllDerivedTypes<T>() where T : class
    {
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

        for (int i = 0; i < assemblies.Length; i++)
        {
            Assembly assembly = assemblies[i];
            foreach (Type type in assembly.GetTypes())
                if (type.IsSubclassOf(typeof(T)) && !type.IsAbstract)
                    yield return type;
        }
    }

    public static List<Type> GetAllDerivedTypes<T>() where T : class
    {
        List<Type> derivedTypes = new();
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

        for (int i = 0; i < assemblies.Length; i++)
        {
            Assembly assembly = assemblies[i];
            try
            {
                foreach (Type type in assembly.GetTypes())
                    if (type.IsSubclassOf(typeof(T)) && !type.IsAbstract)
                        derivedTypes.Add(type);
            }
            catch (ReflectionTypeLoadException e)
            {
                // Gère les exceptions si certaines assemblées ne peuvent être chargées.
                Debug.LogWarning($"ReflectionTypeLoadException: {e}");
            }
        }

        return derivedTypes;
    }

    static bool CheckType(this Type type) => type != null && !type.IsAbstract;
    static bool CheckType<T>(this Type type) where T : class => CheckType(type) && type.IsSubclassOf(typeof(T));

    public static bool TryGetType(this string typeName, out Type type)
    {
        type = Type.GetType(typeName);
        if (CheckType(type))
            return true;

        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            type = assembly.GetType(typeName);
            if (CheckType(type))
                return true;
        }

        return false;
    }

    public static Type GetTypeOrNull(this string typeName)
    {
        if (TryGetType(typeName, out Type type))
            return type;
        return null;
    }

    public static bool TryGetType<T>(this string typeName, out Type type) where T : class
    {
        type = Type.GetType(typeName);
        if (CheckType<T>(type))
            return true;

        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            type = assembly.GetType(typeName);
            if (CheckType<T>(type))
                return true;
        }

        return false;
    }
}