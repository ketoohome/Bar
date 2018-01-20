using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;

namespace GameLogic
{
	[TaskCategory("Custom/Other")]
	[TaskDescription("创建一个酒客")]  
	public class AI_Action_CreateDrinker : Action {

		public override void OnStart ()
		{
			L_Actor_Door door = L_ActorManager.It.ActorRoot.GetComponentInChildren<L_Actor_Door>();
			L_CharacterData.CreateCharacter(0,door.EnterTran.position,door.EnterTran.rotation);
		}
	}
}