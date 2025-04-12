#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace _EDITOR_
{
    internal class ReorderableListExample : EditorWindow
    {
        private ReorderableList reorderableList;
        private List<string> items;

        [MenuItem("Tools/Reorderable List Example")]
        public static void ShowWindow()
        {
            GetWindow<ReorderableListExample>("Reorderable List Example");
        }

        private void OnEnable()
        {
            // Initialisation de la liste de données
            items = new List<string> { "Item 1", "Item 2", "Item 3" };

            // Configuration de la ReorderableList
            reorderableList = new(items, typeof(string), true, true, true, true);

            reorderableList.drawHeaderCallback += rect => EditorGUI.LabelField(rect, "Liste Réorganisable");

            reorderableList.drawElementCallback += (rect, index, isActive, isFocused) => items[index] = EditorGUI.TextField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), items[index]);

            reorderableList.onAddCallback += list => items.Add("Nouvel élément");

            reorderableList.onRemoveCallback += list =>
            {
                if (EditorUtility.DisplayDialog("Confirmer la suppression", "Supprimer cet élément ?", "Oui", "Non"))
                    items.RemoveAt(list.index);
            };
        }

        private void OnGUI()
        {
            if (reorderableList != null)
                reorderableList.DoLayoutList(); // Afficher la liste

            if (GUILayout.Button("Afficher la Liste dans la Console"))
                foreach (var item in items)
                    Debug.Log(item);
        }
    }
}
#endif