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
        internal static string SplitName(GameObject go, int indexToReturn) {
            string[] splitString = go.name.Split('_');
            //for (int i = 0; i < splitString.Length; i++) {
            //    Debug.LogFormat($"string: {splitString[i]} obj: {this.gameObject.name} arrlength: {splitString.Length}");
            //    }
            return splitString[indexToReturn];
            }

       /// <summary>
       /// Gets the slideid from Object name.
       /// NamingConvention: Item_id_Name:
       /// numbers must be separated by "." eg: Button_1.2.45.2_RedButton;
       /// </summary>
       /// <param name="nameObject"></param>
       /// <returns></returns>
        public static int[] GetStateIdFromName(GameObject nameObject) {
            string id = SplitName(nameObject, 1);
            string[] idArr = id.Split('.');
            List<int> idList = new List<int>();
            for (int i = 0; i < idArr.Length; i++) {
                try {
                    string val = idArr[i];
                    // Debug.LogFormat($" val: {val}");
                    idList.Add(int.Parse(val));
                    }
                catch (Exception) {
                    Debug.LogWarningFormat($"could not convert to int: {nameObject.name}");
                    throw;
                    }
                }
            return idList.ToArray();
            }
        /// <summary>
        /// trims the array to the length of the nput
        /// </summary>
        /// <param name="id"></param>
        /// <param name="length"></param>
        /// <returns>trimmed array</returns>
        public static int[] TrimIdToInputLength(int[] id, int length) {
            if (id.Length < length)
                return new int[length];
            int[] tmp = new int[length];
            Array.Copy(id, 0, tmp, 0, length);
            // Debug.LogFormat($" id: {string.Join(",",id)} lenth: {length} new: {string.Join(",",tmp)}");

            return tmp;
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