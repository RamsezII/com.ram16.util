using UnityEngine;

namespace _EDITOR_
{
    internal class SyncBoneNames : MonoBehaviour
    {
        public Transform source;

        //--------------------------------------------------------------------------------------------------------------

        [ContextMenu(nameof(SyncNames))]
        void SyncNames() => SyncNames(source, transform);
        void SyncNames(in Transform parent_source, in Transform parent_dest)
        {
            if (parent_source == null || parent_dest == null)
            {
                Debug.LogWarning("neh");
                return;
            }

            if (parent_source.childCount == 0 || parent_dest.childCount == 0)
                return;

            for (int i = 0; i < Mathf.Min(parent_source.childCount, parent_dest.childCount); i++)
            {
                Transform child_source = parent_source.GetChild(i);
                Transform child_dest = parent_dest.GetChild(i);
                child_dest.name = child_source.name;
                SyncNames(child_source, child_dest);
            }
        }
    }
}