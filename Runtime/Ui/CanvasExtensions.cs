using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TGP.Utilities {
   static public class CanvasExtensions {
        /// <summary>
        /// bool activates interactable and blockraycast in one go
        /// </summary>
        /// <param name="cg"></param>
        /// <param name="enabled"></param>
        public static void EnableInput(this CanvasGroup cg, bool enabled) {
            cg.interactable = cg.blocksRaycasts = enabled;
            }
        /// <summary>
        /// The bool controls interactable,blockraycast and alpha in one Method. 
        /// enabled = alphaval 1
        /// </summary>
        /// <param name="cg"></param>
        /// <param name="enabled"></param>
        public static void EnableInputVisibility(this CanvasGroup cg, bool enabled) {
            cg.interactable = cg.blocksRaycasts = enabled;
            if (enabled)
                cg.alpha = 1;
            else
                cg.alpha = 0;
            }
        /// <summary>
        /// Hides the canvasgroup and sets blocksRaycas accordingly
        /// </summary>
        /// <param name="cg">CanvasGroup</param>
        /// <param name="hide">bool</param>
        public static void HideCanvasGroup(this CanvasGroup cg, bool hide) {
            if (hide == true)
                cg.alpha = 0;
            else
                cg.alpha = 1;
            cg.blocksRaycasts = !hide;
            }
        }
    }