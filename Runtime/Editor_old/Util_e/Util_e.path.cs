#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

static partial class Util_e
{
    [MenuItem("CONTEXT/" + nameof(Transform) + "/" + nameof(_EDITOR_) + "/" + nameof(GetGameObjectPath))]
    static void GetGameObjectPath(MenuCommand command) => Debug.Log(AssetDatabase.GetAssetPath(((Transform)command.context).gameObject));

    [MenuItem("CONTEXT/" + nameof(Transform) + "/" + nameof(_EDITOR_) + "/" + nameof(GetPrefabPath))]
    static void GetPrefabPath(MenuCommand command) => Debug.Log(PrefabUtility.GetCorrespondingObjectFromSource(((Transform)command.context).gameObject));
}
#endif