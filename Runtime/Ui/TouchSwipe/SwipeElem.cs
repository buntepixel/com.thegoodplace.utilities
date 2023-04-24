
using UnityEngine;
using UnityEngine.Events;
namespace TGP.Utilities.Ui {
	public class SwipeElem : BaseSwipeElem {

		public TouchManager.TouchDirection InSwipe;
		public TouchManager.TouchDirection OutSwipe;

		public UnityEvent InSwipeEvent;
		public UnityEvent OutSwipeEvent;
		public override void DoSwipeAction(TouchManagerEventData attributes) {
			if (debug)
				Debug.LogFormat($"SwipeElem--DoSwipeAction ");

			if (attributes.Direction == InSwipe) {
				InSwipeEvent?.Invoke();
				}
			else if (attributes.Direction == OutSwipe) {
				OutSwipeEvent?.Invoke();
				}
			}
		}
	}