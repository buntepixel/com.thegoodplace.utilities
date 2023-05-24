using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TGP.Utilities
{
    [CustomPropertyDrawer(typeof(EnumDataContainer<,>))]
    public class EnumDataContainerDrawer :PropertyDrawer
    {
       

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            SerializedProperty content=null;
            SerializedProperty enumType=null;
            if (content == null)
                content = property.FindPropertyRelative("content");
            if (enumType == null)
                enumType = property.FindPropertyRelative("enumType");

            float height = EditorGUIUtility.singleLineHeight;
			if (property.isExpanded) {
                if (content.arraySize != enumType.enumNames.Length)
                    content.arraySize = enumType.enumNames.Length;
                for (int i = 0; i < content.arraySize; i++)
                    height += EditorGUI.GetPropertyHeight(content.GetArrayElementAtIndex(i));
			}

            return height;
        }

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            SerializedProperty content = null;
            SerializedProperty enumType = null;
            if (content == null)
                content = property.FindPropertyRelative("content");
            if (enumType == null)
                enumType = property.FindPropertyRelative("enumType");
            EditorGUI.BeginProperty(position, label, property);
            Rect foldoutRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label);
            int indentlevel = EditorGUI.indentLevel;
			if (property.isExpanded) {
                EditorGUI.indentLevel ++;

                float addY = EditorGUIUtility.singleLineHeight;
                for(int i = 0; i < content.arraySize; i++) {
                    Rect rect = new Rect(position.x, position.y + addY, position.width, EditorGUI.GetPropertyHeight(content.GetArrayElementAtIndex(i)));
                    addY += rect.height;
                    EditorGUI.PropertyField(rect, content.GetArrayElementAtIndex(i),new GUIContent(enumType.enumNames[i]),true);
				}
                EditorGUI.indentLevel = indentlevel;
			}

            EditorGUI.EndProperty();
		}
	}
}
