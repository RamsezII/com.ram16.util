#if UNITY_EDITOR
using UnityEngine;

partial class Util_e
{
    [UnityEditor.MenuItem("Assets/" + nameof(_EDITOR_) + "/" + nameof(CleanupEditorMemory))]
    static void CleanupEditorMemory()
    {
        Resources.UnloadUnusedAssets();
        System.GC.Collect();
        Debug.Log("🧼 Mémoire nettoyée !");
    }
}
#endif