using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TGP.Utilities {
	//https://www.youtube.com/watch?v=ur-qy6SjVQw&list=PLqy--wDEnoVIxVmP_V6RXFg-tc9mVlFgX&index=6
	[Serializable]
	public class EnumDataContainer<TValue, TEnum> where TEnum : Enum {
		
		[SerializeField] private TValue[] content = null;
		[SerializeField] private TEnum enumType;

		public TValue this[int i] {
			get { return content[i]; }
		}
		public int Length {
			get { return content.Length; }
		}
	}
}
