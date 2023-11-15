using DG.Tweening;
using log4net.Util;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace TGP.Utilities {
	[CreateAssetMenu(fileName = "Float", menuName = "ScriptableObjects/Tweenables/CanvasGroup", order = 3)]

	public class TransformCanvasGroupSO : TweenableValueSo2<CanvasGroup,float> {
		TransformCanvasGroupSO() { }

		public override Sequence GetTweenSequenceIn(CanvasGroup property, float value, TweenCallback onComplete) {
			Sequence sequ = DOTween.Sequence().SetAutoKill(false);
			sequ.Join(property.DOFade(value,Settings.InDuration)).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve).OnComplete(onComplete);
			return sequ.Pause();
		}

		

		public override Sequence GetTweenSequenceOut(CanvasGroup property, float value, TweenCallback onComplete) {
			Sequence sequ = DOTween.Sequence().SetAutoKill(false);
			sequ.Join(property.DOFade(value, Settings.OutDuration)).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve).OnComplete(onComplete);
			return sequ.Pause();
		}

	}
}
