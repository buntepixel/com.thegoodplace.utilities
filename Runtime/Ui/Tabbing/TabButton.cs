using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;



namespace TGP.Utilities.Ui {
	[RequireComponent(typeof(Image))]
	public class TabButton : BaseMonoBehaviour,IPointerEnterHandler,IPointerClickHandler,IPointerExitHandler {
		
		public TabGroup TabGroup;
		public Image Backgroud;
		public UnityEvent OnClick;
		protected override void Awake() {
			base.Awake();
			Backgroud = GetComponent<Image>();
		}

		private void OnEnable() {
			TabGroup.Subscribe(this);
		}
		private void OnDisable() {
			TabGroup.UnSubscribe(this);
		}
		public void OnPointerClick(PointerEventData eventData) {
			TabGroup.OnTabSelected(this);
			OnClick?.Invoke();
		}
		public void SetActive() {
			TabGroup.OnTabSelected(this);
		}

		public void OnPointerEnter(PointerEventData eventData) {
			TabGroup.OnTabEnter(this);
		}

		public void OnPointerExit(PointerEventData eventData) {
			TabGroup.OnTabExit(this);
		}

		

	}
}
