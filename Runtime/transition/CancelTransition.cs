using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGP.Utilities
{
    [RequireComponent(typeof(BaseTransition2<>))]
    public class CancelTransition : MonoBehaviour
    {
        ICancelableTransition[] BaseTransitions;
        [SerializeField]
        CancelBehaviour CancelBehaviour;
		private void Awake() {
            BaseTransitions = GetComponents<ICancelableTransition>();
            Debug.Log($"basetranstions length: {BaseTransitions.Length}");
		}
		public CancelTransition() {
            Debug.Log($"Canceling Transitions ");

        }
    }
}
