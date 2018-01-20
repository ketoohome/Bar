using UnityEngine;
using System.Collections;

namespace GameCommon {

	/// <summary>
	/// User interface type.
	/// </summary>
	public enum UIType{
		UIMenu = 0,
		UIPlay = 1,
	} 

	/// <summary>
	/// 公共调用类型
	/// </summary>
	public class GameCommon 
    {
        public static void LogWarning(string s)
        {
            Debug.LogWarning("------GAME(Time:" + Time.time + ")------" + s);
        }

        //
        public static void Log(string s)
        {
            Debug.Log("------GAME(Time:" + Time.time + ")------" + s);
        }

        //
        public static void LogError(string s)
        {
            Debug.LogError("------GAME(Time:" + Time.time + ")------" + s);
        }

		// 不同平台的streaming资源路径
		public static readonly string PathURL =
			#if UNITY_STANDALONE_WIN || UNITY_EDITOR
			"file://" + Application.dataPath + "/StreamingAssets/";
			#elif UNITY_ANDROID
			Application.streamingAssetsPath;
			//"jar:file://" + Application.dataPath + "!/assets/";
			#elif UNITY_IPHONE
			Application.dataPath + "/Raw/";
			#else
			string.Empty;
			#endif
    }
}
