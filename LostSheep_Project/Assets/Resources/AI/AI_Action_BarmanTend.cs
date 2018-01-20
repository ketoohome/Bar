using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using GameCommon;

namespace GameLogic
{
	[TaskCategory("Custom/Actor")]
	[TaskDescription("调酒师招待")]
	public class AI_Action_BarmanTend : Action {

		public SharedGameObject m_CurrDrinker;

		L_Character charachter;

		public override void OnStart ()
		{
			// 确定去哪个目标招待
			uint seatid = m_CurrDrinker.Value.GetComponent<L_Character_Drinker> ().SeatID; 
			charachter = gameObject.GetComponent<L_Character> ();
			charachter.MoveTarget = L_ActorManager.It.Find<L_Actor_Seat>(seatid).BarmanSet;
		}

		public override TaskStatus OnUpdate ()
		{
			if (!charachter.IsArrive) return TaskStatus.Running;
			GameEventMachine.SendEvent (GameEventID.Event_Barman_Tend,m_CurrDrinker.Value.gameObject);
			return TaskStatus.Success;
		}
	}
}