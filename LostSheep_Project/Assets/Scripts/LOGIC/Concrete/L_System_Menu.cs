using UnityEngine;
using System.Collections;
using GameCommon;
using TOOL;

namespace GameLogic{
	public class L_System_Menu : L_System {

		public override void Start(){
			// 注册监听函数
			GameEventMachine.Register(GameEventID.Event_UI_Menu_Play, OnGameStart);
			// 创建一个UI
			GameEventMachine.SendEvent(GameEventID.Event_UI_Create,UIType.UIMenu);
		} 

		public override void End(){
			// 移除一个UI
			GameEventMachine.SendEvent(GameEventID.Event_UI_Delete,UIType.UIMenu);
			// 注销监听函数
			GameEventMachine.Unregister(GameEventID.Event_UI_Menu_Play, OnGameStart);
		}

		public override void CustomUpdate () {}

		void OnGameStart(params object[] args){
			L_GameRoot.ChangeState(GameState.GS_Play);
		}
	}
}