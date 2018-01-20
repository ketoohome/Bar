using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using GameCommon;

namespace GameLogic
{
	[TaskCategory("Custom/Actor")]
	[TaskDescription("酒客点酒")]
	public class AI_Action_DrinkerPoint : Action {

		L_Character_Drinker m_Drinker;
		//
		public override void OnStart(){
			m_Drinker = gameObject.GetComponent<L_Character_Drinker> ();
			m_Drinker.PointMenu ();
		}

		public override TaskStatus OnUpdate ()
		{
			return m_Drinker.CocktailID != 0 ? TaskStatus.Success : TaskStatus.Running;
		}
	}
}