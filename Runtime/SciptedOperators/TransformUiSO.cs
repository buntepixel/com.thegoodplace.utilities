using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEditor;

namespace TGP.Utilities {
	[CreateAssetMenu(fileName = "TransformUi", menuName = "ScriptableObjects/Tweenables/Values/TransformUi", order = 2)]

	public class TransformUiSO : TweenableValueSo<RectTransform> {

		TransformUiSO() { }
		public RectTransformStore rectStore;
		public override Sequence GetTweenSequence(RectTransform rect, TweenCallback onComplete, bool isIn = true) {
			Sequence sequ = DOTween.Sequence().SetAutoKill(false);
				sequ.Join(rect.DOAnchorPos3D(rectStore.Position, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
				sequ.Join(rect.DOSizeDelta(rectStore.Width_Height, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
				sequ.Join(rect.DOAnchorMin(rectStore.Min, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
				sequ.Join(rect.DOAnchorMax(rectStore.Max, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
				sequ.Join(rect.DOPivot(rectStore.Pivot, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
				sequ.Join(rect.DORotateQuaternion(Quaternion.Euler(rectStore.Rotation), Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
				sequ.Join(rect.DOScale(rectStore.Scale, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
			sequ.AppendCallback(onComplete);
			return sequ.Pause();
		}
		public  Sequence GetTweenSequence(RectTransform rect, RectTransformStore rectStore,TweenCallback onComplete) {
			Sequence sequ = DOTween.Sequence().SetAutoKill(false);
			sequ.Join(rect.DOAnchorPos3D(rectStore.Position, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
			sequ.Join(rect.DOSizeDelta(rectStore.Width_Height, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
			sequ.Join(rect.DOAnchorMin(rectStore.Min, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
			sequ.Join(rect.DOAnchorMax(rectStore.Max, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
			sequ.Join(rect.DOPivot(rectStore.Pivot, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
			sequ.Join(rect.DORotateQuaternion(Quaternion.Euler(rectStore.Rotation), Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
			sequ.Join(rect.DOScale(rectStore.Scale, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));

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

		public RectTransformStore(Vector3 pos, Vector2 width_height, Vector2 min, Vector2 max, Vector2 pivot, Vector3 rot, Vector3 scl) {
			Position = pos;
			Width_Height = width_height;
			Min = min;
			Max = max;
			Pivot = pivot;
			Rotation = rot;
			Scale = scl;
		}
	}
}
