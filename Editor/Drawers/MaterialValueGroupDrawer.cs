using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TGP.Utilities;

namespace TGP.Utilities.Editor {
	[CustomPropertyDrawer(typeof(MaterialValueGroup), true)]
	public class MaterialValueGroupDrawer : PropertyDrawer {

		string[] valueName = new string[] { "EndColor", "FadeEndVal", "Tiling", "Offset", "FloatParam" };
		SerializedProperty[] GetBoolValues(SerializedProperty property) {
			return new SerializedProperty[] {
				property.FindPropertyRelative("color"),
				property.FindPropertyRelative("fade"),
				property.FindPropertyRelative("tiling"),
				property.FindPropertyRelative("offset"),
				property.FindPropertyRelative("custParam")
		};
		}
	
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			EditorGUI.BeginProperty(position, label, property);

			EditorGUI.PrefixLabel(new Rect(position.x, position.y, 80, position.height), GUIUtility.GetControlID(FocusType.Passive), label);

			//save labelWidth/indent original values
			float labelWidth = EditorGUIUtility.labelWidth;
			int indent = EditorGUI.indentLevel;

			EditorGUI.indentLevel++;

			//setup bool ticboxes to show/hide other values	
			SerializedProperty[] bools = GetBoolValues(property);//as array to interate
			int[] labelWidthArr = new int[] { 50, 48, 50, 52, 85 };//array for required label widths

			float widthSize = position.width / (bools.Length + 0.5f);

			for (int i = 0; i < bools.Length; i++) {//set all bools into one line
				EditorGUIUtility.labelWidth = labelWidthArr[i];
				EditorGUI.PropertyField(new Rect(position.x + widthSize * i, position.y + EditorGUIUtility.singleLineHeight, widthSize, EditorGUI.GetPropertyHeight(bools[i])), bools[i]);
			}

			//setup required values depending on activeness of bools above.
			int counter = 2;//start with 2 since we have a label and the bools in one line for multiplying y

			EditorGUIUtility.labelWidth = 100;
			for (int i = 0; i < bools.Length; i++) {//add all other Value fields if enabled
				if (bools[i].boolValue) {
					SerializedProperty prop = property.FindPropertyRelative(valueName[i]);
					EditorGUI.PropertyField(new Rect(position.x, position.y + (EditorGUIUtility.singleLineHeight * counter), position.width, EditorGUI.GetPropertyHeight(prop)), prop, true);
					counter++;
				}
			}

			// Set labelWidth/indent back to what it was
			EditorGUIUtility.labelWidth = labelWidth;
			EditorGUI.indentLevel = indent;

			EditorGUI.EndProperty();
		}
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			float totalHeight = EditorGUIUtility.singleLineHeight;
			property.NextVisible(true);
			if (property.type == "bool" && property.boolValue == true)//workaround for first value is colorbool
				totalHeight += EditorGUIUtility.singleLineHeight;
			while (property.NextVisible(false)) {
				Debug.Log($"path: {property.displayName}");
				if (property.type != "bool") {
					totalHeight += EditorGUI.GetPropertyHeight(property, true);
				} else {
					if (!property.boolValue)
						totalHeight -= EditorGUIUtility.singleLineHeight;//since the values that dont get drawn seem to be added we need to remove a line for every false bool
				}
			}
			return totalHeight;
		}
	}
	//[CustomPropertyDrawer(typeof(FloatVal), true)]

	//public class FloatValDrawer : PropertyDrawer {
	//	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
	//		EditorGUI.BeginProperty(position, GUIContent.none, property);
	//		SerializedProperty val = property.FindPropertyRelative("FValue");
	//		SerializedProperty prop = property.FindPropertyRelative("Property");
	//		Debug.Log($"path: {property.propertyPath}");
	//		int indent = EditorGUI.indentLevel;
	//		EditorGUI.indentLevel++;

	//		property.isExpanded = EditorGUI.PropertyField(new Rect(position.x, position.y / 2, position.width, position.height), property);

	//		EditorGUI.indentLevel = indent;
	//		EditorGUI.EndProperty();
	//	}

	//}
}
