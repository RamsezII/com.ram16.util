#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

partial class Util_e
{
    public static string GetSelectedFolderPath()
    {
        foreach (Object obj in Selection.objects)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            if (AssetDatabase.IsValidFolder(path))
                return path;
        }
        return null;
    }
}
#endif