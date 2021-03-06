using TOOL;

namespace GameCommon{

	/// <summary>
	/// 游戏事件ID
	/// </summary>
	public enum GameEventID{

		// 自定义事件
		Event_Custom = 0,	// 自定义事件，第一个参数可做为标识

        // Game事件
        Event_GameLogicStart = 1,       //游戏逻辑初始化完成，游戏启动
        Event_GameLogicStop,	        // 游戏逻辑停止，游戏结束
		
		// 游戏事件
		Event_ChangeMoney = 1001,			// 改变钱的数量

		Event_Drinker_PointMenu = 1201,		// 点单
		Event_Drinker_CallBarman = 1202,	// 呼叫调酒师
		Event_Drinker_Pay = 1204,			// 买单
		Event_Drinker_FindSeat = 1205,		// 找到位置
		Event_Drinker_LeaveSeat = 1206,		// 离开位置

		Event_Barman_Tend = 1301,			// 调酒师招待酒客
		Event_Barman_CreateCocktail = 1302, // 制作鸡尾酒



		// UI事件
		Event_UI_Create = 2001,			// 创建UI
		Event_UI_Delete = 2002,			// 删除UI
		Event_UI_DeleteAll = 2003,		// 删除所有UI
		Event_UI_Menu_Play = 2101,		// 游戏开始

		// 通知UI的事件
		Event_2UI_ChangeMoney = 3001,	// 金钱改变
	}

	/// <summary>
	/// 事件管理器
	/// </summary>
	public class GameEventMachine : EventMachine<GameEventID>
	{
		private static GameEventMachine instance = null;
		public static GameEventMachine gInstance
		{
			get
			{
				if (instance == null)
				{
					instance = new GameEventMachine();
				}
				
				return instance;
			}
		}

        public static void SetNull(){
            instance = null;
        }

		/// <summary>
		/// 注册事件
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="function">Function.</param>
		public static void Register(GameEventID key,OnEvent function){
			gInstance.Add(key,function);
		}

		/// <summary>
		/// 注销事件
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="function">Function.</param>
		public static void Unregister(GameEventID key,OnEvent function){
			gInstance.Remove(key,function);
		}

		/// <summary>
		/// 发送事件
		/// </summary>
		/// <param name="key">Key.</param>
		public static void SendEvent(GameEventID key,params object[] args){
			gInstance.Send(key,args);
		}

	}

	//GameEventMachine.gInstance.Send(GameEventID.Title);
	//GameEventMachine.gInstance.Add(GameEventID.Title, OnGamePrepare);
	//GameEventMachine.gInstance.Remove(GameEventID.Title, OnGamePrepare);
	//void OnChangeUI(params object[] args)
}