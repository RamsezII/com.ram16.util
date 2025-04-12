#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

partial class Util_e
{
    [MenuItem("CONTEXT/" + nameof(Component) + "/" + nameof(_EDITOR_) + "/" + nameof(LogType))]
    static void LogType(MenuCommand command) => Debug.Log(command.context.GetType().FullName, command.context);
}
#endif