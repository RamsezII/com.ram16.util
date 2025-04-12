#if UNITY_EDITOR
using UnityEngine;

namespace _EDITOR_
{
    internal class CharacterCount : MonoBehaviour
    {
        [SerializeField, TextArea(10, int.MaxValue)] string text;
        [SerializeField] int count;

        //----------------------------------------------------------------------------------------------------------

        [ContextMenu(nameof(OnValidate))]
        private void OnValidate()
        {
            if (string.IsNullOrEmpty(text))
                count = 0;
            else
                count = text.Length;
        }
    }
}
#endif