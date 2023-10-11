using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGP.Utilities {
	[CreateAssetMenu(fileName = "Material", menuName = "ScriptableObjects/Tweenables/Values/Material", order = 2)]

	public class MaterialSo : TweenableValueSo2<Material, MaterialValueGroup> {
		//public MaterialSo() { }


		public MaterialValueGroup MaterialValue;

		//public override Sequence GetTweenSequence(Material tweenProperty, Material tweenValue, TweenCallback onComplete, bool isIn = true) {
		//	Sequence sequ = DOTween.Sequence().SetAutoKill(false);
		//	foreach (MaterialValueGroup group in MaterialValue) {
		//		if (isIn) {
		//			if (group.color)
		//				sequ.Join(tweenProperty.DOColor(group.EndColor, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
		//			if (group.fade)
		//				sequ.Join(tweenProperty.DOFade(group.EndFadeVal, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
		//			if (group.tiling)
		//				sequ.Join(tweenProperty.DOTiling(group.EndTiling, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
		//			if (group.offset)
		//				sequ.Join(tweenProperty.DOOffset(group.EndOffset, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
		//			if (group.custParam) {
		//				foreach (FloatVal item in group.FloatParam) {
		//					sequ.Join(tweenProperty.DOFloat(item.FValue, item.Property, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
		//				}
		//			}
		//		} else {
		//			if (group.color)
		//				sequ.Join(tweenProperty.DOColor(tweenValue.color, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
		//			if (group.fade)
		//				sequ.Join(tweenProperty.DOFade(tweenValue.color.a, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
		//			if (group.tiling)
		//				sequ.Join(tweenProperty.DOTiling(group.EndTiling, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
		//			if (group.offset)
		//				sequ.Join(tweenProperty.DOOffset(group.EndOffset, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
		//			if (group.custParam) {
		//				foreach (FloatVal item in group.FloatParam) {
		//					sequ.Join(tweenProperty.DOFloat(tweenProperty.GetFloat(item.Property), item.Property, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
		//				}
		//			}
		//		}
		//	}
		//	sequ.AppendCallback(onComplete);
		//	return sequ.Pause();
		//}

		public override Sequence GetTweenSequenceIn(Material property, MaterialValueGroup value, TweenCallback onComplete) {
			Sequence sequ = DOTween.Sequence().SetAutoKill(false);

			if (value.color)
				sequ.Join(property.DOColor(value.EndColor, Settings.InDuration));
			if (value.fade)
				sequ.Join(property.DOFade(value.EndFadeVal, Settings.InDuration));
			if (value.tiling)
				sequ.Join(property.DOTiling(value.EndTiling, Settings.InDuration));
			if (value.offset)
				sequ.Join(property.DOOffset(value.EndOffset, Settings.InDuration));
			if (value.custParam) {
				foreach (FloatVal item in value.FloatParam) {
					sequ.Join(property.DOFloat(item.FValue, item.Property, Settings.InDuration));
				}
			}
			sequ.SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve).AppendCallback(onComplete);
			return sequ.Pause();
		}


		public override Sequence GetTweenSequenceOut(Material property, MaterialValueGroup value, TweenCallback onComplete) {
			Sequence sequ = DOTween.Sequence().SetAutoKill(false);

			if (value.color)
				sequ.Join(property.DOColor(value.EndColor, Settings.OutDuration));
			if (value.fade)
				sequ.Join(property.DOFade(value.EndFadeVal, Settings.OutDuration));
			if (value.tiling)
				sequ.Join(property.DOTiling(value.EndTiling, Settings.OutDuration));
			if (value.offset)
				sequ.Join(property.DOOffset(value.EndOffset, Settings.OutDuration));
			if (value.custParam) {
				foreach (FloatVal item in value.FloatParam) {
					sequ.Join(property.DOFloat(property.GetFloat(item.Property), item.Property, Settings.OutDuration));
				}
			}
			sequ.SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve).AppendCallback(onComplete);
			return sequ.Pause();
		}
	}
	[Serializable]
	public class MaterialValueGroup {
		public MaterialValueGroup() {
			EndColor = Color.magenta;
			EndTiling = Vector2.one;
		}
		public MaterialValueGroup(Material material, MaterialSo transVal) {
			color = transVal.MaterialValue.color;
			fade= transVal.MaterialValue.fade;
			tiling = transVal.MaterialValue.tiling;
			offset = transVal.MaterialValue.offset;
			custParam = transVal.MaterialValue.custParam;

			EndColor = material.color;
			EndFadeVal = material.color.a;
			EndTiling = material.mainTextureScale;
			EndOffset = material.mainTextureOffset;
			if (transVal.MaterialValue.FloatParam != null) {
				FloatParam = new FloatVal[transVal.MaterialValue.FloatParam.Length];
				for (int i = 0; i < FloatParam.Length; i++) {
				FloatParam[i] = new FloatVal(material.GetFloat(transVal.MaterialValue.FloatParam[i].Property), transVal.MaterialValue.FloatParam[i].Property);
				}
			}
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
		public FloatVal() { }
		public FloatVal(float value, string property) {
			FValue = value;
			Property = property;
		}
	}
}

