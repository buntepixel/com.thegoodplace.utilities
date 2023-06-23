using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TGP.Utilities {
	[RequireComponent(typeof(RectTransform))]
	public class TweenableTranformUi : BaseTransition<RectTransform> {
		TransformUiSO tranVal;
		RectTransform rect;
		RectTransformStore rectOri;
		private void Awake() {
			tranVal = TweenValue as TransformUiSO;
			rect = GetComponent<RectTransform>();
			//save values from rect to class rectOri
			rectOri = new RectTransformStore(rect);
		}
		protected override void Start() {
			base.Start();
			if (startOut) {
				Debug.Log("startOut");
				rect.anchoredPosition3D = tranVal.rectStore.Position;
				rect.sizeDelta = tranVal.rectStore.Width_Height;
				rect.anchorMin = tranVal.rectStore.Min;
				rect.anchorMax = tranVal.rectStore.Max;
				rect.pivot = tranVal.rectStore.Pivot;
				rect.rotation = Quaternion.Euler(tranVal.rectStore.Rotation.x, tranVal.rectStore.Rotation.y, tranVal.rectStore.Rotation.z);
				rect.localScale = tranVal.rectStore.Scale;
			}
		
		}
		protected override void SetupSequenc() {
			if (tranVal != null) {
				inSequ = tranVal.GetTweenSequence(rect, rectOri, () => OnTransitionFinished());
				outSequ = tranVal.GetTweenSequence(rect, () => OnTransitionFinished());
			} else {
				Debug.LogWarning($"Could not find an TransformSo on {this.gameObject.name}");
			}
		}
		public override void TransitionIn() {
			base.TransitionIn();
		}
		public override void TransitionOut() {
			base.TransitionOut();
		}
	}
}
