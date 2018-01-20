using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameCommon;
using TOOL;

namespace GameLogic{
	/// <summary>
	/// 调酒师，根据当前的材料制作鸡尾酒cocktail
	/// </summary>
	public class L_Character_Barman : L_Character {

		[SerializeField]
		List<L_Item_Cocktail> m_CocktailPrefabs = new List<L_Item_Cocktail>();

		protected override void Birth () {
			gameObject.name = "Barman_" + ID;

			L_Attribute att;
			att = Attributes.CreatChildData("name",gameObject.name);
			att = Attributes.CreatChildData("cocktailsData",1); // 酒单的相关属性 
			{
				// ...
			}
			att = Attributes.CreatChildData("materials",1); // 材料等级
			{
				// ...
			}
			att = Attributes.CreatChildData ("level",1);

		}

		protected override void Dead (){}

		public override void CustomUpdate (){}

		/// <summary>
		/// 制作鸡尾酒
		/// </summary>
		public virtual void CreateCocktail(int index,Vector3 pos,Quaternion quat,uint drinkerID){
			L_Item_Cocktail cocktail = GameObject.Instantiate(m_CocktailPrefabs[0],pos,quat) as L_Item_Cocktail;
			cocktail.GetAttribute ("level").Value = 1;// 临时（材料等级、调酒师等级、熟练度）
			GameEventMachine.SendEvent(GameEventID.Event_Barman_CreateCocktail,drinkerID,cocktail.ID);
		}
	}
}