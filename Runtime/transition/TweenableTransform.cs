using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGP.Utilities {
	public class TweenableTransform : BaseTransition<Transform> {
		protected override void Start() {
			base.Start();
			if (startOut) {
				TransformSO tranVal = TweenValue as TransformSO;
				if (tranVal.Pos)
					transform.localPosition = tranVal.OutPosition;
				if (tranVal.Rot)
					transform.localRotation=Quaternion.Euler( tranVal.OutRotation.x, tranVal.OutRotation.y, tranVal.OutRotation.z);
				if (tranVal.Scl)
					transform.localScale = tranVal.OutScale;
			}
		}
		protected override void SetupSequenc() {
			
			TransformSO tranVal = TweenValue as TransformSO;
			if (tranVal != null) {
				inSequ = tranVal.GetTweenSequence(this.transform,()=>OnTransitionFinished());
				outSequ = tranVal.GetTweenSequence(this.transform, () => OnTransitionFinished(), false);
			} else {
				Debug.LogWarning($"Could not find an TransformSo on {this.gameObject.name}");
			}

		}
	}
}
