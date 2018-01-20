using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameLogic{
	/// <summary>
	/// 角色预制数据存放点
	/// </summary>
	public class L_CharacterData : ScriptableObject {
		[SerializeField]
		List<CharacterContainer> m_Characters = new List<CharacterContainer>();

		[System.Serializable]
		struct CharacterContainer{
			public int index;
			public L_Character character;
		}

		/// <summary>
		/// 获得角色的预制体
		/// </summary>
		/// <returns>The character prefab.</returns>
		public GameObject GetCharacterPrefab(int index){
			for(int i = 0; i < m_Characters.Count; i++){
				if(m_Characters[i].index == index){
					return m_Characters[i].character.gameObject;
				}
			}
			return null;
		}

		public GameObject GetCharacterPrefab(string name){
			for(int i = 0; i < m_Characters.Count; i++){
				if(m_Characters[i].character.name == name){
					return m_Characters[i].character.gameObject;
				}
			}
			return null;
		}

		/// <summary>
		/// 单件
		/// </summary>
		static L_CharacterData Instance = null;

		/// <summary>
		/// 创建一个角色
		/// </summary>
		/// <returns>The character prefab.</returns>
		/// <param name="index">Index.</param>
		/// <param name="pos">Position.</param>
		/// <param name="quat">Quat.</param>
		public static GameObject CreateCharacter(int index,Vector3 pos,Quaternion quat){
			if(Instance == null) 
				Instance = Resources.Load<L_CharacterData>("SerializeableData/CharacterData");
			GameObject prefab = Instance.GetCharacterPrefab(index) as GameObject;
			if(prefab == null) return null;
			return GameObject.Instantiate(prefab,pos,quat) as GameObject;
		}

		public static GameObject CreateCharacter(string name,Vector3 pos,Quaternion quat){
			if(Instance == null) 
				Instance = Resources.Load<L_CharacterData>("SerializeableData/CharacterData");
			GameObject prefab = Instance.GetCharacterPrefab(name) as GameObject;
			if(prefab == null) return null;
			return GameObject.Instantiate(prefab,pos,quat) as GameObject;
		}
	}
}