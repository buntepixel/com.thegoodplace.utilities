
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


namespace TGP.Utilities {
	public interface IPooling<T> where T : class {
		T GetItem(Transform parent);
		void AddItem(T item);
	}

	public abstract class BasePool<T> :IDisposable,IPooling<T> where T : class {
		public int Count { get { return pool.Count; } }
		protected List<T> pool;
		public BasePool() {
			pool = new List<T>();
		}

		public virtual T GetItem(Transform parent) {
			T item = null;
			if (pool.Count > 0) {
				item = pool[0];
				pool.Remove(item);
			}
			return item;
		}

		public virtual void AddItem(T item) {
			pool.Add(item);
		}

		public abstract void Dispose();
	}
}