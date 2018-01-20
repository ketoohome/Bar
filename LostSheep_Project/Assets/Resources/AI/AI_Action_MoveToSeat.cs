using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;

namespace GameLogic
{
	[TaskCategory("Custom/Actor")]
	[TaskDescription("酒客寻找座位")]
	public class AI_Action_MoveToSeat : Action {

		L_Character charachter;
		//
		public override void OnStart(){
			uint seatid = GetComponent<L_Character_Drinker> ().SeatID;
			charachter = gameObject.GetComponent<L_Character> ();
			charachter.MoveTarget = L_ActorManager.It.Find<L_Actor_Seat>(seatid).DrinkerSet;
		}

		public override TaskStatus OnUpdate ()
		{
			if (!charachter.IsArrive) return TaskStatus.Running;
			return TaskStatus.Success;
		}
	}
}