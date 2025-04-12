#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static partial class Util_e
{
    static byte _id;

    //----------------------------------------------------------------------------------------------------------

    [MenuItem("Assets/" + nameof(_EDITOR_) + "/" + nameof(LogStaticByte))]
    static void LogStaticByte() => Debug.Log(_id = ++_id > 0 ? _id : (byte)1);

    [MenuItem("Assets/" + nameof(_EDITOR_) + "/" + nameof(StopAndPlay))]
    static void StopAndPlay()
    {
        EditorApplication.isPlaying = false;
        EditorApplication.isPlaying = true;
    }

    [MenuItem("Assets/" + nameof(_EDITOR_) + "/" + nameof(ReloadDomain))]
    static void ReloadDomain() => AssetDatabase.Refresh();
}
#endif