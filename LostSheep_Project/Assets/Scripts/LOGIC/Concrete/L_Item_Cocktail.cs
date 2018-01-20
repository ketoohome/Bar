using UnityEngine;
using System.Collections;
using TOOL;

namespace GameLogic{

	/// <summary>
	/// 材料单
	/// </summary>
	public enum MaterialType{
		Mat_Water = 0,		// 水
		Mat_Beer = 1,		// 啤酒
		Mat_Ice = 2,		// 冰块
	}

	/// <summary>
	/// 菜单.
	/// </summary>
	public enum DrinkType{
		Drink_Water = 0,	// 水
		Drink_IceWater = 1, // 冰水
		Drink_Beer = 1,		// 啤酒
		Drink_IceBeer = 2,	// 加冰啤酒
	}

	/// <summary>
	/// 鸡尾酒
	/// </summary>
	public class L_Item_Cocktail : L_Item {
		
		/// <summary>
		/// 酒的类型
		/// </summary>
		public DrinkType m_DrinkType;

		/// <summary>
		/// 剩余的量
		/// </summary>
		/// <value>The remain.</value>
		public float Remain { 
			get{ return Attributes.FindChild ("remain").GetValue<float> (); } 
			set{ Attributes.FindChild ("remain").Value = value; }
		}

		protected override void Birth ()
		{
			Attributes.CreatChildData("level",0);		// 酒的等级	
			Attributes.CreatChildData("remain",10.0f);	// 酒量
		}

		public override void CustomUpdate (){}
	}
}