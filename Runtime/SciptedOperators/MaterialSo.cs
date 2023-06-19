using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGP.Utilities {
	[CreateAssetMenu(fileName = "Material", menuName = "ScriptableObjects/Tweenables/Values/Material", order = 2)]

	public class MaterialSo : TweenableValueSo<Material> {
		//public MaterialSo() { }

		[SerializeField]
		public MaterialValueGroup[] MaterialValues;


		//public override Sequence GetTweenSequence(Material value, TweenCallback onComplete, bool isIn = true) {
		//	Sequence sequ = DOTween.Sequence().SetAutoKill(false);

		//	if (isIn) {
		//		if (MaterialValues.color)
		//			sequ.Join(value.DOColor(MaterialValues.EndColor, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
		//		if (MaterialValues.fade)
		//			sequ.Join(value.DOFade(MaterialValues.EndFadeVal, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
		//		if (MaterialValues.tiling)
		//			sequ.Join(value.DOTiling(MaterialValues.EndTiling, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
		//		if (MaterialValues.offset)
		//			sequ.Join(value.DOOffset(MaterialValues.EndOffset, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
		//		if (MaterialValues.custParam) {
		//			foreach (FloatVal item in MaterialValues.FloatParam) {
		//				sequ.Join(value.DOFloat(item.FValue, item.Property, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
		//			}
		//		}
		//	} else {
		//		if (MaterialValues.color)
		//			sequ.Join(value.DOColor(value.color, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));

		//		if (MaterialValues.fade)
		//			sequ.Join(value.DOFade(value.color.a, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
		//		if (MaterialValues.tiling)
		//			sequ.Join(value.DOTiling(MaterialValues.EndTiling, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
		//		if (MaterialValues.offset)
		//			sequ.Join(value.DOOffset(MaterialValues.EndOffset, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
		//		if (MaterialValues.custParam) {
		//			foreach (FloatVal item in MaterialValues.FloatParam) {
		//				sequ.Join(value.DOFloat(value.GetFloat(item.Property), item.Property, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
		//			}

		//		}
		//	}
		//	return sequ.OnComplete(onComplete);
		//}
		public override Sequence GetTweenSequence(Material value, TweenCallback onComplete, bool isIn = true) {
			Sequence sequ = DOTween.Sequence().SetAutoKill(false);
			foreach (MaterialValueGroup group in MaterialValues) {
				if (isIn) {
					if (group.color)
						sequ.Join(value.DOColor(group.EndColor, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
					if (group.fade)
						sequ.Join(value.DOFade(group.EndFadeVal, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
					if (group.tiling)
						sequ.Join(value.DOTiling(group.EndTiling, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
					if (group.offset)
						sequ.Join(value.DOOffset(group.EndOffset, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
					if (group.custParam) {
						foreach (FloatVal item in group.FloatParam) {
							sequ.Join(value.DOFloat(item.FValue, item.Property, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
						}
					}
				} else {
					if (group.color)
						sequ.Join(value.DOColor(value.color, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));

					if (group.fade)
						sequ.Join(value.DOFade(value.color.a, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
					if (group.tiling)
						sequ.Join(value.DOTiling(group.EndTiling, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
					if (group.offset)
						sequ.Join(value.DOOffset(group.EndOffset, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
					if (group.custParam) {
						foreach (FloatVal item in group.FloatParam) {
							sequ.Join(value.DOFloat(value.GetFloat(item.Property), item.Property, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
						}
					}
				}
			}
			return sequ.OnComplete(onComplete);
		}


	}
	[Serializable]
	public class MaterialValueGroup {
		public MaterialValueGroup() {
			EndColor = Color.magenta;
			EndTiling = Vector2.one;
		}
		public bool color;
		public bool fade;
		public bool tiling;
		public bool offset;
		public bool custParam;

		public Color EndColor;
		public float EndFadeVal;
		public Vector2 EndTiling;
		public Vector2 EndOffset;

		public FloatVal[] FloatParam;

	}
	[Serializable]
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
