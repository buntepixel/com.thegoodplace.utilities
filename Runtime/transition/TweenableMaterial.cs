using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGP.Utilities
{
	public class TweenableMaterial : BaseTransition2<Material> {
		public Material[] materials;
		
		protected override void Start() {
			materials = GetComponent<Renderer>().materials;

			base.Start();
			

			if (startOut) {
				MaterialSo mat = TweenValue as MaterialSo;
				if (mat.color)
					materials[0].color = mat.EndColor;
				if (mat.fade)
					materials[0].color = new Color(materials[0].color.r, materials[0].color.g, materials[0].color.b, mat.FadeEndVal);
				if (mat.customParam) {
					foreach(MaterialSo.FloatVal item in mat.FloatParam) {
						materials[0].SetFloat(item.Property, item.FValue);
					}
				}
			}
		}
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
