#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

namespace _EDITOR_
{
    internal static class SpriteCombiner
    {
        [MenuItem("Assets/" + nameof(_EDITOR_) + "/" + nameof(CombineSpritesToTexture))]
        public static void CombineSpritesToTexture()
        {
            // Récupérer les objets sélectionnés dans l'inspecteur
            Object[] selectedObjects = Selection.objects;

            if (selectedObjects.Length == 0)
            {
                Debug.LogError("Aucune image sélectionnée !");
                return;
            }

            // Charger toutes les textures sélectionnées
            Texture2D[] textures = new Texture2D[selectedObjects.Length];
            for (int i = 0; i < selectedObjects.Length; i++)
            {
                string path = AssetDatabase.GetAssetPath(selectedObjects[i]);
                textures[i] = AssetDatabase.LoadAssetAtPath<Texture2D>(path);

                if (textures[i] == null)
                {
                    Debug.LogError("L'objet sélectionné n'est pas une texture !");
                    return;
                }
            }

            // Calcul de la largeur et de la hauteur de la texture finale
            int singleWidth = textures[0].width;
            int singleHeight = textures[0].height;

            int combinedWidth = singleWidth * textures.Length;
            int combinedHeight = singleHeight;

            // Créer une texture vide pour accueillir le spritesheet
            Texture2D combinedTexture = new Texture2D(combinedWidth, combinedHeight);
            for (int i = 0; i < textures.Length; i++)
            {
                // Copier chaque texture dans la texture combinée
                combinedTexture.SetPixels(i * singleWidth, 0, singleWidth, singleHeight, textures[i].GetPixels());
            }

            combinedTexture.Apply();

            // Sauvegarder la texture combinée
            string savePath = EditorUtility.SaveFilePanel("Enregistrer Spritesheet", "Assets", "CombinedTexture", "png");
            if (!string.IsNullOrEmpty(savePath))
            {
                File.WriteAllBytes(savePath, combinedTexture.EncodeToPNG());
                Debug.Log("Spritesheet créée : " + savePath);
                AssetDatabase.Refresh();
            }
            else
            {
                Debug.LogError("Opération annulée par l'utilisateur.");
            }
        }
    }
}
#endif