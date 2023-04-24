using UnityEngine;
using UnityEngine.EventSystems;


namespace Cf.Utilities.Ui {

	public abstract class BaseSwipeElem : BaseMonoBehaviour {

		internal virtual void OnEnable() {
			Swipegestures.On_EndSwipe += Swipegestures_On_EndSwipe;
			}

		internal virtual void OnDisable() {
			Swipegestures.On_EndSwipe -= Swipegestures_On_EndSwipe;
			}
		private void Swipegestures_On_EndSwipe(TouchManagerEventData attributes, PointerEventData eventData) {
			if (debug)
				Debug.LogFormat($"BaseSwipeElem---Swipegestures_On_EndSwipe dir: {attributes.Direction} raycasttarget: { eventData.pointerCurrentRaycast.gameObject?.name}");
			if (!attributes.isValidSwipe)
				return;
			else if (eventData.pointerCurrentRaycast.gameObject == null || !eventData.pointerCurrentRaycast.gameObject.transform.IsChildOf(this.gameObject.transform))
				return;
			DoSwipeAction(attributes);
			}
		public abstract void DoSwipeAction(TouchManagerEventData attributes);
		}
	}
