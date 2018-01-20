using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using GameCommon;

namespace GameLogic
{
	[TaskCategory("Custom/Actor")]
	[TaskDescription("接受酒客的订单队列")]
	public class AI_Action_BarmanQueue : Action {

		public SharedGameObjectList m_Drinkerlist;

		//
		public override void OnStart()
		{
			GameEventMachine.Register(GameEventID.Event_Drinker_CallBarman,OnCallBarman);
			GameEventMachine.Register(GameEventID.Event_Drinker_Pay,OnPay);
		}

		public override void OnEnd ()
		{
			GameEventMachine.Unregister(GameEventID.Event_Drinker_CallBarman,OnCallBarman);
			GameEventMachine.Unregister(GameEventID.Event_Drinker_Pay,OnPay);
		}	

		public override void OnAwake ()
		{
			m_Drinkerlist.Value.Clear();
		}

		public override TaskStatus OnUpdate ()
		{
			return TaskStatus.Running;
		}

		void OnCallBarman(params object[] args){
			m_Drinkerlist.Value.Add((GameObject)args[0]);
		}

		void OnPay(params object[] args){
			m_Drinkerlist.Value.Remove((GameObject)args[0]);
		}
	}
}