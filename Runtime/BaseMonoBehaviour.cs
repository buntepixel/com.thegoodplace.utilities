using UnityEngine;
namespace TGP.Utilities {
	public class BaseMonoBehaviour : MonoBehaviour {
		protected string TAG ;
		[SerializeField]
		protected bool debug;

		protected virtual void Awake() {
			TAG = string.Concat("[", Application.productName, "] ");
		}
	}
}