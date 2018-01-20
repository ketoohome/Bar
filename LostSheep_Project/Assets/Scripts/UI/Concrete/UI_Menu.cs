using UnityEngine;
using System.Collections;
using GameCommon;

namespace GameUI{

	public class UI_Menu : UI_Base {
		public void OnGamePlay(){
			GameEventMachine.SendEvent(GameEventID.Event_UI_Menu_Play);
		}
	}
}