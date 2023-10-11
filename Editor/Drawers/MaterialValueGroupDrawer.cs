using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using TGP.Utilities;
using System.Text.RegularExpressions;

namespace TGP.Utilities.Editor {
	//[CustomPropertyDrawer(typeof(MaterialValueGroup), true)]
	public class MaterialValueGroupDrawer : PropertyDrawer {

		string[] valueName = new string[] { "EndColor", "EndFadeVal", "EndTiling", "EndOffset", "FloatParam" };
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
				SerializedProperty prop = property.FindPropertyRelative(valueName[i]);
				if (bools[i].boolValue) {
					EditorGUI.PropertyField(new Rect(position.x, position.y + (EditorGUIUtility.singleLineHeight * counter), position.width, EditorGUI.GetPropertyHeight(prop)), prop, true);
					counter++;
				} else {
					if (prop.isExpanded) {
						prop.isExpanded = false;
					}
				}
			}

			// Set labelWidth/indent back to what it was
			EditorGUIUtility.labelWidth = labelWidth;
			EditorGUI.indentLevel = indent;
			
			EditorGUI.EndProperty();
		
		}
		//public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {

		//	float totalHeight = EditorGUIUtility.singleLineHeight * 2;
		
		//	if (property == null)
		//		return 0;
		//	if (!property.isExpanded)
		//		return totalHeight;
		//	//string[] path = property.propertyPath.Split('.');
		//	//string pattern = @"[\[]+\d+[\]]";
		//	//Match match = Regex.Match(path[path.Length - 1], pattern);
		//	//if (match.Success) {

		//	//}

		//	if (property.hasChildren) {
		//		property.NextVisible(true);
		//		int counter = 0;
		//		int visible = 0;
		//		while (property.NextVisible(false)) {
		//			if (property.propertyType == SerializedPropertyType.Boolean) {
		//				counter++;
		//				if (!property.boolValue) {
		//					visible++;
		//				}
		//			}
		//			if (property.isExpanded)
		//				//Debug.Log($"hasChildren: {property.displayName}");
		//			totalHeight += EditorGUI.GetPropertyHeight(property);
		//		}
		//		totalHeight -= EditorGUIUtility.singleLineHeight * (counter + visible);

		//	}
		//	return totalHeight;

	
		//}
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
