using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGP.Utilities {
	public class TweenableMaterial : BaseTransition2<Material> {
		public Material[] materials;

		protected override void Start() {
			if (materials.Length == 0)
				Debug.LogWarning("you must specify the material you want to tweak");
			materials = GetComponent<Renderer>().materials;

			base.Start();
			if (startOut) {
				MaterialSo mat = TweenValue as MaterialSo;
				for (int i = 0; i < mat.MaterialValues.Length; i++)
					foreach (MaterialValueGroup item in mat.MaterialValues) {
						if (item.color)
							materials[i].color = item.EndColor;
						if (item.fade)
							materials[i].color = new Color(materials[0].color.r, materials[0].color.g, materials[0].color.b, item.EndFadeVal);
						if (item.custParam) {
							foreach (FloatVal val in item.FloatParam) {
								materials[i].SetFloat(val.Property, val.FValue);
							}
						}
					}

			}
		}
		//#if UNITY_EDITOR
		//		private void OnGUI() {
		//			if (materials.Length==0) {
		//				Debug.Log("hello");
		//				materials = GetComponent<Renderer>().materials;
		//			}
		//		}
		//#endif
		protected override void SetupSequenc() {
			MaterialSo Material = TweenValue as MaterialSo;
			if (Material != null) {
				inSequ = Material.GetTweenSequence(materials[0], () => OnTransitionFinished());
				outSequ = Material.GetTweenSequence(materials[0], () => OnTransitionFinished(), false);
			} else {
				Debug.LogWarning($"Could not find an MaterialSo on {this.gameObject.name}");
			}

		}
	}
}
