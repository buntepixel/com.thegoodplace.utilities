using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using TGP.Utilities;


namespace TGP.Utilities.Ui {
	[RequireComponent(typeof(CanvasGroup))]
	public abstract class DialogueWindowBase : BaseMonoBehaviour {
		public enum AnimationType {
			fade,
			moveRtL
		}
		[SerializeField]
		protected RectTransform Mover;
		[SerializeField]
		AnimationType TypeOfAnimation;
		[SerializeField]
		[Tooltip("if animated In/Out")]
		protected Vector2 In_Out_Value;
		public static bool isOpen { get;protected set; }
		CanvasGroup cg;
		protected override void Awake() {
			base.Awake();
			cg = GetComponent<CanvasGroup>();
		}
		protected virtual void Start() {
			cg.EnableInputVisibility(false);
			switch (TypeOfAnimation) {
				case AnimationType.fade:
					In_Out_Value = new Vector2(1, 0);
					break;
				case AnimationType.moveRtL:
					Mover.anchoredPosition = new Vector2(In_Out_Value.y, Mover.anchoredPosition.y);
					//Debug.LogFormat($" seting mover to: {Mover.anchoredPosition} ioVal: {In_Out_Value}");
					break;
				default:
					break;
			}
		}


		protected virtual void OpenWindow(System.Object sender, DialogueWindowBaseEventArgs args) {
			if (debug)
				Debug.LogFormat($"DiaogueWindowBase---OpenWindow: on:{this.gameObject.name}");
			if (!isOpen)
				ButtonAnimation(In_Out_Value.x, args, TypeOfAnimation);
			
			isOpen = true;
		}

		protected virtual void CloseWindow(System.Object sender, DialogueWindowBaseEventArgs args) {
			if (this.gameObject!=null &&debug)
				Debug.LogFormat($"DiaogueWindowBase---CloseWindow: on:{this.gameObject.name}");
			ButtonAnimation(In_Out_Value.y, args, TypeOfAnimation);
			isOpen = false;
		}

		protected async void AutoHide(DialogueWindowBaseEventArgs args) {
			await Task.Delay(args.DisplayDuration * 1000);
			CloseWindow(this, args);
		}
		protected virtual void ButtonAnimation(float endValue, DialogueWindowBaseEventArgs args, AnimationType animType) {
			switch (animType) {
				case AnimationType.fade:
					cg.DOFade(endValue, args.TweenDuration).OnComplete(() => {
						cg.EnableInputVisibility(In_Out_Value.x == endValue ? true : false);
					});
					break;
				case AnimationType.moveRtL:
					if (Mover == null)
						return;
					Mover.DOAnchorPosX(endValue, args.TweenDuration).OnComplete(() => {
						cg.EnableInputVisibility(In_Out_Value.x == endValue ? true : false);
					});
					break;
				default:
					break;
			}

		}

	}

	public class DialogueWindowBaseEventArgs : EventArgs {
		public DialogueWindowBaseEventArgs() {
			TweenDuration = 0.1f;
			
		}

		public DialogueWindowBaseEventArgs(int displayDuration):base() {
			DisplayDuration = displayDuration;
		}

		public float TweenDuration { get; set; }
		public int DisplayDuration { get; set; }
	}
}
