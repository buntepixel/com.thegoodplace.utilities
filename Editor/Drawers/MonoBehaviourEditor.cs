using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
namespace TGP.Utilities.Editor {

	[UnityEditor.CanEditMultipleObjects]
	[CustomEditor(typeof(MonoBehaviour), true)]
	public class MonoBehaviourEditor : UnityEditor.Editor {
	}
}
#endif