using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace TGP.Utilities {

	static public class ClassExtensions  {
       
        public static byte[] GetBytesFromStream(Stream input) {
            using (MemoryStream ms = new MemoryStream()) {
                input.CopyTo(ms);
                return ms.ToArray();
                }
            }
        }

    }
