using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace Cf.Utilities {

	static  class ClassExtensions  {
        public static bool EqualSequence(this int[] arr, int[] value) {
            return arr.SequenceEqual(value);
			}


        public static byte[] GetBytesFromStream(Stream input) {
            using (MemoryStream ms = new MemoryStream()) {
                input.CopyTo(ms);
                return ms.ToArray();
                }
            }
        }

    }
