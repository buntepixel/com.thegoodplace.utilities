using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGP.Utilities {
	[CreateAssetMenu(fileName = "Transform", menuName = "ScriptableObjects/Tweenables/Values/Transform", order = 1)]

	public class TransformSO : TweenableValueSo2<Transform, TransformStorer> {
		TransformSO() { }
		public TransformStorer Transforms;

		public bool Pos;
		public bool Rot;
		public bool Scl;

		public override Sequence GetTweenSequenceIn(Transform property, TransformStorer value, TweenCallback onComplete) {
			Sequence sequ = DOTween.Sequence().SetAutoKill(false);
			if (Pos) {
				sequ.Join(property.DOMove(value.Position, Settings.InDuration));
			}
			if (Rot) {
				sequ.Join(property.DORotateQuaternion(value.Rotation, Settings.InDuration));
			}
			if (Scl) {
				sequ.Join(property.DOScale(value.Scale, Settings.InDuration));
			}
			sequ.SetDelay(Settings.InDelay).SetEase(Settings.InEaseCurve).AppendCallback(onComplete);
			return sequ.Pause();
		}

		public override Sequence GetTweenSequenceOut(Transform property, TransformStorer value, TweenCallback onComplete) {
			Sequence sequ = DOTween.Sequence().SetAutoKill(false);
			if (Pos) {
				sequ.Join(property.DOMove(value.Position, Settings.OutDuration));
			}
			if (Rot) {
				sequ.Join(property.DORotateQuaternion(value.Rotation, Settings.OutDuration));
			}
			if (Scl) {
				sequ.Join(property.DOScale(value.Scale, Settings.OutDuration));
			}
			sequ.SetDelay(Settings.OutDelay).SetEase(Settings.OutEaseCurve).AppendCallback(onComplete);
			return sequ.Pause();
		}
	}
	[Serializable]
	public class TransformStorer {
		public Vector3 Position;
		public Quaternion Rotation;
		public Vector3 Scale = Vector3.one;
		public TransformStorer(Transform tran) {
			Position = tran.localPosition;
			Rotation = tran.localRotation;
			Scale = tran.localScale;
		}
	
		public Transform StoredValToTransfom(Transform trans) {
			trans.localPosition = Position;
			trans.localRotation = Rotation;
			trans.localScale = Scale;
			return trans;
		}
	}
	//public class TransformValuesContainer:BaseClassValueStore<Transform,TransformStorer> {
	//	public TransformValuesContainer(Transform tran) : base(tran) { }

	//	public override void SetTargetValue(TransformStorer targetVal) {
	//		TargetValue = targetVal;	
	//	}

	//	public  override TransformStorer ConvertToStoreClass(Transform oriValue) {
	//		OriValue = new TransformStorer(oriValue);
	//		return OriValue;
	//	}
	//}
	
}
