#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class QuatToEulAttribute : PropertyAttribute
{

}

namespace _UTIL_
{
    [CustomPropertyDrawer(typeof(QuatToEulAttribute))]
    class EulQuatGUI : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => EditorGUIUtility.singleLineHeight;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) => EditorGUI.PropertyField(
            new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight),
            property,
            label);
    }
}
#endif