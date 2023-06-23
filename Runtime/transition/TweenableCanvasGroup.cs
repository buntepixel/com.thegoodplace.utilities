using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGP.Utilities {
	public class TweenableCanvasGroup : BaseTransition<float> {
		TransformFloatSO floatSO;
		private void Awake() {
			floatSO = TweenValue as TransformFloatSO;
		}

		protected override void SetupSequenc() {
			if (floatSO != null) {
				inSequ = floatSO.GetTweenSequence(0, () => OnTransitionFinished());
				outSequ = floatSO.GetTweenSequence(1, () => OnTransitionFinished());
			} else {
				Debug.LogWarning($"Could not find an TransformFloatSO on {this.gameObject.name}");
			}
		}
	}
}
