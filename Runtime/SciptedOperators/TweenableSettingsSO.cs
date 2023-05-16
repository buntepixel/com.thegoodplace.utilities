using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TGP.StateFramework {
	[CreateAssetMenu(fileName = "TweenableSettings", menuName = "ScriptableObjects/Tweenables/Settings", order = 1)]
	public class TweenableSettingsSO : ScriptableObject {
		//public Settings Settings;
		public Ease InEaseCurve = Ease.InCubic;
		public Ease OutEaseCurve = Ease.OutCubic;
		public float InDuration = 1;
		public float OutDuration = 1;
		public float InDelay = 0;
		public float OutDelay = 0;
	}
	[Serializable]
	public class Settings {
	
		public Ease InEaseCurve = Ease.InCubic;
		public Ease OutEaseCurve = Ease.OutCubic;
		public float InDuration = 1;
		public float OutDuration = 1;
		public float InDelay = 0;
		public float OutDelay = 0;
		//public Settings(Ease inCurve, float inDur, float inDelay, Ease outCurve, float outDur, float outDelay) {
		//	InEaseCurve = inCurve;
		//	InDuration = inDur;
		//	InDelay = inDelay;
		//	OutEaseCurve = outCurve;
		//	OutDuration = outDur;
		//	OutDelay = outDelay;
		//}
	}
}
