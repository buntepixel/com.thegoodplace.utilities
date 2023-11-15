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
        public abstract Sequence GetTweenSequenceIn(T property,  TweenCallback onComplete);
        public abstract Sequence GetTweenSequenceOut(T property,  TweenCallback onComplete);
    }
    public abstract class TweenableValueSo2<P,V> : ScriptableObject {
		[Expandable]
		public TweenableSettingsSO Settings;
		public abstract Sequence GetTweenSequenceIn(P property, V value,TweenCallback onComplete);
		public abstract Sequence GetTweenSequenceOut(P property, V value, TweenCallback onComplete);
	}
	
}
