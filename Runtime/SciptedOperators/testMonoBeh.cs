using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGP.Utilities {
	public class testMonoBeh : MonoBehaviour {

		[SerializeField]
		EnumDataContainer<CustomClass, TextStyle>[] Container;
		[SerializeField]
		MaterialValueGroup[] group;
	}
	[System.Serializable]
	public class CustomClass {

		public int Size;
		
		public Color color;

	}
	public enum TextStyle{
		hello,
		there,
		isa,
		shitLoad
	}
}
