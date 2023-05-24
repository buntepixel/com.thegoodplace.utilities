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
				//for(int i = 0;i<mat.Mv.Length;i++)
				//foreach(MaterialSo.MaterialValueGroup item in mat.Mv) {
				//	if (item.color)
				//		materials[i].color =item.EndColor;
				//	if (item.fade)
				//		materials[i].color = new Color(materials[0].color.r, materials[0].color.g, materials[0].color.b, item.FadeEndVal);
				//	if (item.custParam) {
				//		foreach (MaterialSo.FloatVal val in item.FloatParam) {
				//			materials[i].SetFloat(val.Property,val.FValue);
				//		}
				//	}
				//}
				
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
