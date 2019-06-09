using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using TOOL;
using GameCommon;

namespace GameLogic{
	/// <summary>
	/// 座位，侍者和顾客的酒客的位置，是否已经有人占据
	/// </summary>
	public class L_Actor_Seat : L_Actor {
		Transform m_Barman_Seat,m_Drinker_Seat,m_Wine_Position;
		public Transform BarmanSet{ get{return m_Barman_Seat;}}		// Barman 位置
		public Transform DrinkerSet{ get{return m_Drinker_Seat;}}	// Drink  位置
		public Transform WinePos{ get{return m_Wine_Position;}}		// Wine   位置
		int m_LayerMask;
		protected override void Birth (){
			m_Barman_Seat = transform.Find("Barman_Seat");
			m_Drinker_Seat = transform.Find("Drinker_Seat");
			m_Wine_Position = transform.Find ("Wine_Position");
			m_LayerMask = 1 << LayerMask.NameToLayer ("Character");

			m_Barman_Seat.GetComponent<Renderer> ().enabled = false;
			m_Drinker_Seat.GetComponent<Renderer> ().enabled = false;
			m_Wine_Position.GetComponent<Renderer> ().enabled = false;

			GameEventMachine.Register (GameEventID.Event_Drinker_FindSeat,OnDrinkerFindSeat);
			GameEventMachine.Register (GameEventID.Event_Drinker_LeaveSeat,OnDrinkerLeaveSeat);
		}

		protected override void Dead ()
		{
			GameEventMachine.Unregister (GameEventID.Event_Drinker_FindSeat,OnDrinkerFindSeat);
			GameEventMachine.Unregister (GameEventID.Event_Drinker_LeaveSeat,OnDrinkerLeaveSeat);
		}

		public override void CustomUpdate (){}

		/// <summary>
		/// 占领位置的酒客ID
		/// </summary>
		protected uint m_DrinkerID = NoID;
		public uint DrinkerID{
			get{ return m_DrinkerID;}
		}

		/// <summary>
		/// 当前座位是否被占据 
		/// </summary>
		/// <value><c>true</c> if this instance is occupied; otherwise, <c>false</c>.</value>
		public bool IsEmpty{ 
			get{ return m_DrinkerID == NoID;
				//return Physics.OverlapBox (transform.position, transform.localScale * 0.5f, transform.rotation, m_LayerMask).Length == 0;
			}
		}

		/// <summary>
		/// 获得所有的座位
		/// </summary>
		/// <returns>The all aeats.</returns>
		public static L_Actor_Seat[] GetAllAeats(){
			GameObject actors = L_ActorManager.It.ActorRoot;
			return actors.GetComponentsInChildren<L_Actor_Seat> ();
		}

		/// <summary>
		/// 找到位置
		/// </summary>
		/// <param name="args">Arguments.</param>
		void OnDrinkerFindSeat(params object[] args){
			if (ID != (uint)args [0]) return;
			m_DrinkerID = (uint)args [1];
		}

		/// <summary>
		/// 离开位置
		/// </summary>
		/// <param name="args">Arguments.</param>
		void OnDrinkerLeaveSeat(params object[] args){
			if (ID != (uint)args [0]) return;
			m_DrinkerID = NoID;
		}
	}
}