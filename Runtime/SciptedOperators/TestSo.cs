using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGP.Utilities {
	[CreateAssetMenu(fileName = "test", menuName = "ScriptableObjects/Tweenables/Values/test", order = 1)]

	public class TestSo : ScriptableObject {
	
		public FloatVal[] FloatVal;
	}
	[System.Serializable]
	public class FloatVal {

		public float FValue;
		public string Property;
		FloatVal() { }
		FloatVal(float value, string property) {
			FValue = value;
			Property = property;
		}
	}
}
