using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameCommon;
using TOOL;

namespace GameLogic{
	public class L_System_Play : L_System {

		/// <summary>
		/// 玩家状态
		/// </summary>
		StateMachine<L_System_Play> m_stateMachine;

		public override void Start(){

			// 实例化一个玩家
			L_PlayerManager.It.CreatePlayer<L_Player_User>();

			// 创建一个UI
			GameEventMachine.SendEvent(GameEventID.Event_UI_Create,UIType.UIPlay);

			// 注册事件
			// ...

			// 注册所有状态 
			m_stateMachine = new StateMachine<L_System_Play>(this);
			m_stateMachine.Add(PlayState.PS_Initialize, new PlayState_Initilize());
			m_stateMachine.Add(PlayState.PS_Business, new PlayState_Business());
			// ...
			m_stateMachine.SetCurrentState(PlayState.PS_Initialize); // 设置默认状态
		} 

		public override void End() {
			// 注销事件
			// ...
		}

		public override void CustomUpdate () {
			m_stateMachine.Update();
		}

		public void SetCurrentState(PlayState type){
			m_stateMachine.SetCurrentState(type); // 设置默认状态
		}

		/// <summary>
		/// 装载数据
		/// </summary>
		public void LoadData(){
			// 装载玩家属性
			/*
			L_Data root = L_DataPool.Instance.CreatChildData("Root","Root");

			L_Data test1 = root.CreatChildData("Test1","Test1");
			test1.CreatChildData("Child1",(int)1001);
			test1.CreatChildData("Child2",(float)1.002);
			L_Data test2 = root.CreatChildData("Test2","Test2");
			test2.CreatChildData("Child3",(uint)1003);
			test2.CreatChildData("Child4",(bool)false);
			L_Data test3 = root.CreatChildData("Test3","Test3");
			test3.CreatChildData("Child5",'c');
			test3.CreatChildData("Child6",(byte)255);

			XmlTool.SaveData<L_Data>(root,"Assets/test.xml");

			XmlTool.LoadData<L_Data>(ref root,"Assets/test.xml");
			XmlTool.SaveData<L_Data>(root,"Assets/test1.xml");
			*/

			L_Data root = L_DataPool.Instance.CreatChildData("Root","Root");
			root = CSVTool.LoadCsv<L_Data>(root,"Test");
		}
	}

	/// <summary>
	/// 游戏系统状态类型
	/// </summary>
	public enum PlayState
	{
		PS_Initialize,	// 游戏初始状态
		PS_Business,	// 营业状态
	}


	/// <summary>
	/// 初始化状态
	/// </summary>
	public class PlayState_Initilize : State<L_System_Play> {
		public override void Enter(L_System_Play root){
			// 装载数据
			root.LoadData();
		}

		public override void Execute (L_System_Play root){
			root.SetCurrentState(PlayState.PS_Business);
		}
	}

	/// <summary>
	/// 营业状态
	/// </summary>
	public class PlayState_Business : State<L_System_Play> {
		public override void Execute (L_System_Play root)
		{
			// 更新所有玩家和角色
			L_ActorManager.It.CustomUpdate();
			L_PlayerManager.It.CustomUpdate();
		}
	}
}