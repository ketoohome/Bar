using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GameCommon;

namespace GameUI{

	public class UI_Play : UI_Base {
		void Awake(){
			GameEventMachine.Register(GameEventID.Event_2UI_ChangeMoney,OnAddMoney);
		}
		void OnDestroy(){
			GameEventMachine.Unregister(GameEventID.Event_2UI_ChangeMoney,OnAddMoney);
		}

		void OnAddMoney(params object[] args){
			int money = (int)args[0];
			transform.FindChild("Money").GetComponent<Text>().text = "$:" + money;
		}
	}
}