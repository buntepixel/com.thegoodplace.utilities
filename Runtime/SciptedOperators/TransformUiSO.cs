using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


namespace TGP.Utilities {
	[CreateAssetMenu(fileName = "TransformUi", menuName = "ScriptableObjects/Tweenables/Values/TransformUi", order = 2)]

	public class TransformUiSO : TweenableValueSo2<RectTransform, RectTransformStore> {

		TransformUiSO() { }
		public RectTransformStore rectStore;

		public override Sequence GetTweenSequenceIn(RectTransform property, RectTransformStore value, TweenCallback onComplete) {
			Sequence sequ = DOTween.Sequence().SetAutoKill(false);
			sequ.Join(property.DOAnchorPos3D(value.Position, Settings.InDuration));
			sequ.Join(property.DOSizeDelta(value.Width_Height, Settings.InDuration));
			sequ.Join(property.DOAnchorMin(value.Min, Settings.InDuration));
			sequ.Join(property.DOAnchorMax(value.Max, Settings.InDuration));
			sequ.Join(property.DOPivot(value.Pivot, Settings.InDuration));
			sequ.Join(property.DORotateQuaternion(Quaternion.Euler(value.Rotation), Settings.InDuration));
			sequ.Join(property.DOScale(value.Scale, Settings.InDuration));

			sequ.SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve);//sequ delay
			sequ.AppendCallback(onComplete);
			return sequ.Pause();
		}

		public override Sequence GetTweenSequenceOut(RectTransform property, RectTransformStore value, TweenCallback onComplete) {
			Sequence sequ = DOTween.Sequence().SetAutoKill(false);
			sequ.Join(property.DOAnchorPos3D(value.Position, Settings.OutDuration));
			sequ.Join(property.DOSizeDelta(value.Width_Height, Settings.OutDuration));
			sequ.Join(property.DOAnchorMin(value.Min, Settings.OutDuration));
			sequ.Join(property.DOAnchorMax(value.Max, Settings.OutDuration));
			sequ.Join(property.DOPivot(value.Pivot, Settings.OutDuration));
			sequ.Join(property.DORotateQuaternion(Quaternion.Euler(value.Rotation), Settings.OutDuration));
			sequ.Join(property.DOScale(value.Scale, Settings.OutDuration));

			sequ.SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve);//sequ delay
			sequ.AppendCallback(onComplete);
			return sequ.Pause();
		}
	}
	[Serializable]
	public class RectTransformStore {

		public Vector3 Position;
		public Vector2 Width_Height;
		[Header("Anchors")]
		public Vector2 Min;
		public Vector2 Max;
		public Vector2 Pivot = new Vector2(0.5f, 0.5f);
		[Space(10)]
		public Vector3 Rotation;
		public Vector3 Scale = Vector3.one;
		public RectTransformStore(RectTransform rect) {
			Position = rect.anchoredPosition3D;
			Width_Height = rect.sizeDelta;
			Min = rect.anchorMin;
			Max = rect.anchorMax;
			Pivot = rect.pivot;
			Rotation = rect.rotation.eulerAngles;
			Scale = rect.localScale;
		}

		//public RectTransformStore(Vector3 pos, Vector2 width_height, Vector2 min, Vector2 max, Vector2 pivot, Vector3 rot, Vector3 scl) {
		//	Position = pos;
		//	Width_Height = width_height;
		//	Min = min;
		//	Max = max;
		//	Pivot = pivot;
		//	Rotation = rot;
		//	Scale = scl;
		//}
	}
}
