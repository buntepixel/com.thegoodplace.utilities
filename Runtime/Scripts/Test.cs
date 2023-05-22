using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGP.Utilities
{
    public class Test : MonoBehaviour
    {
        [SerializeReference, SubclassPicker] AbstractClass myField;
    }

    public abstract class AbstractClass { }

    public class DerivedClass1 : AbstractClass {
        public float floatField;
    }

    public class DerivedClass2 : AbstractClass {
        public string stringField;
    }
}
