using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;

namespace GameLogic
{
	[TaskCategory("Custom/Other")]
	[TaskDescription("酒是否被喝完")]  
	public class AI_Conditionals_CocktailEmpty : Conditional {

		// Update is called once per frame
		public override TaskStatus OnUpdate (){
			//float v = GetComponent<L_Item_Cocktail> ().Remain;
			//Debug.LogWarning (gameObject.name + " : " + v);
			return GetComponent<L_Item_Cocktail> ().Remain == 0 ? TaskStatus.Success : TaskStatus.Failure;
		}
	}
}