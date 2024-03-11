using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TGP.Utilities.Ui {
	public class DialogueWindowInfo : DialogueWindowBase {
		public delegate void DialogueWindow_delegate(System.Object sender, DialogueWindowBaseEventArgs context);
		public static DialogueWindow_delegate OnOpenDialogueWindow;
		public static DialogueWindow_delegate OnCloseDialogueWindow;
		[SerializeField]
		TMP_Text headline;
		[SerializeField]
		TMP_Text message;
		[SerializeField]
		Button confirm;
		TMP_Text confirmText;

		protected override void Awake() {
			base.Awake();
			if (headline == null || message == null)
				Debug.LogError($" Serialized Fields are empty on Obj: {this.gameObject.name}");
			if (confirm == null)
				Debug.LogError($" no Button Assigned on Obj: {this.gameObject.name}");
			else
				confirmText = confirm.GetComponentInChildren<TMP_Text>();
			confirm.onClick.AddListener(() => CloseWindow(this, new DialogueWindowBaseEventArgs()));
		}
		private void OnEnable() {
			OnOpenDialogueWindow = OpenWindow;
			OnCloseDialogueWindow = CloseWindow;
		}

		protected override void OpenWindow(object sender, DialogueWindowBaseEventArgs args) {
			if (args == null)
				return;
			DialogueWindowInfoEventArgs myArgs = new DialogueWindowInfoEventArgs();
			try {
				myArgs = args as DialogueWindowInfoEventArgs;
			} catch (Exception ex) {
				Debug.LogError(ex.Message);
				}
			if (isOpen) {
				Debug.LogWarning($"Window already open, change text from:\n{message.text}\nto\n{myArgs.Message}");
			}
			base.OpenWindow(sender, myArgs);
			confirmText.text = myArgs.ConfirmBtnText;
			confirm.gameObject.SetActive(myArgs.Confirm&& myArgs.ConfirmBtnText!=string.Empty);
			headline.text = myArgs.Headline;
			message.text = myArgs.Message;
			if (!myArgs.Confirm)
				AutoHide(myArgs);
			}
	}
	public class DialogueWindowInfoEventArgs : DialogueWindowBaseEventArgs {
		public DialogueWindowInfoEventArgs() {

		}
		public DialogueWindowInfoEventArgs(string headline, string message, bool confirm = false, string confirmBtnMessage = "Ok") : base(3) {
			Confirm = confirm;
			ConfirmBtnText = confirmBtnMessage;
			if (!Confirm)
				DisplayDuration = 5;
			Headline = headline;
			Message = message;
		}
		public string ConfirmBtnText { get; set; }
		public bool Confirm { get; set; }
		public string Headline { get; set; }
		public string Message { get; set; }

	}
}