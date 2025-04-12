using _UTIL_;
using UnityEngine;

namespace _UTIL_
{
    internal class DestroyOnAwake : MonoBehaviour
    {
        private void Awake() => Destroy(gameObject);
    }
}

#if UNITY_EDITOR
partial class Util
{
    [UnityEditor.MenuItem("CONTEXT/" + nameof(Transform) + "/" + nameof(AddDestroyOnAwake))]
    static void AddDestroyOnAwake(UnityEditor.MenuCommand command) => ((Transform)command.context).gameObject.AddComponent<DestroyOnAwake>();
}
#endif