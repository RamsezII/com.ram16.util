#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace _EDITOR_
{
    internal static class AnimationSplitter
    {
        [MenuItem("Assets/" + nameof(_EDITOR_) + "/" + nameof(SplitFirstAnimationClip))]
        static void SplitFirstAnimationClip()
        {
            // Sélectionne l'objet avec l'animation à modifier
            GameObject selectedObject = Selection.activeObject as GameObject;
            if (selectedObject == null)
            {
                Debug.LogError("Aucun objet sélectionné. Sélectionne un GameObject avec une animation !");
                return;
            }

            string path = AssetDatabase.GetAssetPath(selectedObject);
            ModelImporter importer = AssetImporter.GetAtPath(path) as ModelImporter;

            if (importer == null)
            {
                Debug.LogError("Impossible de récupérer le ModelImporter pour cet objet.");
                return;
            }

            // Définir les plages de frames pour chaque clip
            List<ModelImporterClipAnimation> animationClips = new();

            ModelImporterClipAnimation clip0 = importer.clipAnimations[0];
            int index = 0;
            float frame_f = clip0.firstFrame;
            while (frame_f < clip0.lastFrame)
            {
                float frame_l = Mathf.Min(frame_f + 1000, clip0.lastFrame);
                animationClips.Add(new()
                {
                    name = (++index).ToString(),
                    firstFrame = frame_f,
                    lastFrame = frame_l,
                });
                frame_f = frame_l;
            }

            // Appliquer les clips d'animation
            importer.clipAnimations = animationClips.ToArray();
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);

            Debug.Log("Les animations ont été séparées avec succès !");
        }
    }
}
#endif