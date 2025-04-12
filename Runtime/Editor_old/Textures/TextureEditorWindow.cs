#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace _EDITOR_
{
    public class TextureEditorWindow : EditorWindow
    {
        Color colorModifier = Color.white;

        float
            brightness = 1f,
            contrast = 1f,
            lerp;

        //----------------------------------------------------------------------------------------------------------

        [MenuItem("Window/" + nameof(_EDITOR_) + "/" + nameof(TextureEditorWindow))]
        static void ShowWindow() => GetWindow<TextureEditorWindow>(nameof(TextureEditorWindow));

        //----------------------------------------------------------------------------------------------------------

        void OnGUI()
        {
            GUILayout.Label("Texture Editor", EditorStyles.boldLabel);

            if (Selection.activeObject is not Texture2D selectedTexture)
            {
                GUILayout.Label("No Texture2D selected");
                return;
            }

            GUILayout.Label("Selected Texture: " + selectedTexture.name);
            DrawTexturePreview(selectedTexture);

            GUILayout.Space(10);

            colorModifier = EditorGUILayout.ColorField("Color Modifier", colorModifier);
            brightness = EditorGUILayout.Slider("Brightness", brightness, 0, 2);
            contrast = EditorGUILayout.Slider("Contrast", contrast, 0, 2);
            lerp = EditorGUILayout.Slider("Lerp", contrast, 0, 1);

            GUILayout.Space(10);

            if (GUILayout.Button("Apply Color Tint"))
                ApplyColorTint(ref selectedTexture);

            if (GUILayout.Button("Adjust Brightness/Contrast"))
                AdjustBrightnessContrast(ref selectedTexture);

            if (GUILayout.Button("Save Modified Texture"))
            {
                string originalPath = AssetDatabase.GetAssetPath(selectedTexture);
                string fullPath = Path.Combine(
                    Path.GetDirectoryName(originalPath),
                    Path.GetFileNameWithoutExtension(originalPath) + "_modified.png");

                byte[] pngData = selectedTexture.EncodeToPNG();
                if (pngData != null)
                {
                    File.WriteAllBytes(fullPath, pngData);
                    AssetDatabase.Refresh();
                    Debug.Log("Texture saved to: " + fullPath);
                }
                else
                    Debug.LogError("Failed to encode texture to PNG.");
            }
        }

        void DrawTexturePreview(Texture2D texture)
        {
            float maxWidth = 200f;
            float maxHeight = 200f;

            float textureWidth = texture.width;
            float textureHeight = texture.height;

            float aspectRatio = textureWidth / textureHeight;

            float previewWidth = maxWidth;
            float previewHeight = maxHeight;

            if (aspectRatio > 1f)
                previewHeight = maxWidth / aspectRatio;
            else
                previewWidth = maxHeight * aspectRatio;

            Rect previewRect = GUILayoutUtility.GetRect(previewWidth, previewHeight, GUILayout.ExpandWidth(false));
            EditorGUI.DrawPreviewTexture(previewRect, texture);
        }

        void ApplyColorTint(ref Texture2D selectedTexture)
        {
            Texture2D modifiedTexture = new Texture2D(selectedTexture.width, selectedTexture.height);
            Color[] pixels = selectedTexture.GetPixels();

            for (int i = 0; i < pixels.Length; i++)
                pixels[i] *= colorModifier;

            modifiedTexture.SetPixels(pixels);
            modifiedTexture.Apply();

            selectedTexture = modifiedTexture;

            Debug.Log("Color tint applied.");
        }

        void AdjustBrightnessContrast(ref Texture2D selectedTexture)
        {
            Texture2D modifiedTexture = new Texture2D(selectedTexture.width, selectedTexture.height);
            Color[] pixels = selectedTexture.GetPixels();

            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] *= brightness;

                pixels[i] = new Color(
                    Mathf.Clamp01((pixels[i].r - 0.5f) * contrast + 0.5f),
                    Mathf.Clamp01((pixels[i].g - 0.5f) * contrast + 0.5f),
                    Mathf.Clamp01((pixels[i].b - 0.5f) * contrast + 0.5f),
                    pixels[i].a
                );
            }

            modifiedTexture.SetPixels(pixels);
            modifiedTexture.Apply();

            selectedTexture = modifiedTexture;

            Debug.Log("Brightness and contrast adjusted.");
        }
    }
}
#endif