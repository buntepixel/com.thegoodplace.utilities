using DG.Tweening;
using log4net.Util;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace TGP.Utilities {
	[CreateAssetMenu(fileName = "TransformUi", menuName = "ScriptableObjects/Tweenables/Values/Float", order = 3)]

	public class TransformFloatSO : TweenableValueSo<float> {
		public float targetValue;
		public override Sequence GetTweenSequence(float value, TweenCallback onComplete, bool isIn = true) {
			Sequence sequ = DOTween.Sequence().SetAutoKill(false);
			if (isIn)
				sequ.Join(DOTween.To(() => value, x => value = x, value, Settings.InDuration)).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve);
			else
				sequ.Join(DOTween.To(() => value, x => value = x, targetValue, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
			sequ.AppendCallback(onComplete);
			return sequ.Pause();
		}
	}
}
