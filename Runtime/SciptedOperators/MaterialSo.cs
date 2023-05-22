using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGP.Utilities {
	[CreateAssetMenu(fileName = "Material", menuName = "ScriptableObjects/Tweenables/Values/Material", order = 2)]

	public class MaterialSo : TweenableValueSo<Material> {
		public MaterialSo() { }
		//public Material Material;
		public Color EndColor = Color.magenta;
		public float FadeEndVal = 1;
		public Vector2 Tiling = Vector2.one;
		public Vector2 Offset;

		public FloatVal[] FloatParam;
		public bool color;
		public bool fade;
		public bool tiling;
		public bool offset;
		public bool customParam;


		public override Sequence GetTweenSequence(Material value, TweenCallback onComplete, bool isIn = true) {
			Sequence sequ = DOTween.Sequence().SetAutoKill(false);
			if (isIn) {
				if (color)
					sequ.Join(value.DOColor(EndColor, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
				if (fade)
					sequ.Join(value.DOFade(FadeEndVal, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
				if (tiling)
					sequ.Join(value.DOTiling(Tiling, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
				if (offset)
					sequ.Join(value.DOOffset(Offset, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
				if (customParam) {
					foreach (FloatVal item in FloatParam) {
						sequ.Join(value.DOFloat(item.FValue, item.Property, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
					}
				}
			} else {
				if (color)
					sequ.Join(value.DOColor(value.color, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));

				if (fade)
					sequ.Join(value.DOFade(value.color.a, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
				if (tiling)
					sequ.Join(value.DOTiling(Tiling, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
				if (offset)
					sequ.Join(value.DOOffset(Offset, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
				if (customParam) {
					foreach (FloatVal item in FloatParam) {
						sequ.Join(value.DOFloat(value.GetFloat(item.Property), item.Property, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
					}
				}
			}
			return sequ.OnComplete(onComplete);
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
}
