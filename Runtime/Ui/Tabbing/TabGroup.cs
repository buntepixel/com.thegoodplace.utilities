using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace TGP.Utilities.Ui {
	public class TabGroup : BaseMonoBehaviour {
		public TabButton StartTab;
		public List<TabButton> TabButtons;
		public Color TabIdle;
		public Color TabHover;
		public Color TabActive;

		
		TabButton selected;
		private void Start() {
			OnTabSelected(StartTab);
		}
		public void Subscribe(TabButton button) {
			if (TabButtons == null) {
				TabButtons = new List<TabButton>();
			}
			TabButtons.Add(button);
		}
		public void UnSubscribe(TabButton button) {
			TabButtons.Remove(button);
		}
		public void OnTabEnter(TabButton button) {
			ResetTabs();
			if (selected!=null &&button ==selected)
				return;
			button.Backgroud.color = TabHover;
		}
		public void OnTabExit(TabButton button) {
			ResetTabs();

		}
		public void OnTabSelected(TabButton button) {
			ResetTabs();
			SetSelected(button);
		}
		public void ResetTabs() {
			foreach (TabButton button in TabButtons) {
				if (selected != null && button == selected)
					continue;
				button.Backgroud.color = TabIdle;
			}
		}
		public void SetSelected(TabButton buttonSelected) {
			foreach (TabButton button in TabButtons) {
				if (buttonSelected == button) {
					selected =buttonSelected;
					button.Backgroud.color = TabActive;
				} else {
					button.Backgroud.color = TabIdle;
				}
			}
		}
	}
}
