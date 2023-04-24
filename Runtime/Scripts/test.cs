using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cf.Utilities;
using Cf.Utilities.Ui;

public class test : MonoBehaviour {
	public GameObject parent;
	GoPool pool;
	public GameObject instance;

	private void Awake() {
		//pool = new GoPool(parent, instance, "testpool");
		}
	public void Activate() {
		WaitIcon.OnShow(this, new WaitIconEventArgs { Show = true });
		}
	public void DeActivate() {
		WaitIcon.OnShow(this, new WaitIconEventArgs { Show = false });
		}

	}
