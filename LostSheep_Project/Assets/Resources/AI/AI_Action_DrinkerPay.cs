using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using GameCommon;

namespace GameLogic
{
	[TaskCategory("Custom/Actor")]
	[TaskDescription("酒客买单")]
	public class AI_Action_DrinkerPay : Action {
		public override TaskStatus OnUpdate ()
		{
			gameObject.GetComponent<L_Character_Drinker> ().Pay(100);
			return TaskStatus.Success;
		}
	}
}