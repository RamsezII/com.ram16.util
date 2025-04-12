#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

static partial class Util_e
{
    [MenuItem("CONTEXT/" + nameof(Transform) + "/" + nameof(_EDITOR_) + "/" + nameof(LogPrefabPath))]
    static void LogPrefabPath(MenuCommand command)
    {
        GameObject gameObject = ((Transform)command.context).gameObject;

        string path = PrefabStageUtility.GetCurrentPrefabStage()?.assetPath;

        if (string.IsNullOrWhiteSpace(path))
            path = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(gameObject);

        Debug.Log(path);
    }
}
#endif