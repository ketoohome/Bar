/*******************************************************************************************************************
 * 作者：杜凯
 * 时间：2016.10.17
 * *****************************************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;

namespace TOOL
{
    /// <summary>
	/// 基础数据节点
	/// </summary>
	public class BaseData<T> : IDataNode<T>, IData<T> where T : class, IDataNode<T>, IData<T>,  new()
    {
        protected string m_Key = "";
        protected object m_Value = null;
        protected Dictionary<string, T> m_Children = new Dictionary<string, T>();
        
        //
        public List<T> Children{
            get {
                List<T> list = new List<T>();
                foreach (T node in m_Children.Values)
                {
                    list.Add(node);
                }
                return list;
            }
        }

        /// <summary>
        /// 获取指定类型的数据
        /// </summary>
        public TYPE GetValue<TYPE>() { return (TYPE)m_Value; }

        /// <summary>
        /// 获得key值
        /// </summary>
        public string GetKey() { return m_Key; }
        /// <summary>
        /// 修改存入的值（必须保证为同种类型）
        /// </summary>
        /// <param name="value"></param>
        public object Value {
            get{ return m_Value; }
            set{
                if (m_Value == null){
                    m_Value = value;
                    return;
                }
                // 如果已经赋值，判断是否与上一种类型一致
                if (value.GetType() == m_Value.GetType())
                    m_Value = value;
                else throw new ArgumentException("赋值类型与原数据类型不符！");
            }
        }

        /// <summary>
		/// 获得节点的Key（Key值不能包含符号'_',Key尽量避免在外部修改）
		/// </summary>
        public string Key { 
			get { return m_Key.Substring(m_Key.LastIndexOf('_')+1); } 
			set {
				int count = m_Key.LastIndexOf('_') + 1;
				if(count == 0) m_Key = value;
				else m_Key = m_Key.Substring(0,m_Key.Length - count) + "_" + value;
			}
		}

		/// <summary>
		/// 数据所在路径
		/// </summary>
		/// <value>The path.</value>
		public string Path{ get{ return m_Key;} }

        /// <summary>
        /// 获得子节点
        /// </summary>
        public T FindChild(params string[] keys)
        {
			return FindChild(0,keys);
		}

		/// <summary>
		/// 递归获得子节点(该方法禁止外部使用)
		/// </summary>
		/// <returns>The child data.</returns>
		/// <param name="index">Index.</param>
		/// <param name="keys">Keys.</param>
		public T FindChild(int index,params string[] keys){
			string key = m_Key + "_" + keys[index];
			if(m_Children.ContainsKey(key)){
				if(index == keys.Length - 1) return m_Children[key];
				else return m_Children[key].FindChild(index + 1,keys);
			}
			else return null;
		}

        /// <summary>
        /// 添加(创建)子节点到子节点中
        /// </summary>
        /// <param name="key">索引值</param>
        /// <param name="value">存储数据</param>
        /// <returns></returns>
        //public abstract T CreatChildData(string key, object value);
        public T CreatChildData(string key, object value) {

			if(key.IndexOf('_') != -1) 
				throw new ArgumentException("Key值不能包含符号'_'");

            key = m_Key + "_" + key;
            T node = new T();
            node.Value = value;
            node.Key = key;
            Add(key, node);
            return node;
        }

        /// <summary>
        /// 增加子节点
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        void Add(string key,T data) {
            if (m_Children.ContainsKey(key)){
                throw new ArgumentException("该key值已经存在:" + key);
            }
            m_Children[key] = data; 
        }

        /// <summary>
        /// 移除子节点
        /// </summary>
        void Remove(string key) {
            if (m_Children.ContainsKey(key)) {
                m_Children.Remove(key);
            }
        }

        /// <summary>
        /// 是否存在数据
        /// </summary>
        /// <param name="key">子节点</param>
        public bool Exist(string key){
            return m_Children.ContainsKey(key);
        }

        /// <summary>
        /// 清空子节点
        /// </summary>
		public void ClearChildren()
        {
            m_Children.Clear();
        }
    }

    /// <summary>
    /// 数据接口
    /// </summary>
    public interface IData<T>
    {
		string Key { set; get;}
		string Path{ get;}
		object Value { set; get;}
		List<T> Children{get;}
		/// <summary>
		/// 获得子节点(该方法禁止外部使用)
		/// </summary>
		/// <returns>The child data.</returns>
		/// <param name="index">Index.</param>
		/// <param name="keys">Keys.</param>
		T FindChild(int index,params string[] keys);
		/// <summary>
		/// 创建子节点
		/// </summary>
		/// <returns>The child data.</returns>
		/// <param name="key">Key.</param>
		/// <param name="value">Value.</param>
		T CreatChildData(string key, object value);
		/// <summary>
		/// 清空子节点
		/// </summary>
		void ClearChildren();
    }

    /// <summary>
    /// 只读数据类型接口，可用于中间层的信息获取
    /// </summary>
    public interface IDataNode<T>
    {
        /// <summary>
        /// 获取指定类型的数据
        /// </summary>
        V GetValue<V>();
		/// <summary>
		/// 获得Key值
		/// </summary>
		string GetKey();
        /// <summary>
        /// 获得子节点链表(该方法效率低，使用时应当实例赋值)
        /// </summary>
        List<T> Children { get; }
        /// <summary>
        /// 获得子节点
        /// </summary>
		T FindChild(params string[] keys);
    }

	/// <summary>
	/// 数据工具
	/// </summary>
	public class DataTool
	{
		/// <summary>
		/// 解析值的格式并赋值给数据
		/// </summary>
		/// <returns>The format.</returns>
		/// <param name="format">Format.</param>
		/// <param name="value">Value.</param>
		/// <param name="data">Data.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T ParsingFormat<T>(string format,string value,T data) where T : class, IData<T>{
			
			if(format == typeof(sbyte).ToString() || format == "sbyte") data.Value = sbyte.Parse(value);
			else if(format == typeof(byte).ToString() || format == "byte") data.Value = byte.Parse(value);
			else if(format == typeof(char).ToString() || format == "char") data.Value = char.Parse(value);
			else if(format == typeof(bool).ToString() || format == "bool") data.Value = bool.Parse(value);
			else if(format == typeof(float).ToString() || format == "float") data.Value = float.Parse(value);
			else if(format == typeof(double).ToString() || format == "double") data.Value = double.Parse(value);
			else if(format == typeof(decimal).ToString() || format == "decimal") data.Value = decimal.Parse(value);
			else if(format == typeof(int).ToString() || format == "int") data.Value = int.Parse(value);
			else if(format == typeof(short).ToString() || format == "short") data.Value = short.Parse(value);
			else if(format == typeof(long).ToString() || format == "long") data.Value = long.Parse(value);
			else if(format == typeof(uint).ToString() || format == "uint") data.Value = uint.Parse(value);
			else if(format == typeof(ushort).ToString() || format == "ushort") data.Value = ushort.Parse(value);
			else if(format == typeof(ulong).ToString() || format == "ulong") data.Value = ulong.Parse(value);
			else if(format == typeof(string).ToString() || format == "string") data.Value = value;
			else throw new ArgumentException("无法解析当前的数据格式：" + format);
			
			return data;
		}

		/// <summary>
		/// 根绝数据格式创建ID
		/// </summary>
		/// <returns>The data.</returns>
		/// <param name="name">Name.</param>
		/// <param name="format">Format.</param>
		/// <param name="value">Value.</param>
		/// <param name="data">Data.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T ParsingFormat<T>(string name,string format,string value,T data) where T :class, IData<T>{
			T childData = null;
			if(format == typeof(sbyte).ToString() || format == "sbyte")
				childData = data.CreatChildData(name,sbyte.Parse(value));
			if(format == typeof(byte).ToString() || format == "byte")
				childData = data.CreatChildData(name,byte.Parse(value));
			else if(format == typeof(char).ToString() || format == "char")
				childData = data.CreatChildData(name,char.Parse(value));
			else if(format == typeof(bool).ToString() || format == "bool")
				childData = data.CreatChildData(name,bool.Parse(value));
			else if(format == typeof(float).ToString() || format == "float")
				childData = data.CreatChildData(name,float.Parse(value));
			else if(format == typeof(double).ToString() || format == "double")
				childData = data.CreatChildData(name,double.Parse(value));
			else if(format == typeof(decimal).ToString() || format == "decimal")
				childData = data.CreatChildData(name,decimal.Parse(value));
			else if(format == typeof(int).ToString() || format == "int")
				childData = data.CreatChildData(name,int.Parse(value));
			else if(format == typeof(short).ToString() || format == "short")
				childData = data.CreatChildData(name,short.Parse(value));
			else if(format == typeof(long).ToString() || format == "long")
				childData = data.CreatChildData(name,long.Parse(value));
			else if(format == typeof(uint).ToString() || format == "uint")
				childData = data.CreatChildData(name,uint.Parse(value));
			else if(format == typeof(ushort).ToString() || format == "ushort")
				childData = data.CreatChildData(name,ushort.Parse(value));
			else if(format == typeof(ulong).ToString() || format == "ulong")
				childData = data.CreatChildData(name,ulong.Parse(value));
			else if(format == typeof(string).ToString() || format == "string")
				childData = data.CreatChildData(name,value);
			else throw new ArgumentException("无法解析当前的数据格式：" + format);
			
			return childData;
		}
	}
}