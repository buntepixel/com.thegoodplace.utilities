using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGP.Utilities {
	[CreateAssetMenu(fileName = "Transform", menuName = "ScriptableObjects/Tweenables/Values/Transform", order = 1)]

	public class TransformSO : TweenableValueSo<Transform> {
		TransformSO() { }
		public Vector3 OutPosition;
		public Vector3 OutRotation;
		public Vector3 OutScale= Vector3.one;
		public bool Pos;
		public bool Rot;
		public bool Scl;

		public override Sequence GetTweenSequence(Transform transform, TweenCallback onComplete, bool isIn = true) {
			Sequence sequ = DOTween.Sequence().SetAutoKill(false); 
			if (isIn) {
				if (Pos) {
					sequ.Join(transform.DOMove(transform.position, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
				}
				if (Rot) {
					sequ.Join(transform.DORotateQuaternion(transform.rotation, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
				}
				if (Scl) {
					sequ.Join(transform.DOScale(transform.localScale, Settings.InDuration).SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve));
				}
			} else {
				if (Pos) {
					sequ.Join(transform.DOLocalMove(OutPosition, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
				}
				if (Rot) {
					sequ.Join(transform.DOLocalRotate(OutRotation, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
				}
				if (Scl) {
					sequ.Join(transform.DOScale(OutScale, Settings.OutDuration).SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve));
				}
			}
			sequ.AppendCallback(onComplete);
			return sequ.Pause();
		}
		
	}
}
