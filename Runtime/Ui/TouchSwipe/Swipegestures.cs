using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cf.Utilities;


namespace Cf.Utilities.Ui{

	[RequireComponent(typeof(TouchManager))]
	public class Swipegestures : BaseMonoBehaviour {

		public delegate void Del_SwipeGesture(TouchManagerEventData attributes, PointerEventData eventData);
		public static event Del_SwipeGesture On_StartSwipe;
		public static event Del_SwipeGesture On_Swipe;
		public static event Del_SwipeGesture On_EndSwipe;

		[SerializeField]
		[Range(1, 50)]
		internal float MinSwipePerc = 5;

		float TimeCount;
		Vector2 totalDragDelta;
		Vector2 StartPos;
		

		private void OnEnable() {
			TouchManager.On_BeginDragDir += TouchManager_On_BeginDragDir;
			TouchManager.On_DragDir += TouchManager_On_DragDir;
			TouchManager.On_EndDragDir += TouchManager_On_EndDragDir;
			}
		private void OnDisable() {
			TouchManager.On_BeginDragDir -= TouchManager_On_BeginDragDir;
			TouchManager.On_DragDir -= TouchManager_On_DragDir;
			TouchManager.On_EndDragDir -= TouchManager_On_EndDragDir;
			}
		private void TouchManager_On_BeginDragDir(TouchManagerEventData attributes, UnityEngine.EventSystems.PointerEventData eventData) {
			if (debug)
				Debug.LogFormat($"SwipeGesture--TouchManager_On_BeginDragDir: {eventData.position} pressPos: {eventData.pressPosition}");
			StartPos = eventData.position;

			TimeCount = 0;
			totalDragDelta = Vector2.zero;
			//if (attributes.isBorderStart = isBoderSwipe(StartPos))
			//	return;
			if (attributes.isBorderStart)
				return;
			On_StartSwipe?.Invoke(attributes, eventData);
			}
		private void TouchManager_On_DragDir(TouchManagerEventData attributes, UnityEngine.EventSystems.PointerEventData eventData) {
			//throw new System.NotImplementedException();
			TimeCount += Time.deltaTime;
			totalDragDelta += eventData.delta;
			if (attributes.isBorderStart)
				return;
			attributes.isValidSwipe = isValidDragLength(attributes.Direction, totalDragDelta);
			On_Swipe?.Invoke(attributes, eventData);
			}
		private void TouchManager_On_EndDragDir(TouchManagerEventData attributes, UnityEngine.EventSystems.PointerEventData eventData) {

			if (debug)
				Debug.LogFormat($"SwipeGesture--TouchManager_On_EndDragDir endDragpos: {eventData.position} pressPos: {eventData.pressPosition} timeC: {TimeCount} ");
			attributes.isValidSwipe = isValidDragLength(attributes.Direction, totalDragDelta);
			On_EndSwipe?.Invoke(attributes, eventData);
			}
		bool isValidDragLength(TouchManager.TouchDirection direction, Vector2 delta) {
			float perc = 0;
			switch (direction) {
				case TouchManager.TouchDirection.up:
				case TouchManager.TouchDirection.down:
					perc = delta.y / (Screen.height / 100);
					break;
				case TouchManager.TouchDirection.right:
				case TouchManager.TouchDirection.left:
					perc = delta.x / (Screen.height / 100);
					break;
				case TouchManager.TouchDirection.undefined:
					break;
				default:
					break;
				}

			if (Mathf.Abs(perc) > MinSwipePerc)
				return true;
			else
				return false;
			}
	

		}
	
	}