using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;

namespace GameLogic
{
	[TaskCategory("Custom/Other")]
	[TaskDescription("门是否被占用了")]  
	public class AI_Conditionals_DoorFull : Conditional {

		bool IsDoorFull;
		public override void OnStart ()
		{
			IsDoorFull = true;
			L_Actor_Door[] doors = L_Actor_Door.GetAllDoors ();
			for (int i = 0; i < doors.Length; i++) {
				if (doors [i].IsEmpty) {
					IsDoorFull = false;
					return;
				}
			}
		}

		// Update is called once per frame
		public override TaskStatus OnUpdate (){
			return IsDoorFull ? TaskStatus.Success : TaskStatus.Failure;
		}
	}
}