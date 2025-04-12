using _EDITOR_;
using UnityEngine;

namespace _EDITOR_
{
    internal class GizmosRay : MonoBehaviour
    {
        [SerializeField] bool destroyOnAwake = true;

        //--------------------------------------------------------------------------------------------------------------

        private void Awake()
        {
            if (destroyOnAwake)
                Destroy(this);
        }

        //--------------------------------------------------------------------------------------------------------------

        void OnDrawGizmos()
        {
            Util.DrawTransform_gizmos(transform.position, transform.rotation, 1);
        }
    }
}

#if UNITY_EDITOR
partial class Util_e
{
    [UnityEditor.MenuItem("CONTEXT/" + nameof(Transform) + "/" + nameof(Util_e) + "." + nameof(AddGizmosRay))]
    static void AddGizmosRay(UnityEditor.MenuCommand command) => ((Transform)command.context).gameObject.AddComponent<GizmosRay>();
}
#endif