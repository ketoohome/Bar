using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using GameCommon;
using TOOL;

namespace GameLogic{
	/// <summary>
	/// 角色类，可根据目标移动，角色之间可以产生对话
	/// </summary>
	public abstract class L_Character : L_Actor {



		/// <summary>
		/// 移动代理
		/// </summary>
		NavMeshAgent m_NavMeshAgent = null;
		protected NavMeshAgent MoveAgent{
			get{
				if (m_NavMeshAgent == null) {
					m_NavMeshAgent = GetComponent<NavMeshAgent> ();
					// 设置碰撞优先级
					//m_NavMeshAgent.avoidancePriority = NavPrioirty;
					//NavPrioirty = (NavPrioirty == 70) ? 0 : NavPrioirty+1;
				}
				return m_NavMeshAgent;
			}
		}

		/// <summary>
		/// 移动目标
		/// </summary>
		Transform m_MoveTarget = null;
		public Transform MoveTarget{
			get{ return m_MoveTarget;}
			set{ m_MoveTarget = value; 
				 MoveAgent.SetDestination (m_MoveTarget.position);
				 m_Lock = true;
				 ClockMachine.It.CreateClock (0.1f,()=>{m_Lock = false;});
			}
		}

		// 
		bool m_Lock = false;
		/// <summary>
		/// 是否到达目的地
		/// </summary>
		/// <value><c>true</c> if this instance is arrive; otherwise, <c>false</c>.</value>
		public bool IsArrive{
			get{
				if (m_Lock) return false;
				if (Mathf.Abs (MoveAgent.remainingDistance) > 0.01f) return false;
				if (Vector3.Angle (m_MoveTarget.transform.forward, transform.forward) > 1.0f) {
					transform.forward = Vector3.Lerp (transform.forward,m_MoveTarget.transform.forward,Time.deltaTime*10);
					return false;				
				} 
				return true;
			}
		}

		public override void CustomUpdate (){}

		// 如果两个角色碰撞，通过改变优先级来避免碰撞
		void OnCollisionEnter(Collision collision){
			int priority = MoveAgent.avoidancePriority;
			MoveAgent.avoidancePriority = Random.Range(priority,priority+10);
		}
	}
}