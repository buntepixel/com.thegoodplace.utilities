using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
namespace TGP.Utilities {
	public class BaseTransitionTween : BaseTransition {

		protected List<Tween> currTweens;
		protected Queue<Tween> TweenQueue;
		public UnityEvent OnCompletedIn;
		public UnityEvent OnCompletedOut;
		protected override void Start() {
			base.Start();
			}
		public override void OnTransitionStarted() {
			base.OnTransitionStarted();
			int counter = TweenQueue.Count;
			Tween curr;
			if (currTweens == null)
				currTweens = new List<Tween>();
			for (int i = 0; i < counter; i++) {
				curr = TweenQueue.Dequeue().Play();
				currTweens.Add(curr);
				}
			}
		public override void OnTransitionCanceled() {
			base.OnTransitionCanceled();
			float perc;
			Tween curr;
			List<Tween> tmp = new List<Tween>();
			try {
				foreach (Tween item in currTweens) {
					perc = item.ElapsedPercentage();
					item.Kill();
					curr = TweenQueue.Dequeue();
					curr.Goto(GetPercDur(perc), true);
					tmp.Add(curr);
					}
				}
			catch (System.InvalidOperationException e) {
				Debug.LogWarning($"BaseTransitionTween---there was a invalid operation. message: {e.Message}");
				}
			finally {
				currTweens = tmp;
				}
			}
		public override void OnTransitionFinished() {
			base.OnTransitionFinished();
			if (TweenQueue.Count != 0) {
				if (debug)
					Debug.LogWarningFormat($" Queue not Empty contains: {TweenQueue.Count} tweens");
				}
			if (TransDirIn)
				OnCompletedIn?.Invoke();
			else
				OnCompletedOut?.Invoke();
			}
		private void OnTransitionFinished(Tween tween) {
			if (currTweens.Contains(tween))
				currTweens.Remove(tween);
			if (currTweens.Count == 0)
				OnTransitionFinished();
			}
		protected void DoOnComplete(Tween tween) {
			tween.OnComplete(() => {
				OnTransitionFinished(tween);
			});
			if (debug)
				Debug.LogFormat($"BaseTransitionTween---DoOnComplete  queueing tween on obj: {this.gameObject.name};");
			if (TweenQueue == null)
				TweenQueue = new Queue<Tween>();
			TweenQueue.Enqueue(tween);
			}
		}
	}