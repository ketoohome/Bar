using UnityEngine;
using System.Collections;
using GameCommon;
using TOOL;

namespace GameLogic{
	/// <summary>
	/// 酒客
	/// </summary>
	public class L_Character_Drinker : L_Character {

		/// <summary>
		/// 喜欢的酒品
		/// </summary>
		public DrinkType likeDrinkType = DrinkType.Drink_Water;

		protected override void Birth (){
			Attributes.CreatChildData("name",gameObject.name);
			Attributes.CreatChildData("winetype",likeDrinkType);

			GameEventMachine.Register (GameEventID.Event_Barman_CreateCocktail, OnGetCocktail);
		}

		protected override void Dead ()
		{
			GameEventMachine.Unregister (GameEventID.Event_Barman_CreateCocktail, OnGetCocktail);
		}

		public override void CustomUpdate (){}

		/// <summary>
		/// 座位ID
		/// </summary>
		uint m_SeatID;
		public uint SeatID{ get{ return m_SeatID;} set{ m_SeatID = value;}}

		/// <summary>
		/// 酒ID
		/// </summary>
		uint m_CocktailID;
		public uint CocktailID{ get{ return m_CocktailID;}}

		/// <summary>
		/// 付钱
		/// </summary>
		public virtual void Pay(int cash){
			L_Actor_Seat seat = L_ActorManager.It.Find<L_Actor_Seat> (m_SeatID);
			L_Item item = L_ItemData.CreateItem("Cash",seat.WinePos.position,seat.WinePos.rotation);
			item.GetAttribute ("money").Value = cash;

			GameEventMachine.SendEvent(GameEventID.Event_Drinker_Pay,gameObject,cash);
		}

		/// <summary>
		/// 评估鸡尾酒
		/// </summary>
		public virtual void EvaluateCocktail(){
			
		}

		/// <summary>
		/// 点酒 
		/// </summary>
		public virtual void PointMenu(){
			string name = Attributes.FindChild("name").GetValue<string>();
			DrinkType winetype = Attributes.FindChild("winetype").GetValue<DrinkType>();
			GameEventMachine.SendEvent(GameEventID.Event_Drinker_PointMenu,name,winetype);
		}

		/// <summary>
		/// 得到鸡尾酒
		/// </summary>
		/// <param name="args">Arguments.</param>
		void OnGetCocktail(params object[] args){
			if ((uint)args [0] != ID) return;
			m_CocktailID = (uint)args [1];
		}
	}
}