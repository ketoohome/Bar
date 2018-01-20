using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using GameCommon;

namespace GameLogic
{
	[TaskCategory("Custom/Actor")]
	[TaskDescription("酒客买单")]
	public class AI_Action_DrinkerDrink : Action {
		L_Character_Drinker drinker;
		L_Item_Cocktail cocktail;
		//
		public override void OnStart(){
			drinker = gameObject.GetComponent<L_Character_Drinker> ();
			cocktail = L_ActorManager.It.Find<L_Item_Cocktail> (drinker.CocktailID);
		}

		public override TaskStatus OnUpdate ()
		{
			float remain = cocktail.Remain;
			cocktail.Remain = Mathf.Max (remain - Time.deltaTime, 0);
			if (remain == 0) return TaskStatus.Success;
			else return TaskStatus.Running;
		}
	}
}