using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TGP.Utilities;
using DG.Tweening;

namespace TGP.StateFramework {
	public abstract class BaseTransition2 : BaseMonoBehaviour, ICancelableTransition {
		[SerializeField]
		TransitionState _currState;
		[SerializeField]
		protected bool startOnEnd;
		public TransitionState CurrState { get { return _currState; } protected set { _currState = value; } }
		[SerializeField]
		protected TweenableSettingsSO Tweenable;


		protected bool TransDirIn;
		protected Sequence TweenSequ;
		protected Queue<Tween> TweenQueue;
		public UnityEvent OnCompletedIn;
		public UnityEvent OnCompletedOut;
		private void Awake() {
			TweenSequ = new List<Tween>();
		}
		protected virtual void Start() {
			if (startOnEnd) {
				CurrState = Utilities.TransitionState.Out;
				TransDirIn = true;
			} else {
				CurrState = Utilities.TransitionState.In;
				TransDirIn = false;
			}
		}
		/// <summary>
		/// implement your code bevore calling Base
		/// </summary>
		public virtual void TransitionIn() {
			TransDirIn = true;
			switch (CurrState) {
				case TransitionState.In:
					if (debug)
						Debug.LogWarningFormat($"BaseTransition---TransitionIn  already IN on Obj: {this.gameObject.name}");
					break;
				case TransitionState.Transition:
					//OnAnimationCanceled();
					break;
				case TransitionState.Out:
					if (debug)
						Debug.LogWarningFormat($"BaseTransition---Transitioning In on Obj: {this.gameObject.name}");
					StartTranstion();
					break;
				default:
					break;
			}
		}
		/// <summary>
		/// implement your code bevore calling Base
		/// </summary>
		public virtual void TransitionOut() {
			TransDirIn = false;
			switch (CurrState) {
				case TransitionState.In:
					if (debug)
						Debug.LogWarningFormat($"BaseTransition---Transitioning Out on Obj: {this.gameObject.name}");
					StartTranstion();
					break;
				case TransitionState.Transition:
					//OnTransitionCanceled();
					break;
				case TransitionState.Out:
					if (debug)
						Debug.LogWarningFormat($"BaseTransition---TransitionOut  already OUT on Obj: {this.gameObject.name}");
					break;
				default:
					break;
			}
		}
		void StartTranstion(Sequence sequence) {
			sequence.OnComplete(() => OnTransitionFinished());
			CurrState = TransitionState.Transition;
			Tween curr;
			for (int i = 0; i < TweenQueue.Count; i++) {
				curr = TweenQueue.Dequeue().Play();
				TweenSequ.Add(curr);
			}
		}
		void OnTransitionFinished() {
			TransitionState endState;
			GetTransEndStateByDir(TransDirIn, out endState);
			CurrState = endState;
			if (debug)
				Debug.LogFormat($"BaseTransition---OnTransitionFinished Finished transiton on Obj: {this.gameObject.name} currState:{endState}  dir: {TransDirIn}");

			if (TweenQueue.Count != 0) {
				if (debug)
					Debug.LogWarningFormat($" Queue not Empty contains: {TweenQueue.Count} tweens \n clearing Queue");
				TweenQueue.Clear();
			}
			if (TransDirIn)
				OnCompletedIn?.Invoke();
			else
				OnCompletedOut?.Invoke();
		}

		public void CancelTransition(CancelBehaviour behaviour) {
			if (debug)
				Debug.LogWarningFormat($" Canceling current transiton on Obj: {this.gameObject.name}\n Action: {behaviour}");
			if (behaviour == CancelBehaviour.returnToOrigin) {
				TransitionState oldState;
				GetTransEndStateByDir(TransDirIn, out oldState);
				CurrState = oldState;
				float perc;
				Tween curr;
				List<Tween> tmp = new List<Tween>();
				try {
					foreach (Tween item in TweenSequ) {
						perc = item.ElapsedPercentage();
						item.Kill();
						curr = TweenQueue.Dequeue();
						curr.Goto(GetPercDur(perc), true);
						tmp.Add(curr);
					}
				} catch (System.InvalidOperationException e) {
					Debug.LogWarning($"BaseTransitionTween---there was a invalid operation. message: {e.Message}");
				} finally {
					TweenSequ = tmp;
				}
			}
		}
		/// <summary>
		/// Returns the Endstate based on animationDirection
		/// </summary>
		/// <param name="dirIn">if animating in = true</param>
		/// <param name="endState">State animation will end on</param>
		/// <returns></returns>
		bool GetTransEndStateByDir(bool dirIn, out Utilities.TransitionState endState) {
			bool retVal = false;
			if (CurrState == TransitionState.Transition) {
				retVal = true;
				if (dirIn)
					endState = TransitionState.In;
				else
					endState = TransitionState.Out;
			} else {
				endState = CurrState;
			}
			return retVal;
		}
		protected float GetPercDur(float perc) {
			if (TransDirIn)
				return Tweenable.InDuration * (1 - perc);
			else
				return Tweenable.OutDuration * (1 - perc);
		}
		private void OnTransitionFinished(Tween tween) {
			if (TweenSequ.Contains(tween))
				TweenSequ.Remove(tween);
			if (TweenSequ.Count == 0)
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
