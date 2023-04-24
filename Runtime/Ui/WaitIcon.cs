using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TGP.Utilities.Ui {

	public class WaitIcon : MonoBehaviour {
		public delegate void WaitIcon_delegate(System.Object sender, WaitIconEventArgs args);
		public static WaitIcon_delegate OnShow;
		[SerializeField]
		Image RotateImage;
		RectTransform RectRotImage;
		[SerializeField]
		Image AnimImage;
		RectTransform RectAnimImage;
		[SerializeField]
		float TurnsPerSec = 1;
		[SerializeField]
		float MovesPerSec = 3;

		bool active;
		List<System.Object> dispatchlist;
		float animTimer;

		CanvasGroup cg;
		private void Awake() {
			cg = GetComponent<CanvasGroup>();
			dispatchlist = new List<object>();
			RectAnimImage = AnimImage.GetComponent<RectTransform>();
			RectRotImage = RotateImage.GetComponent<RectTransform>();
			OnShow = Activate;
			}
		private void Start() {
			cg.EnableInputVisibility(false);
			
			}

		void Activate(System.Object sender, WaitIconEventArgs args) {
			if (args.Show) {
				dispatchlist.Add(sender);
				cg.EnableInputVisibility(true);
				active = true;
				}
			else {
				if (dispatchlist.Contains(sender))
					dispatchlist.Remove(sender);
				if (dispatchlist.Count == 0) {
					cg.EnableInputVisibility(false);
					active = false;
					}
				}
			}
		public void Activate() {
			WaitIcon.OnShow(this, new WaitIconEventArgs { Show = true });
			}
		public void DeActivate() {
			WaitIcon.OnShow(this, new WaitIconEventArgs { Show = false });
			}

		private void Update() {
			if (active) {
				Quaternion vec = RectRotImage.localRotation;
				RectRotImage.localRotation = vec * Quaternion.Euler(0, 0, ((360 * Time.deltaTime) * -TurnsPerSec));
				animTimer += Time.deltaTime;
				if (animTimer > 1 / MovesPerSec) {
					RectAnimImage.localScale = RectAnimImage.localScale.x == 1 ? new Vector3(-1, 1, 1) : Vector3.one;
					animTimer = 0;
					}

				}
			}

		}
	public class WaitIconEventArgs : EventArgs {
		public bool Show;
		}
	}
