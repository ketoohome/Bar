using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;

namespace GameLogic
{
	[TaskCategory("Custom/Other")]
	[TaskDescription("房子是否已经满了")]  
	public class AI_Conditionals_HouseFull : Conditional {

		bool IsHouseFull;
		public override void OnStart ()
		{
			IsHouseFull = true;
			L_Actor_Seat[] seats = L_Actor_Seat.GetAllAeats ();
			for (int i = 0; i < seats.Length; i++) {
				if (seats [i].IsEmpty) {
					IsHouseFull = false;
					return;
				}
			}
		}

		// Update is called once per frame
		public override TaskStatus OnUpdate (){
			return IsHouseFull ? TaskStatus.Success : TaskStatus.Failure;
		}
	}
}