#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace _EDITOR_
{
    internal static class PrecompileShader
    {
        const string
            button_prefixe = "Assets/" + nameof(_EDITOR_) + "/" + nameof(PrecompileShader) + "/",
            button_selected = button_prefixe + nameof(Precompile_Selected),
            button_URP_Lit = button_prefixe + nameof(Precompile_URL_Lit),
            button_URP_SimpleLit = button_prefixe + nameof(Precompile_URP_SimpleLit);

        //--------------------------------------------------------------------------------------------------------------

        [MenuItem(button_selected, true)]
        static bool ValidateSelection_Selected() => Selection.activeObject is Shader;

        [MenuItem(button_selected)]
        static void Precompile_Selected()
        {
            Shader shader = Selection.activeObject as Shader;

            if (shader == null)
            {
                Debug.LogError("❌ Aucune sélection valide. Sélectionne un shader !");
                return;
            }

            PrecompileShaderVariants(shader);
        }

        [MenuItem(button_URP_Lit)]
        static void Precompile_URL_Lit()
        {
            Shader shader = Shader.Find("Universal Render Pipeline/Lit");
            if (shader == null)
            {
                Debug.LogError("❌ Shader 'Universal Render Pipeline/Lit' introuvable.");
                return;
            }
            PrecompileShaderVariants(shader);
        }

        [MenuItem(button_URP_SimpleLit)]
        static void Precompile_URP_SimpleLit()
        {
            Shader shader = Shader.Find("Universal Render Pipeline/Simple Lit");
            if (shader == null)
            {
                Debug.LogError("❌ Shader 'Universal Render Pipeline/Simple Lit' introuvable.");
                return;
            }
            PrecompileShaderVariants(shader);
        }

        static void PrecompileShaderVariants(in Shader shader)
        {
            Debug.Log($"🔄 Précompilation du shader : {shader.name}");

            // Force Unity à compiler toutes les variantes du shader
            ShaderVariantCollection svc = new();
            svc.Add(new ShaderVariantCollection.ShaderVariant(shader, PassType.Normal));

            svc.WarmUp();

            Debug.Log($"✅ Shader {shader.name} précompilé avec succès !");
        }
    }

}
#endif