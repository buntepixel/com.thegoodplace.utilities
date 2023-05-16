using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TGP.StateFramework.EditorS {
	//https://forum.unity.com/threads/editor-tool-better-scriptableobject-inspector-editing.484393/

	[CanEditMultipleObjects]
	[CustomEditor(typeof(UnityEngine.Object), true)]
	public class MonoBehaviourEditor : UnityEditor.Editor {
	}
}




