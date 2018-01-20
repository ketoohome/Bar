using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameCommon;
using TOOL;

namespace GameUI{
	
	public class UI_Root : U3DSingleton<UI_Root> {

		void Awake () {

			// 如果已经初始化了，则不执行一下内容
			if (!IsSingle) { Destroy(gameObject); return; }

			DontDestroyOnLoad(gameObject);	// 主逻辑所挂接的gameObject不能在切换场景的时候删除。

			UIRegister();	// 注册UI

			// 注册事件
			GameEventMachine.Register(GameEventID.Event_UI_Create,OnCreateUI);
			GameEventMachine.Register(GameEventID.Event_UI_Delete,OnDeleteUI);
			GameEventMachine.Register(GameEventID.Event_UI_DeleteAll,OnDeleteAllUI);
		}

		void OnDestroy(){
			// 注销事件
			GameEventMachine.Unregister(GameEventID.Event_UI_Create,OnCreateUI);
			GameEventMachine.Unregister(GameEventID.Event_UI_Delete,OnDeleteUI);
			GameEventMachine.Unregister(GameEventID.Event_UI_DeleteAll,OnDeleteAllUI);
		}

		/// <summary>
		/// Raises the create U event.
		/// </summary>
		/// <param name="args">Arguments.</param>
		void OnCreateUI(params object[] args){
			CreateUI((UIType)args[0]);
		}

		void OnDeleteUI(params object[] args){
			DeleteUI((UIType)args[0]);
		}

		void OnDeleteAllUI(params object[] args){
			DeleteAllUI();
		}

		/// <summary>
		/// The _dict.
		/// </summary>
		static readonly Dictionary<int, System.Type> _dict = new Dictionary<int, System.Type>();

		/// <summary>
		/// Creates the U.
		/// </summary>
		/// <param name="type">Type.</param>
		void CreateUI(UIType id){
			System.Type type = null;
			if (_dict.TryGetValue((int)id, out type)){
				// 1. 得到相应Prefab的路径
				string path = "Prefabs/UI/" + type.Name;
				// 2. 实例化Prefab并放入UI_Root下
				GameObject prefab = Resources.Load<GameObject>(path);
				if(prefab == null) GameCommon.GameCommon.LogError("找不到UI预制体： " + path);

				GameObject obj = GameObject.Instantiate<GameObject>(prefab);
				obj.transform.SetParent(transform);
				obj.GetComponent<RectTransform>().localPosition = Vector3.zero;
				obj.GetComponent<RectTransform>().sizeDelta = Vector2.one;
				obj.transform.localScale = Vector3.one;
			}
		}

		/// <summary>
		/// Deletes the U.
		/// </summary>
		void DeleteUI(UIType id){
			System.Type type = null;
			if (_dict.TryGetValue((int)id, out type)){
				// 1. 查找实例
				UI_Base obj = gameObject.GetComponentInChildren(type) as UI_Base;
				// 2. 移除实例
				if(obj != null) GameObject.Destroy(obj.gameObject);
			}
		}

		/// <summary>
		/// Deletes all U.
		/// </summary>
		void DeleteAllUI(){
			Component[] uis = gameObject.GetComponentsInChildren<UI_Base>();
			for(int i = uis.Length - 1;i>=0;i--){
				GameObject.Destroy(uis[i].gameObject);
			}
		}

		/// <summary>
		/// Registers the U.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public void RegisterUI<T> (UIType id) where T : UI_Base {
			var type = typeof(T);
			_dict.Add((int)id,type);
		}

		/// <summary>
		/// User interfaces the register.
		/// </summary>
		void UIRegister(){
			RegisterUI<UI_Menu>(UIType.UIMenu);
			RegisterUI<UI_Play>(UIType.UIPlay);
		}
	}
}

