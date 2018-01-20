using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;

namespace GameLogic
{
	[TaskCategory("Custom/Actor")]
	[TaskDescription("删除自己")]
	public class AI_Action_Destroy : Action {
		//
		public override void OnStart(){
			GameObject.Destroy(gameObject);
		}
	}
}