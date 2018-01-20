using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using GameCommon;

namespace GameLogic
{
	[TaskCategory("Custom/Actor")]
	[TaskDescription("调酒师等待")]
	public class AI_Action_BarmanWait : Action {

		public SharedGameObjectList m_Drinkerlist;
		public SharedGameObject m_CurrDrinker;
		//

		public override TaskStatus OnUpdate ()
		{
			if (m_Drinkerlist.Value.Count > 0) {
				m_CurrDrinker.Value = m_Drinkerlist.Value [0];
				return TaskStatus.Success;
			}
			return TaskStatus.Running;
		}
	}
}