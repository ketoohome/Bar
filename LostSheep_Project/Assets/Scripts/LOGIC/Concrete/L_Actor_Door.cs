using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using TOOL;

namespace GameLogic{
	/// <summary>
	/// 座位，侍者和顾客的酒客的位置，是否已经有人占据
	/// </summary>
	public class L_Actor_Door : L_Actor {
		Transform m_Enter,m_Exit;
		public Transform EnterTran{get{ return m_Enter;}}
		public Transform ExitTran{get{ return m_Exit;}}

		int m_LayerMask;
		protected override void Birth (){
			m_Enter = transform.FindChild ("Enter");
			m_Exit = transform.FindChild ("Exit");
			m_LayerMask = 1 << LayerMask.NameToLayer ("Character");

			m_Enter.GetComponent<Renderer> ().enabled = false;
			m_Exit.GetComponent<Renderer> ().enabled = false;
		}

		public override void CustomUpdate (){}

		/// <summary>
		/// 当前座位是否被占据 
		/// </summary>
		/// <value><c>true</c> if this instance is occupied; otherwise, <c>false</c>.</value>
		public bool IsEmpty{ 
			get{ return Physics.OverlapBox (transform.position, transform.localScale * 0.5f, transform.rotation, m_LayerMask).Length == 0; }
		}

		/// <summary>
		/// 获得所有的座位
		/// </summary>
		/// <returns>The all aeats.</returns>
		public static L_Actor_Door[] GetAllDoors(){
			GameObject actors = L_ActorManager.It.ActorRoot;
			return actors.GetComponentsInChildren<L_Actor_Door> ();
		}
	}
}