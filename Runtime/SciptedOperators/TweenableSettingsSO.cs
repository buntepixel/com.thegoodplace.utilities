using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TGP.Utilities {
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
}
