#if UNITY_EDITOR
using System.Text;
using UnityEditor;
using UnityEngine;

static partial class Util_e
{
    [MenuItem("CONTEXT/" + nameof(Transform) + "/" + nameof(_UTIL_) + "/" + nameof(LogLayerMasks))]
    static void LogLayerMasks(MenuCommand _)
    {
        StringBuilder
            masks = new("\n[System.Flags]\npublic enum LayerMasks\n{\n");

        for (byte i = 0; i != 32; ++i)
        {
            string layer = LayerMask.LayerToName(i).Replace(' ', '_');

            if (!string.IsNullOrEmpty(layer))
                masks.Append($"{layer}=1<<{i},\n");
        }

        Debug.Log(masks);
    }

    [MenuItem("CONTEXT/" + nameof(Transform) + "/" + nameof(_UTIL_) + "/" + nameof(LogLayerMasksAndBits))]
    static void LogLayerMasksAndBits(MenuCommand _)
    {
        StringBuilder
            bits = new("public enum LayerBits : byte\n{\n"),
            masks = new("\n[System.Flags]\npublic enum LayerMasks\n{\n");

        for (byte i = 0; i != 32; ++i)
        {
            string layer = LayerMask.LayerToName(i).Replace(' ', '_');

            if (!string.IsNullOrEmpty(layer))
            {
                bits.Append($"{layer}={i},\n");
                masks.Append($"{layer}=1<<LayerBits.{layer},\n");
            }
        }

        bits.Append("}\n\n");

        Debug.Log(bits.Append(masks));
    }

    [MenuItem("CONTEXT/" + nameof(Transform) + "/" + nameof(_UTIL_) + "/" + nameof(LogLayerBit))]
    static void LogLayerBit(MenuCommand command) => Debug.Log(((Transform)command.context).gameObject.layer);
}
#endif