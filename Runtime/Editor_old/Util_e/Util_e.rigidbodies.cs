#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

partial class Util_e
{
    [MenuItem("CONTEXT/" + nameof(Rigidbody) + "/" + nameof(_EDITOR_) + "/" + nameof(ResetRigidbody))]
    static void ResetRigidbody(MenuCommand menuCommand)
    {
        Rigidbody rigidbody = (Rigidbody)menuCommand.context;
        rigidbody.position = rigidbody.linearVelocity = rigidbody.angularVelocity = Vector3.zero;
        rigidbody.rotation = Quaternion.identity;
    }
}
#endif