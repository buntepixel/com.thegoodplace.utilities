using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGP.Utilities {
	[CreateAssetMenu(fileName = "Material", menuName = "ScriptableObjects/Tweenables/Values/Material", order = 2)]

	public class MaterialSo : TweenableValueSo<Material> {
		public MaterialSo() { }
		
		[SerializeField]
		public MaterialValueGroup Mv;

		//public MaterialValueGroup[] MaterialValues;
		//public Material Material;
		//public color endcolor = color.magenta;
		//public float fadeendval = 1;
		//public vector2 tiling = vector2.one;
		//public vector2 offset;

		//public floatval[] floatparam;

		//public FloatVal[] FloatParam;
		//public bool color;
		//public bool fade;
		//public bool tiling;
		//public bool offset;
		//public bool customParam;
		public override Sequence GetTweenSequence(Material value, TweenCallback onComplete, bool isIn = true) {
			Sequence sequ = DOTween.Sequence().SetAutoKill(false);
			
				if (isIn) {
					if (Mv.color)
						sequ.Join(value.DOColor(Mv.EndColor, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
					if (Mv.fade)
						sequ.Join(value.DOFade(Mv.FadeEndVal, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
					if (Mv.tiling)
						sequ.Join(value.DOTiling(Mv.Tiling, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
					if (Mv.offset)
						sequ.Join(value.DOOffset(Mv.Offset, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
					//if (Mv.custParam) {
					//	foreach (FloatVal item in Mv.FloatParam) {
					//		sequ.Join(value.DOFloat(item.FValue, item.Property, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
					//	}
					//}
				} else {
					if (Mv.color)
						sequ.Join(value.DOColor(value.color, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));

					if (Mv.fade)
						sequ.Join(value.DOFade(value.color.a, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
					if (Mv.tiling)
						sequ.Join(value.DOTiling(Mv.Tiling, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
					if (Mv.offset)
						sequ.Join(value.DOOffset(Mv.Offset, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
					//if (Mv.custParam) {
					//	foreach (FloatVal item in Mv.FloatParam) {
					//		sequ.Join(value.DOFloat(value.GetFloat(item.Property), item.Property, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
					//	}
					
				//}
			}
			return sequ.OnComplete(onComplete);
		}
		//public override Sequence GetTweenSequence(Material value, TweenCallback onComplete, bool isIn = true) {
		//	Sequence sequ = DOTween.Sequence().SetAutoKill(false);
		//	foreach (MaterialValueGroup group in MaterialValues) {
		//		if (isIn) {
		//			if (group.color)
		//				sequ.Join(value.DOColor(group.EndColor, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
		//			if (group.fade)
		//				sequ.Join(value.DOFade(group.FadeEndVal, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
		//			if (group.tiling)
		//				sequ.Join(value.DOTiling(group.Tiling, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
		//			if (group.offset)
		//				sequ.Join(value.DOOffset(group.Offset, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
		//			if (group.custParam) {
		//				foreach (FloatVal item in group.FloatParam) {
		//					sequ.Join(value.DOFloat(item.FValue, item.Property, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
		//				}
		//			}
		//		} else {
		//			if (group.color)
		//				sequ.Join(value.DOColor(value.color, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));

		//			if (group.fade)
		//				sequ.Join(value.DOFade(value.color.a, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
		//			if (group.tiling)
		//				sequ.Join(value.DOTiling(group.Tiling, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
		//			if (group.offset)
		//				sequ.Join(value.DOOffset(group.Offset, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
		//			if (group.custParam) {
		//				foreach (FloatVal item in group.FloatParam) {
		//					sequ.Join(value.DOFloat(value.GetFloat(item.Property), item.Property, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
		//				}
		//			}
		//		}
		//	}
		//	return sequ.OnComplete(onComplete);
		//}
		
		
	}
	[Serializable]
	public class MaterialValueGroup {
		public MaterialValueGroup() {
			EndColor = Color.magenta;
			Tiling = Vector2.one;
		}
		public bool color;
		public bool fade;
		public bool tiling;
		public bool offset;
		public bool custParam;

		public Color EndColor;
		public float FadeEndVal;
		public Vector2 Tiling = Vector2.one;
		public Vector2 Offset;

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
