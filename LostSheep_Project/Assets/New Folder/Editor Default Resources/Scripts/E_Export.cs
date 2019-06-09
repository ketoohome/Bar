using UnityEngine;   
using UnityEditor;   
using System.Collections.Generic;
using GameLogic;

public class E_Export {
	[MenuItem("Tools/Asset/Export CharacterData")]
	public static void ExecuteChacterData()   
	{                
		//实例化SysData               
		L_CharacterData sd = ScriptableObject.CreateInstance<L_CharacterData>();                                                                 
		// SysData将创建为一个对象，这时在project面板上会看到这个对象。                 
		string p = "Assets/Resources/SerializeableData/CharacterData.asset";        
		AssetDatabase.CreateAsset(sd, p);                                 
	}   

	[MenuItem("Tools/Asset/Export ItemData")]
	public static void ExecuteItemData()   
	{                
		//实例化SysData               
		L_ItemData sd = ScriptableObject.CreateInstance<L_ItemData>();                                                                 
		// SysData将创建为一个对象，这时在project面板上会看到这个对象。                 
		string p = "Assets/Resources/SerializeableData/ItemData.asset";        
		AssetDatabase.CreateAsset(sd, p);                                 
	}   
}
