//Script written by Pratyush Priyadarshi.
//Simple script for billboarding.

using UnityEngine;

namespace TGP.Utilities {
	public class Billboard : MonoBehaviour {

		
		public  Transform ObjToFaceTo; // the Object the player is controlling

		[Header("GameObject Calculations Settings")]
		public float facePlayerfactor = 20f; // angle of facing player
		private void Awake() {
			if(ObjToFaceTo == null)
				ObjToFaceTo = GetComponent<Transform>();
		}
		

		// The function to billboard this object to face the player
		void Update() {
			Vector3 direction = (ObjToFaceTo.position - transform.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * facePlayerfactor);
		}
	}
}