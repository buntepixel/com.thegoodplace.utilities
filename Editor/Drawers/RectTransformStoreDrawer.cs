using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TGP.Utilities;

namespace TGP.Utilities.Editor {
	[CustomPropertyDrawer(typeof(RectTransformStore), true)]
	public class RectTransformStoreDrawer : PropertyDrawer {

		SerializedProperty[] GetContent(SerializedProperty property) {
			return new SerializedProperty[] {
				property.FindPropertyRelative("Position"),
				property.FindPropertyRelative("Width_Height"),
				property.FindPropertyRelative("Min"),
				property.FindPropertyRelative("Max"),
				property.FindPropertyRelative("Pivot"),
				property.FindPropertyRelative("Rotation"),
				property.FindPropertyRelative("Scale")
		};
		}
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			EditorGUI.BeginProperty(position, label, property);

			float labelWidth = EditorGUIUtility.labelWidth;
			int indent = EditorGUI.indentLevel;

			EditorGUI.indentLevel++;

			Rect foldoutRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
			property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label);
			int indentlevel = EditorGUI.indentLevel;

			if (property.isExpanded) {
				EditorGUI.indentLevel++;
				float addY = EditorGUIUtility.singleLineHeight;

				SerializedProperty[] content = GetContent(property);
				Rect foldout;
				for (int i = 0; i < content.Length; i++) {
					foldout = new(position.x, position.y + addY, position.width, EditorGUI.GetPropertyHeight(content[i]));
					addY += foldout.height;
					EditorGUI.PropertyField(foldout, content[i], true);
				}

				EditorGUI.indentLevel = indentlevel;
			}

			EditorGUI.EndProperty();
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			SerializedProperty[] content = GetContent(property);
			float height = EditorGUIUtility.singleLineHeight;
			if (property.isExpanded) {

				for (int i = 0; i < content.Length; i++)
					height += EditorGUI.GetPropertyHeight(content[i]);
			}
			return height;
		}
	}
}
