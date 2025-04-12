#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

static partial class Util_e
{
    [MenuItem("CONTEXT/" + nameof(Transform) + "/" + nameof(_EDITOR_) + "/" + nameof(Log2014))]
    static void Log2014(MenuCommand command)
    {
        const string log2014 = "\n//----------------------------------------------------------------------------------------------------------\n";
        GUIUtility.systemCopyBuffer = log2014;
        Debug.Log(log2014);
    }
}
#endif