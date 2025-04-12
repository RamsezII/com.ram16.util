#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace _EDITOR_
{
    internal static class URPConverter
    {
        const string button_name = "Assets/" + nameof(_EDITOR_) + "/convert selected materials to URP";

        //--------------------------------------------------------------------------------------------------------------

        [MenuItem(button_name, true)]
        static bool ValidateSelection() => Selection.activeObject is Material;

        [MenuItem(button_name)]
        static void ConvertSelected()
        {
            foreach (Object obj in Selection.objects)
                if (obj is Material material)
                    ConvertMaterialToURP(material);
        }

        static void ConvertMaterialToURP(Material material)
        {
            Shader urpShader = Shader.Find("Universal Render Pipeline/Lit");

            if (urpShader == null)
            {
                Debug.LogError("URP Shader not found! Make sure URP is installed.");
                return;
            }

            string assetPath = AssetDatabase.GetAssetPath(material);

            Material urpMaterial = new(urpShader)
            {
                color = material.color,
                mainTexture = material.mainTexture
            };
            urpMaterial.SetTexture("_BumpMap", material.GetTexture("_BumpMap"));
            urpMaterial.SetTexture("_MetallicGlossMap", material.GetTexture("_MetallicGlossMap"));

            AssetDatabase.CreateAsset(urpMaterial, assetPath);
            AssetDatabase.SaveAssets();
            Debug.Log($"Converted {urpMaterial} to URP.");
        }
    }
}
#endif