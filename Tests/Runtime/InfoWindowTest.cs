using System.Collections;
using System.Collections.Generic;
using TGP.Utilities.Ui;
using UnityEngine;

namespace TGP.Tests {
	public class InfoWindowTest : MonoBehaviour {
		public void OpenWithConfirm() {
			DialogueWindowInfo.OnOpenDialogueWindow(this, new DialogueWindowInfoEventArgs("info", "WithConfirm", true));

		}
		public void OpenWithConfirmN0Button() {
			DialogueWindowInfo.OnOpenDialogueWindow(this, new DialogueWindowInfoEventArgs("info", "WithConfirm", true, string.Empty));
		}
		public void OpenAutoClose() {
			DialogueWindowInfo.OnOpenDialogueWindow(this, new DialogueWindowInfoEventArgs("info", "AutoHide"));
		}
		public void Confirm() {
			DialogueWindowInfo.OnCloseDialogueWindow(this, new());
		}
	}
}
