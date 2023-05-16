using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGP.Utilities {
	public abstract class BaseTransition : MonoBehaviour {
		[SerializeField]
		protected bool debug;
		[SerializeField]
		[Range(0, 5)]
		protected float durationIn = 1;
		[SerializeField]
		[Range(0, 5)]
		protected float delayIn;
		[SerializeField]
		[Range(0, 5)]
		protected float durationOut = 1;
		[SerializeField]
		[Range(0, 5)]
		protected float delayOut;
		[SerializeField]
		protected bool startOut;
	
		protected bool TransDirIn;
		TransitionState _currState;
		public TransitionState CurrState { get { return _currState; } set { _currState = value; } }


		protected virtual void Start() {
			if (startOut) {
				CurrState = TransitionState.Out;
				TransDirIn = true;
				}
			else {
				CurrState = TransitionState.In;
				TransDirIn = false;
				}
			}
		/// <summary>
		/// Does a Transiton IN: you should implement your stuff before the base.TransitionIn()
		/// </summary>
		public virtual void TransitionIn() {
			TransDirIn = true;
			switch (CurrState) {
				case TransitionState.In:
					if (debug)
						Debug.LogWarningFormat($"BaseTransition---TransitionIn  already IN on Obj: {this.gameObject.name}");
					break;
				case TransitionState.Transition:
					OnTransitionCanceled();
					break;
				case TransitionState.Out:
					if (debug)
						Debug.LogWarningFormat($"BaseTransition---Transitioning In on Obj: {this.gameObject.name}");
					OnTransitionStarted();
					break;
				default:
					break;
				}
			}
		/// <summary>
		/// Does a Transiton OUt: you should implement your stuff before the base.TransitionOut()
		/// 
		/// </summary>
		public virtual void TransitionOut() {
			TransDirIn = false;
			switch (CurrState) {
				case TransitionState.In:
					if (debug)
						Debug.LogWarningFormat($"BaseTransition---Transitioning Out on Obj: {this.gameObject.name}");
					OnTransitionStarted();
					break;
				case TransitionState.Transition:
					OnTransitionCanceled();
					break;
				case TransitionState.Out:
					if (debug)
						Debug.LogWarningFormat($"BaseTransition---TransitionOut  already OUT on Obj: {this.gameObject.name}");
					break;
				default:
					break;
				}
			}
		public virtual void OnTransitionStarted() {
			if (CurrState != TransitionState.Transition) {
				CurrState = TransitionState.Transition;
				}
			}
		public virtual void OnTransitionCanceled() {
			if (debug)
				Debug.LogWarningFormat($"BaseTransition---OnTransitionCanceled Canceling current transiton on Obj: {this.gameObject.name}");
			TransitionState oldState;
			GetTransEndStateByDir(TransDirIn, out oldState);//getEndState since dir already correct
			CurrState = oldState;
			}
		public virtual void OnTransitionFinished() {
		
			}
		bool GetTransEndStateByDir(bool dirIn, out TransitionState endState) {
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

		bool GetTransStartStateByDir(bool dirIn, out TransitionState endState) {
			return GetTransEndStateByDir(!dirIn, out endState);//invert direction to get the opposite
			}
		protected float GetPercDur(float perc) {
			if (TransDirIn)
				return durationIn * (1 - perc);
			else
				return durationOut * (1 - perc);
			}
		}
	}
