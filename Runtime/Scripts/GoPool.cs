using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGP.Utilities {
	public class GoPool : BasePool<GameObject> {
		GameObject poolObj;
		GameObject instanceObj;
		List<GameObject> activeObj;
		public GoPool(GameObject poolParent,GameObject instanceObj,string poolName = "pool") {
			poolObj = new GameObject(poolName);
			poolObj.transform.SetParent(poolParent.transform,false);
			this.instanceObj = instanceObj;
			activeObj = new List<GameObject>();
			}
	
		/// <summary>
		/// Adds an Item to the pool and deactivates it.
		/// </summary>
		/// <param name="item">the item to add to the pool</param>
		public override void AddItem(GameObject item) {
			base.AddItem(item);
			item.transform.SetParent(poolObj.transform);
			item.gameObject.SetActive(false);
			}

		//todo: remove
		/// <summary>
		/// Returns a pool item and parents it to the pool Obj Parent
		/// </summary>
		/// <returns>GameObject</returns>
		public GameObject GetItem() {
			return GetItem(GetPoolRoot().transform);
			}
		/// <summary>
		/// Returns an Item from the pool and activates it.
		/// if there are none it instantiates one
		/// </summary>
		/// <param name="parent">the parent it will be grouped below</param>
		/// <returns>Gameobject</returns>
		public override GameObject GetItem(Transform parent) {
			GameObject item = base.GetItem(parent);
			if (item == null) {
				item = Object.Instantiate(instanceObj, parent, false);
				}
			else
				item.transform.SetParent(parent);
			activeObj.Add(item);
			item.gameObject.SetActive(true);
			return item;
			}
		public void CleanupActivesToPool() {
			if (activeObj.Count == 0)
				return;
			foreach(GameObject item in activeObj) {
				AddItem(item);
				}
			activeObj.Clear();
			}
		public List<GameObject> GetActiveObj() {
			return activeObj;
			}
		
		public GameObject GetPoolRoot() {
			return poolObj.gameObject;
			}

		public override void Dispose() {
			Object.Destroy(poolObj);
		}
	}
	}
