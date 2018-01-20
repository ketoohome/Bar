using UnityEngine;
using System.Collections;
using TOOL;
using GameCommon;

namespace GameLogic
{
    /// <summary>
    /// 游戏主逻辑
    /// </summary>
    public class L_GameRoot : U3DSingleton<L_GameRoot>
    {
        /// <summary>
        /// 游戏状态
        /// </summary>
		GameState m_GameState = GameState.GS_Initialize;
        /// <summary>
        /// 游戏当前状态
        /// </summary>
        public GameState State { get { return m_GameState; } }
        /// <summary>
        /// 游戏状态机实例
        /// </summary>
        StateMachine<L_GameRoot> m_stateMachine;

        /// <summary>
        /// 改变游戏当前状态
        /// </summary>
        /// <param name="state">改变的状态</param>
        static public void ChangeState(GameState state) {
            Instance.m_GameState = state;
            Instance.m_stateMachine.ChangeState(state);
        }

        /// <summary>
        /// 系统管理器
        /// </summary>
        L_SystemManager m_sysMgr;

        /// <summary>
        /// 缓存池
        /// </summary>
        L_DataPool m_DataPool;

        /// <summary>
        /// 游戏初始化
        /// </summary>
        void Awake() {
            // 防止重复创建
            if (!IsSingle) { Destroy(gameObject); return; }
            // 主逻辑所挂接的gameObject不能在切换场景的时候删除。
            DontDestroyOnLoad(gameObject);

            // 初始化系统管理器
            m_sysMgr = gameObject.AddComponent<L_SystemManager>();	
            // 初始化缓存池
            m_DataPool = gameObject.AddComponent<L_DataPool>();
            // 初始化状态机
            m_stateMachine = new StateMachine<L_GameRoot>(this);
            m_stateMachine.Add(GameState.GS_Initialize, new L_GameStateInitialize());	// 初始化状态
            m_stateMachine.Add(GameState.GS_Menu, new L_GameStateMenu());				// Logo状态
            m_stateMachine.Add(GameState.GS_Play, new L_GameStatePlay());			    // 版本更新状态

            GameEventMachine.SendEvent(GameEventID.Event_GameLogicStart);				// 游戏逻辑初始化完成，游戏启动

            m_stateMachine.SetCurrentState(GameState.GS_Initialize);
        }

        /// <summary>
        /// 游戏更新
        /// </summary>
        void Update() {
            if (!m_IsLogicPause) {
                // 系统更新
                m_sysMgr.CustomUpdate();
                // 游戏状态更新
                m_stateMachine.Update();
                // 计时器更新
                ClockMachine.It.CustomUpdate(Time.deltaTime);
                // 计数器更新
                CounterMachine.It.CustomUpdate();
            }
            // 实时计时器更新
            ClockMachine.It.RealTimeUpdate(Time.unscaledDeltaTime);
        }

        bool m_IsLogicPause = false;
        /// <summary>
        /// 游戏是否暂停
        /// </summary>
        public bool LogicPause { get { return m_IsLogicPause; } set { m_IsLogicPause = value; } }
    }
}