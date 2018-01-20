using UnityEngine;
using System.Collections;

namespace GameUI{

	[ExecuteInEditMode]
	/// <summary>
	/// UI基础类型
	/// 	1. 在编辑的时候自动命名为类名，并自动获取预制体路径（序列化）
	/// 	2. 创建的时候自动添加到UI_Root中，删除的时候自动从UI_Root中删除
	/// 	3. 
	/// </summary>
	public abstract class UI_Base : MonoBehaviour {
		/// <summary>
		/// 自删除删除
		/// </summary>
		void DestroySelf(){
			Destroy(gameObject);
		}

#if UNITY_EDITOR
		void Update(){
			if(name != this.GetType().Name) 
				name = this.GetType().Name;
		}
#endif
	}
}
