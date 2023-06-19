using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TGP.Utilities;


using DG.Tweening;


namespace TGP.Utilities {
	public abstract class BaseTransition2<T> : BaseMonoBehaviour, ICancelableTransition {
		[SerializeField]
		TransitionState _currState;
		[SerializeField]
		CancelBehaviour CancelBehaviour;
		[SerializeField]
		protected bool startOut = true;
		CancellationTokenSource source = new CancellationTokenSource();
		CancellationToken token;
		public TransitionState CurrState {
			get { return _currState; }
			protected set {
				_currState = value; if (debug) Debug.Log($" setting currState to: {value}");
			}
		}

		[SerializeField]
		protected TweenableValueSo<T> TweenValue;


		protected bool TransDirIn;
		protected Sequence inSequ;
		protected Sequence outSequ;
		public UnityEvent OnCompletedIn;
		public UnityEvent OnCompletedOut;

		protected virtual void Start() {
			if (startOut) {
				CurrState = TransitionState.Out;
				TransDirIn = true;
			} else {
				CurrState = TransitionState.In;
				TransDirIn = false;
			}
			SetupSequenc();
		}

		protected abstract void SetupSequenc();
		/// <summary>
		/// implement your code bevore calling Base
		/// </summary>
		public virtual void TransitionIn() {
			TransDirIn = true;
			switch (CurrState) {
				case TransitionState.In:
					if (debug)
						Debug.Log($"BaseTransition---TransitionIn  already IN on Obj: {this.gameObject.name}");
					break;
				case TransitionState.Transition:
					CancelTransition(CancelBehaviour);
					break;
				case TransitionState.Out:
					if (debug)
						Debug.Log($"BaseTransition---Transitioning In on Obj: {this.gameObject.name}");
					StartTranstion(inSequ);
					break;
				case TransitionState.cancel:
					source.Cancel();
					StartTranstion(inSequ);
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
						Debug.Log($"BaseTransition---Transitioning Out on Obj: {this.gameObject.name}");
					StartTranstion(outSequ);
					break;
				case TransitionState.Transition:
					CancelTransition(CancelBehaviour);
					break;
				case TransitionState.Out:
					if (debug)
						Debug.Log($"BaseTransition---TransitionOut  already OUT on Obj: {this.gameObject.name}");
					break;
				case TransitionState.cancel:
					source.Cancel();
					StartTranstion(outSequ);
					break;
				default:
					break;
			}
		}
		void StartTranstion(Sequence sequence) {
			if (debug)
				Debug.Log($"StartTransition");
			//sequence.OnComplete(() => OnTransitionFinished());
			CurrState = TransitionState.Transition;
			sequence.Restart();
			//Debug.Log($" isplaying: {sequence.IsPlaying()}   {sequence.IsActive()} ");
		}

		protected void OnTransitionFinished() {
			TransitionState endState;
			GetTransEndStateByDir(TransDirIn, out endState);
			CurrState = endState;
			if (debug)
				Debug.LogFormat($"BaseTransition---OnTransitionFinished Finished transiton on Obj: {this.gameObject.name} currState:{endState}  dir: {TransDirIn}");
			if (TransDirIn) {
				OnCompletedIn?.Invoke();
			} else {
				OnCompletedOut?.Invoke();
			}
		}
		async Task<bool> waitForCompletion(Sequence seq, CancellationToken token, bool continueTrans = false) {
			bool isCanced = false;
			float elapsed = seq.Elapsed();
			float elapsedDelay = seq.ElapsedDelay();
			int waittime;
			if (continueTrans) {
				waittime = Mathf.FloorToInt(seq.Duration() - elapsed + seq.Delay() - elapsedDelay);
			} else
				waittime = Mathf.FloorToInt((elapsed + elapsedDelay) * 1000);
			try {
				await Task.Delay(waittime, token);
			} catch (TaskCanceledException e) {
				Debug.LogWarning($"TaskCanceledException {e.Message}");
			} finally {
				isCanced = true;
			}

			Debug.Log($"waitEnd: isCanceled{isCanced}");
			return isCanced;
		}

		public async void CancelTransition(CancelBehaviour behaviour) {
			if (debug)
				Debug.LogWarningFormat($" Canceling current transiton on Obj: {this.gameObject.name}\n Action: {behaviour}");
			CurrState = TransitionState.cancel;
			TransitionState oldState;
			bool isCanceled = false;
			switch (behaviour) {
				case CancelBehaviour.returnToOrigin:
					GetTransEndStateByDir(TransDirIn, out oldState);
					if (inSequ.IsPlaying()) {
						inSequ.Flip();
						isCanceled = await waitForCompletion(inSequ, source.Token);
					} else if (outSequ.IsPlaying()) {
						outSequ.Flip();
						isCanceled = await waitForCompletion(outSequ, source.Token);
					}
					if (isCanceled)
						CurrState = TransitionState.cancel;
					else
						CurrState = oldState;
					break;
				case CancelBehaviour.continueToEndThenReturn:
					if (inSequ.IsPlaying()) {
						isCanceled = await waitForCompletion(inSequ, source.Token, true);
						StartTranstion(outSequ);
					} else if (outSequ.IsPlaying()) {
						isCanceled = await waitForCompletion(outSequ, source.Token, true);
						StartTranstion(inSequ);
					}
					break;
				default:
					break;
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
			switch (CurrState) {
				case TransitionState.Transition:
				case TransitionState.cancel:
					retVal = true;
					if (dirIn)
						endState = TransitionState.In;
					else
						endState = TransitionState.Out;
					break;
				default:
					endState = CurrState;
					break;
			}
			//if (CurrState == TransitionState.Transition) {
			//	retVal = true;
			//	if (dirIn)
			//		endState = TransitionState.In;
			//	else
			//		endState = TransitionState.Out;
			//} else {

			//}
			return retVal;
		}
		//protected float GetPercDur(float perc) {
		//	if (TransDirIn)
		//		return Tweenable.InDuration * (1 - perc);
		//	else
		//		return Tweenable.OutDuration * (1 - perc);
		//}
	}
}
