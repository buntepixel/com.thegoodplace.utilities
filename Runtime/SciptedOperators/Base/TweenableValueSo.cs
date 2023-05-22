using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace TGP.Utilities
{
    public abstract class TweenableValueSo<T> : ScriptableObject
    {
        [Expandable]
        public TweenableSettingsSO Settings;
        public abstract Sequence GetTweenSequence(T value, TweenCallback onComplete, bool isIn = true);
    }
}
