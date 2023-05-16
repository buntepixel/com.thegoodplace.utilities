using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGP.StateFramework {
	[CreateAssetMenu(fileName = "Transform", menuName = "ScriptableObjects/Tweenables/Values/Transform", order = 1)]

	public class TransformSO : ScriptableObject {
		public Vector3 Postion;
		public Vector3 Rotation;
		public Vector3 Scale;
	}
}
