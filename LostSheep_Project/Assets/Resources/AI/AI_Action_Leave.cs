using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using GameCommon;

namespace GameLogic
{
	[TaskCategory("Custom/Actor")]
	[TaskDescription("酒客离开")]
	public class AI_Action_Leave : Action {

		L_Character_Drinker drinker;
		L_Actor_Seat seat;
		//
		public override void OnStart(){
			drinker = gameObject.GetComponent<L_Character_Drinker> ();
			seat = L_ActorManager.It.Find<L_Actor_Seat> (drinker.SeatID);
			GameEventMachine.SendEvent (GameEventID.Event_Drinker_LeaveSeat,drinker.SeatID,drinker.ID);

			L_Actor_Door door = L_ActorManager.It.ActorRoot.GetComponentInChildren<L_Actor_Door>();
			drinker.MoveTarget = door.ExitTran;
		}

		public override TaskStatus OnUpdate ()
		{
			if (!drinker.IsArrive) return TaskStatus.Running;
			return TaskStatus.Success;
		}
	}
}