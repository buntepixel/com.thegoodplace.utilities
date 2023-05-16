using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGP.StateFramework {
	[CreateAssetMenu(fileName = "Tweenable", menuName = "ScriptableObjects/Tweenables/Tweenable", order = 1)]
	
	public class TweenableSO : ScriptableObject {
		[Expandable]
		public ScriptableObject Object;
		[Expandable]
		public TweenableSettingsSO Settings;
	}
}
