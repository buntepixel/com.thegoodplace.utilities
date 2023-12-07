using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;



namespace TGP.Utilities.Ui {
	public class UiAkkordeonDropdown : BaseMonoBehaviour, IPointerClickHandler, ISubmitHandler, ICancelHandler {
		protected internal class DropdownItem : BaseMonoBehaviour, IPointerClickHandler {

			[SerializeField]
			private TMP_Text m_Text;
			[SerializeField]
			private Image m_Image;
			public Action<int> menuPos;
			[SerializeField]
			internal int PosInMenu;

			internal UiAkkordeonDropdown parent;

			private void OnEnable() {
				m_Image = GetComponentInChildren<Image>();
				m_Text = GetComponentInChildren<TMP_Text>();
				}

			public bool SetImage(Sprite sprite) {
				bool sucess = !!m_Image;
				if (sucess)
					m_Image.sprite = sprite;
				return sucess;
				}
			public void SetPosInMenu(int pos) {
				PosInMenu = pos;
				}

			public bool SetText(String txt) {
				bool sucess = !!(m_Text);
				if (sucess) {
					this.m_Text.text = txt;
					this.gameObject.name = string.Concat("option_", txt);
					}
				return sucess;
				}

			public void OnPointerClick(PointerEventData eventData) {
				if (debug)
					Debug.LogFormat($"pointerclickDropdown  {eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<RectTransform>().anchoredPosition.y} item ");
				menuPos?.Invoke(Mathf.RoundToInt(eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<RectTransform>().anchoredPosition.y));
				parent.SetCurrentOption(PosInMenu);
				}
			}
		[SerializeField]
		RectTransform m_Template;
		[SerializeField]
		RectTransform m_SelectionBar;
		[SerializeField]
		 protected RectTransform m_DropdownParent;
		[SerializeField]
		RectTransform Arrow;


		// Items that will be visible when the dropdown is shown.
		// We box this into its own class so we can use a Property Drawer for it.
		[SerializeField]
		protected OptionDataList m_Options = new OptionDataList();
		GoPool m_menuElemPool;
		 List<GameObject> m_MenuItems;
		bool m_menuOpen = false;
		/// <summary>
		/// holds the current Selection pos in the menue. base is the OptionDataList;
		/// </summary>
		public int CurrentOption { get; private set; }
		protected virtual void Awake() {
			m_menuElemPool = new GoPool(m_DropdownParent, m_Template.gameObject, "MenueElem_Pool");
			m_MenuItems = new List<GameObject>();
			}

		private bool validTemplate = false;
		private void SetupSelectionBar() {
			Vector2 rectDim = m_DropdownParent.GetComponent<RectTransform>().sizeDelta;
			m_SelectionBar.sizeDelta = new Vector2(rectDim.x, m_Template.sizeDelta.y);
			m_SelectionBar.pivot = m_Template.anchorMin = m_Template.anchorMax = Vector2.one;
			m_SelectionBar.anchoredPosition = new Vector2(m_DropdownParent.GetComponent<VerticalLayoutGroup>().padding.left, 0);
			//m_SelectionBar.gameObject.SetActive(false);
			}

		protected virtual void SetCurrentOption(int val) {
			CurrentOption = val;
			}

		void SetupMenuItems() {
			int counter = 0;
			foreach (OptionData data in m_Options.options) {

				GameObject item = m_menuElemPool.GetItem(m_DropdownParent);
				m_MenuItems.Add(item);
				DropdownItem dropdown = GetOrAddComponent<DropdownItem>(item);
				dropdown.parent = this;
				dropdown.menuPos += MoveBarToPos;
				dropdown.SetImage(data.image);
				dropdown.SetText(data.text);
				dropdown.SetPosInMenu(counter);
				counter++;
				}
			}


		protected internal virtual void MoveBarToPos(int pos) {
			m_SelectionBar.anchoredPosition = new Vector2(m_SelectionBar.anchoredPosition.x, pos + m_SelectionBar.sizeDelta.y / 2);
			}

		private static T GetOrAddComponent<T>(GameObject go) where T : Component {
			T comp = go.GetComponent<T>();
			if (!comp)
				comp = go.AddComponent<T>();
			return comp;
			}
		public void OnPointerClick(PointerEventData eventData) {
			if (debug)
				Debug.LogFormat($"pointerclick  {eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<RectTransform>().anchoredPosition}");
			if (!m_menuOpen)
				Show();
			else
				Hide();
			}

		public void OnSubmit(BaseEventData eventData) {
			Show();
			}

		public void OnCancel(BaseEventData eventData) {
			Hide();
			}

		public virtual void Show() {
			if (m_menuOpen)
				return;
			
			Arrow.localRotation = Quaternion.Euler(0, 0, 90f);
			Arrow.anchoredPosition = new Vector2(Arrow.anchoredPosition.x, -5);
			SetupMenuItems();
			SetupSelectionBar();
			m_SelectionBar.gameObject.SetActive(true);
			m_menuOpen = true;
			}
		public void Hide() {
			foreach (GameObject gameObject in m_MenuItems) {
				m_menuElemPool.AddItem(gameObject);
				}
			m_SelectionBar.gameObject.SetActive(false);
			Arrow.localRotation = Quaternion.identity;
			Arrow.anchoredPosition = new Vector2(Arrow.anchoredPosition.x, 5);
			m_menuOpen = false;
			}
		protected virtual GameObject CreateDropdownList(GameObject template) {
			return (GameObject)Instantiate(template);
			}

		/// <summary>
		/// Convenience method to explicitly destroy the previously generated dropdown list
		/// </summary>
		/// <remarks>
		/// Override this method to implement a different way to dispose of a dropdown list GameObject.
		/// </remarks>
		/// <param name="dropdownList">The dropdown list GameObject to destroy</param>
		protected virtual void DestroyDropdownList(GameObject dropdownList) {
			// Destroy(dropdownList);
			}
		}

	[Serializable]
	/// <summary>
	/// Class used internally to store the list of options for the dropdown list.
	/// </summary>
	/// <remarks>
	/// The usage of this class is not exposed in the runtime API. It's only relevant for the PropertyDrawer drawing the list of options.
	/// </remarks>
	public class OptionDataList {
		[SerializeField]
		private List<OptionData> m_Options;

		/// <summary>
		/// The list of options for the dropdown list.
		/// </summary>
		public List<OptionData> options { get { return m_Options; } set { m_Options = value; } }


		public OptionDataList() {
			options = new List<OptionData>();
			}
		}


	[Serializable]
	/// <summary>
	/// Class to store the text and/or image of a single option in the dropdown list.
	/// </summary>
	public class OptionData {
		[SerializeField]
		private string m_Text;
		[SerializeField]
		private Sprite m_Image;

		/// <summary>
		/// The text associated with the option.
		/// </summary>
		public string text { get { return m_Text; } set { m_Text = value; } }

		/// <summary>
		/// The image associated with the option.
		/// </summary>
		public Sprite image { get { return m_Image; } set { m_Image = value; } }

		public OptionData() { }

		public OptionData(string text) {
			this.text = text;
			}

		public OptionData(Sprite image) {
			this.image = image;
			}

		/// <summary>
		/// Create an object representing a single option for the dropdown list.
		/// </summary>
		/// <param name="text">Optional text for the option.</param>
		/// <param name="image">Optional image for the option.</param>
		public OptionData(string text, Sprite image) {
			this.text = text;
			this.image = image;
			}
		}
	}