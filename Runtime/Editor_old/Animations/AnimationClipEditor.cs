//#if UNITY_EDITOR
//using System.Collections.Generic;
//using System.Linq;
//using UnityEditor;
//using UnityEditorInternal;
//using UnityEngine;

//namespace _EDITOR_
//{
//    public class AnimationClipEditor : EditorWindow
//    {
//        ReorderableList reorderableList;
//        List<ModelImporterClipAnimation> clipAnimations;

//        //--------------------------------------------------------------------------------------------------------------

//        [MenuItem("Assets/" + nameof(_EDITOR_) + "/" + nameof(ShowAnimationClipEditor))]
//        public static void ShowAnimationClipEditor()
//        {
//            GetWindow<AnimationClipEditor>(typeof(AnimationClipEditor).FullName);
//        }

//        //--------------------------------------------------------------------------------------------------------------

//        private void OnEnable()
//        {
//            ReloadAnimations();

//            reorderableList = new(clipAnimations, typeof(string), true, true, true, true);

//            reorderableList.drawHeaderCallback += rect => EditorGUI.LabelField(rect, "Liste Réorganisable");

//            reorderableList.drawElementCallback += (rect, index, isActive, isFocused) => clipAnimations[index] = EditorGUI.TextField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), clipAnimations[index]);

//            reorderableList.onAddCallback += list => clipAnimations.Add("Nouvel élément");

//            reorderableList.onRemoveCallback += list =>
//            {
//                if (EditorUtility.DisplayDialog("Confirmer la suppression", "Supprimer cet élément ?", "Oui", "Non"))
//                    clipAnimations.RemoveAt(list.index);
//            };
//        }

//        //--------------------------------------------------------------------------------------------------------------

//        private void OnGUI()
//        {
//            GUILayout.Label(typeof(AnimationClipEditor).FullName, EditorStyles.boldLabel);

//            if (GUILayout.Button(nameof(ReloadAnimations)))
//                ReloadAnimations();

//            if (clipAnimations != null && clipAnimations.Count > 0)
//            {
//                GUILayout.Label($"Nombre d'animations : {clipAnimations.Count}", EditorStyles.boldLabel);

//                if (reorderableList != null)
//                    reorderableList.DoLayoutList();

//                if (GUILayout.Button("Appliquer les Modifications"))
//                    ApplyAnimations();
//            }
//        }

//        void ReloadAnimations()
//        {
//            string path = AssetDatabase.GetAssetPath(Selection.activeGameObject);
//            ModelImporter importer = AssetImporter.GetAtPath(path) as ModelImporter;

//            if (importer != null)
//            {
//                clipAnimations = importer.defaultClipAnimations.ToList();
//                Debug.Log($"Chargé {clipAnimations.Count} clips d'animations.");
//            }
//            else
//                Debug.LogError("Le modèle sélectionné n'a pas de ModelImporter.");
//        }

//        void ApplyAnimations()
//        {
//            if (clipAnimations == null)
//            {
//                Debug.LogError("Aucune animation à appliquer.");
//                return;
//            }

//            string path = AssetDatabase.GetAssetPath(Selection.activeGameObject);
//            ModelImporter importer = AssetImporter.GetAtPath(path) as ModelImporter;

//            if (importer != null)
//            {
//                importer.clipAnimations = clipAnimations.ToArray();
//                AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
//                Debug.Log("Modifications appliquées avec succès !");
//            }
//            else
//                Debug.LogError("Erreur lors de l'application des animations.");
//        }
//    }
//}
//#endif