using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using GameCommon;

namespace GameLogic
{
	[TaskCategory("Custom/Actor")]
	[TaskDescription("调酒师招待")]
	public class AI_Action_BarmanCreateCocktail : Action {

		public SharedGameObject m_CurrDrinker;
		public SharedGameObjectList m_Drinkers;

		public override void OnStart ()
		{
			// 获得位置
			uint seatID = m_CurrDrinker.Value.GetComponent<L_Character_Drinker> ().SeatID;
			Transform tran = L_ActorManager.It.Find<L_Actor_Seat> (seatID).WinePos;
			// 创作鸡尾酒
			uint drinkerID = m_CurrDrinker.Value.GetComponent<L_Actor>().ID;
			gameObject.GetComponent<L_Character_Barman> ().CreateCocktail (0,tran.position,tran.rotation,drinkerID);
			m_Drinkers.Value.Remove (m_CurrDrinker.Value);
			m_CurrDrinker.Value = null;
		}
	}
}