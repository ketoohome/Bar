/*******************************************************************************************************************
 * 作者：杜凯
 * 时间：2016.10.16
 * *****************************************************************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TOOL;

namespace GameLogic{

    /// <summary>
    /// 系统类型
    /// </summary>
    public enum SystemType{
		ST_Menu = 1,			// 主界面系统
		ST_Play = 2,			// 游戏系统
	}

	/// <summary>
	/// 系统管理器，系统工厂
	/// </summary>
	public class L_SystemManager : U3DSingleton<L_SystemManager> {

		Dictionary<SystemType, L_System> m_Systems = new Dictionary<SystemType, L_System>(); // 系统列表

		//
		void Awake(){
			// 如果已经初始化了，则不执行一下内容
			if(!IsSingle) return;
            // 系统注册
		    //----------------------------------------------------------------------------
			Factory<L_System>.Register<L_System_Menu>((int)SystemType.ST_Menu);
			Factory<L_System>.Register<L_System_Play>((int)SystemType.ST_Play);
			//----------------------------------------------------------------------------
        }

		//
		void OnDestroy(){
			// 如果已经初始化了，则不执行一下内容
			if(!IsSingle) return;
		}
		
		/// <summary>
		/// 更新系统
		/// </summary>
		public void CustomUpdate () {
			foreach(L_System sys in m_Systems.Values){
				sys.CustomUpdate();
			}
		}

        /// <summary>
        /// 创建系统
        /// </summary>
        /// <param name="type">系统类型</param>
        /// <returns>系统</returns>
		public L_System CreateSystem(SystemType type){

            if(m_Systems.ContainsKey(type))
				return m_Systems[type];

			L_System sys = Factory<L_System>.Create((int)type);
			m_Systems.Add(type, sys);
			sys.Start();
			return sys;
		}

        /// <summary>
        /// 通过系统的种类移除系统
        /// </summary>
        /// <param name="type"></param>
		public void RemoveSystem(SystemType type) {
			if(m_Systems.ContainsKey(type)) {
				m_Systems[type].End();
				m_Systems.Remove(type);
			};
		}

        /// <summary>
        /// 系统是否存在
        /// </summary>
        public bool ExistSystem(SystemType type){
            foreach (KeyValuePair<SystemType, L_System> pair in m_Systems)
            {
                if (pair.Key == type) return true;
            }
            return false;
        }
	}
}