using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameLogic{
	/// <summary>
	/// 物品预制数据存放点
	/// </summary>
	public class L_ItemData : ScriptableObject {

		/// <summary>
		/// 单件
		/// </summary>
		static L_ItemData Instance = null;


		[System.Serializable]
		struct ItemContainer{
			public int index;
			public L_Item item;
		}

		[SerializeField]
		List<ItemContainer> m_Items = new List<ItemContainer>();

		/// <summary>
		/// 获得角色的预制体
		/// </summary>
		/// <returns>The character prefab.</returns>
		public GameObject GetItemPrefab(int index){
			for(int i = 0; i < m_Items.Count; i++){
				if(m_Items[i].index == index){
					return m_Items[i].item.gameObject;
				}
			}
			return null;
		}

		public GameObject GetItemPrefab(string name){
			for(int i = 0; i < m_Items.Count; i++){
				if(m_Items[i].item.name == name){
					return m_Items[i].item.gameObject;
				}
			}
			return null;
		}

		/// <summary>
		/// 创建一个角色
		/// </summary>
		/// <returns>The character prefab.</returns>
		/// <param name="index">Index.</param>
		/// <param name="pos">Position.</param>
		/// <param name="quat">Quat.</param>
		public static L_Item CreateItem(int index,Vector3 pos,Quaternion quat){
			if(Instance == null) 
				Instance = Resources.Load<L_ItemData>("SerializeableData/ItemData");
			GameObject prefab = Instance.GetItemPrefab(index) as GameObject;
			if(prefab == null) return null;
			GameObject obj = Instantiate(prefab,pos,quat) as GameObject;
			return obj.GetComponent<L_Item> ();
		}

		public static L_Item CreateItem(string name,Vector3 pos,Quaternion quat){
			if(Instance == null) 
				Instance = Resources.Load<L_ItemData>("SerializeableData/ItemData");
			GameObject prefab = Instance.GetItemPrefab(name) as GameObject;
			if(prefab == null) return null;
			GameObject obj = Instantiate(prefab,pos,quat) as GameObject;
			return obj.GetComponent<L_Item> ();
		}
	}
}