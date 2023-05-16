using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace TGP.Utilities
{
    public abstract class TweenableValueSo : ScriptableObject
    {
        [Expandable]
        public TweenableSettingsSO Settings;
        public abstract Sequence GetTweenSequence(Transform transform, TweenCallback onComplete, bool isIn = true);
    }
}
