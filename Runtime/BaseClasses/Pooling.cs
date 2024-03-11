
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Pool;
using System.Xml.Serialization;


namespace TGP.Utilities {
	public interface IPooling<T> where T : UnityEngine.Object {
		T GetItem(Transform parent);
		void AddItem(T item);
		void AddItems(IEnumerable<T> items);
	}

	public abstract class BasePool<T> :IDisposable,IPooling<T> where T : UnityEngine.Object {
		protected T instanceObj;
		protected  GameObject poolParent;
		protected GameObject poolObj;
		
		public int Count { get { return pool.Count; } }
		protected List<T> pool;
		public BasePool(Transform poolParent,T instanceObj,string poolName = "pool") {
			poolObj= new GameObject(poolName);
			poolObj.transform.SetParent(poolParent, false);
			this.instanceObj = instanceObj;
			pool = new List<T>();
		}

		public virtual T GetItem(Transform parent) {
			T item = null;
			if (pool.Count > 0) {
				item = pool[0];
				pool.Remove(item);
			} else {
				item = UnityEngine.Object.Instantiate(instanceObj);
			}
			return item;
		}

		public virtual void AddItem(T item) {
			pool.Add(item);
		}
		public virtual void AddItems(IEnumerable<T> items) {
			foreach(T item in items) {
				AddItem(item);
			}
		}

		public abstract void Dispose();
	}
}