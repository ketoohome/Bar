using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using GameCommon;

namespace GameLogic
{
	[TaskCategory("Custom/Actor")]
	[TaskDescription("酒客呼叫调酒师")]
	public class AI_Action_DrinkerCall : Action {

		L_Actor seat;
		bool IsReceive;
		//
		public override void OnStart(){

			IsReceive = false;
			uint seatid = GetComponent<L_Character_Drinker> ().SeatID; 
			seat = L_ActorManager.It.Find (seatid);
			GameEventMachine.SendEvent(GameEventID.Event_Drinker_CallBarman,gameObject,seat.gameObject);

			GameEventMachine.Register (GameEventID.Event_Barman_Tend,OnBarmanTend);
		}

		public override void OnEnd ()
		{
			IsReceive = true;
			GameEventMachine.Unregister (GameEventID.Event_Barman_Tend,OnBarmanTend);
		}

		public override TaskStatus OnUpdate ()
		{	
			if(IsReceive) return TaskStatus.Success;
			return TaskStatus.Running;
		}

		void OnBarmanTend(params object[] args){
			if (gameObject != (GameObject)args [0])
				return;
			IsReceive = true;
		}
	}
}