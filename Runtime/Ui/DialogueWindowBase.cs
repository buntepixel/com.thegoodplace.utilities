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
		protected Vector2 In_Out_Value;
	
		CanvasGroup cg;
		protected virtual void Awake() {
			cg = GetComponent<CanvasGroup>();
			}
		protected virtual void Start() {
			switch (TypeOfAnimation) {
				case AnimationType.fade:
					cg.EnableInputVisibility(false);
					break;
				case AnimationType.moveRtL:
					cg.EnableInputVisibility(false);
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


			ButtonAnimation(In_Out_Value.x, args, TypeOfAnimation);
		
			
			}

		protected virtual void CloseWindow(System.Object sender, DialogueWindowBaseEventArgs args) {
			if (debug)
				Debug.LogFormat($"DiaogueWindowBase---CloseWindow: on:{this.gameObject.name}");
			ButtonAnimation(In_Out_Value.y, args, TypeOfAnimation);
		
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
						cg.EnableInputVisibility( In_Out_Value.x == endValue ? true : false);
					});
					break;
				default:
					break;
				}

			}

		}

	public class DialogueWindowBaseEventArgs : EventArgs {
		public DialogueWindowBaseEventArgs() {
			TweenDuration = 0.3f;
			}
	
		public float TweenDuration { get; set; }
		public int DisplayDuration { get; set; }
		}
	}
