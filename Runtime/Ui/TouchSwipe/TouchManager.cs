using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using TGP.Utilities;

namespace TGP.Utilities.Ui{


	public class TouchManager : BaseMonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler {
		public delegate void Del_Touch(TouchManagerEventData touchAttributes, PointerEventData eventData);
		public static event Del_Touch On_BeginDragDir;
		public static event Del_Touch On_DragDir;
		public static event Del_Touch On_EndDragDir;
		public static event Del_Touch On_Click;
		public enum TouchDirection {
			up,
			down,
			right,
			left,
			undefined
			}
		[SerializeField]
		[Range(1, 50)]
		internal int BorderWidthPerc = 10;
	
		int queueSize = 4;
		Queue<Vector2> dragPosStack;
		TouchManagerEventData touchManagerEventData;

		bool isClick;
		float pointerDown;
		private void Awake() {
			dragPosStack = new Queue<Vector2>();
			}

		public void OnPointerDown(PointerEventData eventData) {
			if (debug)
				Debug.LogFormat($"TochManager:---OnPointerDown {eventData.pointerCurrentRaycast.gameObject.name}");
			touchManagerEventData = new TouchManagerEventData(this, 0);
			pointerDown = Time.realtimeSinceStartup;
			isClick = true;
			}


		public void OnBeginDrag(PointerEventData eventData) {
			if (debug)
				Debug.LogFormat($"TochManager:---OnBeginDrag {eventData.pointerCurrentRaycast.gameObject.name}");
			isClick = false;
			On_BeginDragDir?.Invoke(GetDirectionStart(eventData.position), eventData);
			}

		public void OnDrag(PointerEventData eventData) {
			AddToQueue(eventData.delta);
			On_DragDir?.Invoke(GetDirection(GetAvgDelta()), eventData);
			}

		public void OnEndDrag(PointerEventData eventData) {
			if (debug)
				Debug.LogFormat($"TochManager:---OnEndDrag {eventData.pointerCurrentRaycast.gameObject.name}");

			On_EndDragDir?.Invoke(GetDirection(GetAvgDelta()), eventData);
			dragPosStack.Clear();
			}

		public void OnPointerUp(PointerEventData eventData) {
			if (eventData.pointerCurrentRaycast.gameObject == null)
				return;
			if (debug)
				Debug.LogFormat($"TochManager:---OnPointerUp {eventData.pointerCurrentRaycast.gameObject.name}");
			if (isClick) {
				if (debug)
					Debug.LogFormat($"TochManager:---OnPointerUp WasClick");
				touchManagerEventData.ClickTime = Time.realtimeSinceStartup - pointerDown;
				On_Click?.Invoke(touchManagerEventData, eventData);
				}
			}
		public void AddToQueue(Vector2 item) {
			dragPosStack.Enqueue(item);
			if (dragPosStack.Count > queueSize)
				dragPosStack.Dequeue();
			}
		public Vector2 GetAvgDelta() {
			Vector2[] tmp = new Vector2[dragPosStack.Count];
			Vector2 sum = Vector2.zero;
			dragPosStack.CopyTo(tmp, 0);
			for (int i = 0; i < tmp.Length; i++) {
				sum += tmp[i];
				}
			return sum / tmp.Length;
			}

		TouchManagerEventData GetDirection(Vector2 oldPos, Vector2 newPos) {
			TouchDirection dir = TouchDirection.undefined;
			Vector2 dirvec = (newPos - oldPos).normalized;
			float signedAngle = Vector2.SignedAngle(Vector2.up, dirvec);
			if (dirvec != Vector2.zero) {
				if (signedAngle > 135)
					dir = TouchDirection.down;
				else if (signedAngle > 45)
					dir = TouchDirection.left;
				else if (signedAngle > -45)
					dir = TouchDirection.up;
				else if (signedAngle > -135)
					dir = TouchDirection.right;
				else
					dir = TouchDirection.down;
				}

			if (touchManagerEventData == null) {
				touchManagerEventData = new TouchManagerEventData(this, signedAngle, dir);
				}
			else {
				touchManagerEventData.SignedAngle = signedAngle;
				touchManagerEventData.TouchManager = this;
				touchManagerEventData.Direction = dir;
				}
			return touchManagerEventData;
			// Debug.LogFormat( $"Touchmanager: dir: { dir}  signedAngle: {signedAngle}");

			}
		TouchManagerEventData GetDirection(Vector2 delta) {
			return GetDirection(Vector2.zero, delta);
			}
		TouchManagerEventData GetDirectionStart(Vector2 startpos) {
			TouchManagerEventData eventData = GetDirection(startpos, startpos);
			eventData.isBorderStart = isBoderStart(startpos);
			return eventData;
			}
		//todo: maybe alternate for top bottom values to consider top&footer menue
		bool isBoderStart(Vector2 pos) {
			//float perc = 50;
			bool retVal = false;
			Vector2 border = new Vector2((Screen.width / 100) * BorderWidthPerc, (Screen.height / 100) * BorderWidthPerc);
			if (pos.x < border.x || pos.y < border.y)//bottom or left
				retVal = true;
			else if (pos.x > Screen.width - border.x || pos.y > Screen.height - border.y)//top or right
				retVal = true;
			if (debug)
				Debug.LogFormat($"isBorderStart::: inPos: {pos} Screendim: {Screen.currentResolution} border: {border} retval: {retVal}");
			return retVal;
			}


		}

	public class TouchManagerEventData {
		public TouchManagerEventData(TouchManager touchManager,float signedAngle, TouchManager.TouchDirection dir) {
			this.SignedAngle = signedAngle;
			this.Direction = dir;
			ClickTime = 0;
			}
		public TouchManagerEventData(TouchManager touchManager, float clickTime) {
			this.ClickTime = clickTime;
			this.SignedAngle = 0;
			this.Direction = TouchManager.TouchDirection.undefined;
			}
		public TouchManager.TouchDirection Direction { get; set; }
		public TouchManager TouchManager;
		public float SignedAngle { get; set; }
		public float ClickTime { get; set; }
		public bool isValidSwipe { get; set; }
		public bool isBorderStart { get; set; }

		}
	}


