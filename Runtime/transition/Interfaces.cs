using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGP.Utilities {
	public interface ITransition {
		TransitionState CurrState { get; }
		void TransitionIn();
		void TransitionOut();
		
		

	}
	public interface ICancelableTransition:ITransition {
		void CancelTransition(CancelBehaviour behaviour);
	}
	public enum TransitionState {
		In,
		Transition,
		Out
	}
	public enum CancelBehaviour {
		returnToOrigin,
		continueToEnd
	}
}
