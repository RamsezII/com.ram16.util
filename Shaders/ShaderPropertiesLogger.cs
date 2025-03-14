#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;
using System.Text;

namespace _UTIL_
{
    internal static class ShaderPropertiesLogger
    {
        const string button_name = "Assets/" + nameof(_UTIL_) + "/" + nameof(LogShaderProperties);

        //--------------------------------------------------------------------------------------------------------------

        [MenuItem(button_name, true)] // Afficher uniquement si un Shader ou Matériau est sélectionné
        private static bool ValidateMenu()
        {
            Object selected = Selection.activeObject;
            return selected is Shader || selected is Material;
        }

        [MenuItem(button_name)]
        private static void LogShaderProperties()
        {
            Object selected = Selection.activeObject;

            Shader shader = null;

            if (selected is Material material)
            {
                shader = material.shader;
            }
            else if (selected is Shader selectedShader)
            {
                shader = selectedShader;
            }

            if (shader == null)
            {
                Debug.LogWarning("Aucun Shader trouvé !");
                return;
            }

            StringBuilder log = new();
            log.AppendLine($"Shader: {shader.name}");

            for (int i = 0; i < shader.GetPropertyCount(); i++)
            {
                string propertyName = shader.GetPropertyName(i);
                ShaderPropertyType propertyType = shader.GetPropertyType(i);
                log.AppendLine($"[{propertyType}] {propertyName}");
            }

            Debug.Log(log.ToString()[..^1]);
        }
    }
}
#endif