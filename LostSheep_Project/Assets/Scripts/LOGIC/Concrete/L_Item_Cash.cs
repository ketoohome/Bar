using UnityEngine;
using System.Collections;
using GameCommon;
using TOOL;

namespace GameLogic{

	/// <summary>
	/// 鸡尾酒
	/// </summary>
	public class L_Item_Cash : L_Item {

		Rigidbody m_rigidbody;
		protected override void Birth ()
		{
			// 当前的钱有多少？
			Attributes.CreatChildData("money",0);		// 酒的等级

			m_rigidbody = GetComponent<Rigidbody> ();
			m_rigidbody.angularVelocity = Vector3.forward * 20;
			m_rigidbody.maxAngularVelocity = 50;
			m_rigidbody.velocity = Vector3.up * 4;
		}

		public override void CustomUpdate (){
			base.CustomUpdate ();
		}

		protected override void Touch(){
			int cash = Attributes.FindChild ("money").GetValue<int>();
			GameEventMachine.SendEvent(GameEventID.Event_ChangeMoney,cash);
			Destroy (gameObject);
		}
	}
}