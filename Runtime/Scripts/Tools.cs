using System;
using System.Text;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGP.Utilities {
	public static class Tools {

		/// <summary>
		/// Splits Name with "_"separator
		/// </summary>
		/// <param name="indexToReturn"></param>
		/// <returns></returns>
		public static string SplitName(GameObject go, int indexToReturn) {
			string[] splitString = go.name.Split('_');
			//for (int i = 0; i < splitString.Length; i++) {
			//    Debug.LogFormat($"string: {splitString[i]} obj: {this.gameObject.name} arrlength: {splitString.Length}");
			//    }
			return splitString[indexToReturn];
		}



		/// <summary>
		/// Sets only the alpha of a color
		/// </summary>
		/// <param name="col">Color</param>
		/// <param name="alphaVal">Alpha</param>
		/// <returns></returns>
		public static Color SetAlpha(this Color col, float alphaVal = 1) {
			Color tmp = col;
			tmp.a = alphaVal;
			//Debug.LogFormat($"extension alphaval: {alphaVal}  colAlpha: {tmp.a}");
			return tmp;
		}
		public static string GenerateRandomString(int length) {
			// creating a StringBuilder object()
			StringBuilder str_build = new StringBuilder();
			System.Random random = new System.Random();
			char letter;

			for (int i = 0; i < length; i++) {
				double flt = random.NextDouble();
				int shift = Convert.ToInt32(Math.Floor(25 * flt));
				letter = Convert.ToChar(shift + 65);
				str_build.Append(letter);
			}
			return str_build.ToString();
		}

	}

}