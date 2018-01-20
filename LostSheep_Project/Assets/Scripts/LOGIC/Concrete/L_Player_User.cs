using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameCommon;
using TOOL;

namespace GameLogic{
	public class L_Player_User : L_Player {

		protected override void Birth ()
		{
			GameEventMachine.Register(GameEventID.Event_ChangeMoney,ChangeMoney);

			// 注册属性(根据表格内容注册相关属性)
			m_AttributeRoot.CreatChildData("name","DK"); 						// 名字
			m_AttributeRoot.CreatChildData("money",(int)0); 					// 钱
			// ...

			// 平均每秒调用一次钱的数量
			ClockMachine.It.CreateClock(1,PreAddMoney);
		}

		protected override void Dead(){
			GameEventMachine.Unregister(GameEventID.Event_ChangeMoney,ChangeMoney);
		}

		/// <summary>
		/// 更新，控制
		/// </summary>
		public override void CustomUpdate(){}

		/// <summary>
		/// 升级/解锁材料
		/// </summary>
		/// <param name="id">材料ID</param>
		bool LevelUpStock(uint id){
			return true;
		}

		/// <summary>
		/// 提升调酒师等级
		/// </summary>
		/// <returns><c>true</c>, if up barman was leveled, <c>false</c> otherwise.</returns>
		bool LevelUpBarman(){
			return true;
		}

		/// <summary>
		/// 升级/解锁侍者
		/// </summary>
		/// <returns><c>true</c>, if up waiter was leveled, <c>false</c> otherwise.</returns>
		bool LevelUpWaiter(){
			return true;
		}

		/// <summary>
		/// 升级酒吧
		/// </summary>
		/// <returns><c>true</c>, if up bar was leveled, <c>false</c> otherwise.</returns>
		bool LevelUpBar(){
			return true;
		}

		/// <summary>
		/// 创建鸡尾酒
		/// </summary>
		/// <value>鸡尾酒ID</value>
		bool CreateCocktall(uint id){
			return true;
		}

		void ChangeMoney(params object[] args){
			L_Attribute att = m_AttributeRoot.FindChild("money");
			att.Value = (int)att.Value + (int)args[0];
			GameEventMachine.SendEvent(GameEventID.Event_2UI_ChangeMoney,att.Value,(int)args[0]);
		}

		/// <summary>
		/// 每一秒固定增长的钱的数量
		/// </summary>
		void PreAddMoney(){
			// 增加的进步数量（计算公式？）
			int increment = 1; 
			// 
			L_Attribute att = m_AttributeRoot.FindChild("money");
			att.Value = (int)att.Value + increment;

			// 通知UI钱的数量发生了改变
			GameEventMachine.SendEvent(GameEventID.Event_2UI_ChangeMoney,att.Value,increment);
			// 平均每秒调用一次
			ClockMachine.It.CreateClock(1,PreAddMoney);
		}
	}
}