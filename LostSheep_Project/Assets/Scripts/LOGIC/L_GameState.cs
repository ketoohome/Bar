using UnityEngine;
using System.Collections;
using GameCommon;
using TOOL;

namespace GameLogic
{
    /// <summary>
    /// 游戏逻辑状态枚举
    /// </summary>
    public enum GameState
    {
        GS_Initialize,	// 游戏初始状态
        GS_Menu,        // 游戏主界面状态
        GS_Play,        // 游戏状态
    }

    /// <summary>
    /// 游戏初始化状态
    /// </summary>
    public class L_GameStateInitialize : State<L_GameRoot> {
        /// <summary>
        /// 进入状态时调用
        /// </summary>
        public override void Enter(L_GameRoot root){
			Application.LoadLevel("Initilize");
		}

        /// <summary>
        /// 状态更新时每帧调用
        /// </summary>
        public override void Execute(L_GameRoot root) { 
			L_GameRoot.ChangeState(GameState.GS_Menu);
		}


        /// <summary>
        /// 退出状态时调用
        /// </summary>
        public override void Exit(L_GameRoot root){ }
    }

    /// <summary>
    /// 游戏界面状态
    /// </summary>
    public class L_GameStateMenu : State<L_GameRoot>
    {
        /// <summary>
        /// 进入状态时调用
        /// </summary>
        public override void Enter(L_GameRoot root) {
			Application.LoadLevel("Menu");

			// 创建系统
			L_SystemManager.Instance.CreateSystem(SystemType.ST_Menu);
		}

        /// <summary>
        /// 状态更新时每帧调用
        /// </summary>
        public override void Execute(L_GameRoot root) { }


        /// <summary>
        /// 退出状态时调用
        /// </summary>
        public override void Exit(L_GameRoot root) { 
			// 移除系统
			L_SystemManager.Instance.RemoveSystem(SystemType.ST_Menu);
		}
    }

    /// <summary>
    /// 游戏状态
    /// </summary>
    public class L_GameStatePlay : State<L_GameRoot>
    {
        /// <summary>
        /// 进入状态时调用
        /// </summary>
        public override void Enter(L_GameRoot root) { 
			Application.LoadLevel("Play");
			// 创建系统
			L_SystemManager.Instance.CreateSystem(SystemType.ST_Play);
		}

        /// <summary>
        /// 状态更新时每帧调用
        /// </summary>
        public override void Execute(L_GameRoot root) { }


        /// <summary>
        /// 退出状态时调用
        /// </summary>
        public override void Exit(L_GameRoot root) { 
			L_SystemManager.Instance.RemoveSystem(SystemType.ST_Play);
		}
    }
}
