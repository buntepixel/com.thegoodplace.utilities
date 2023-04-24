using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TGP.Utilities {
	public class TriggerTest : MonoBehaviour {
		public string logText;
		public void Execute() {
			Debug.LogFormat($" executing TRIGGER TEST on: {this.gameObject.name} Text: {logText}");
			}
		}
	}

