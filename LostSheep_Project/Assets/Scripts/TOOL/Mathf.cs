using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace TOOL
{
	// 自定义数学方法
	public class CustomMathf{

        /// <summary>
        /// 2D 的 LookAt 函数
        /// </summary>
		public static void LookAt2D(Transform tran,Vector3 target){
			target = target - tran.position;
			float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
			tran.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
		
        /// <summary>
        /// 数学表达式转换
        /// </summary>
		public static double Evaluate(string expression)
		{
			return(double)new System.Xml.XPath.XPathDocument(
				new StringReader("<r/>")).CreateNavigator().Evaluate(
				string.Format("number({0})",
			              new System.Text.RegularExpressions.Regex(@"([\+\-\*])")
			              .Replace(expression," ${1} ")
			              .Replace("/"," div ")
			              .Replace("%"," mod ")));
		}

        /// <summary>
        /// 弹性差值
        /// </summary>
        /// <param name="time">执行时间</param>
        /// <param name="damp">阻尼</param>
        /// <param name="delta">差值速度</param>
        /// <param name="count">回弹数量</param>
        /// <returns>差值结果（0~1）</returns>
        public static float ElasticLerp(ref Vector2 temp, float delta, int count)
        {
            temp.x = Mathf.Lerp(temp.x, 1, delta / count);
            temp.y += delta;
            return 1.0f - Mathf.Cos(temp.y) * (1.0f - temp.x);
        }

        /// <summary>
        /// 自定义差值（允许越界）
        /// </summary>
        /// <param name="form"></param>
        /// <param name="to"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static float Lerp(float form, float to, float t) {
            return form * (1.0f - t) + to * t;
        }

        /// <summary>
        /// 自定义差值（允许越界）
        /// </summary>
        /// <param name="form"></param>
        /// <param name="to"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vector3 Lerp(Vector3 form, Vector3 to, float t)
        {
            return form * (1.0f - t) + to * t;
        }

        /// <summary>
        /// 取最大值(只限于int，float)
        /// </summary>
		public static int Max(params int[] args){ return Mathf.Max(args);}
		public static int Min(params int[] args){ return Mathf.Min(args);}
		public static float Max(params float[] args){ return Mathf.Max(args);}
		public static float Min(params float[] args){ return Mathf.Min(args);}

        /// <summary>
        /// 点到直线距离
        /// </summary>
        /// <param name="point">点坐标</param>
        /// <param name="linePoint1">直线上一个点的坐标</param>
        /// <param name="linePoint2">直线上另一个点的坐标</param>
        /// <returns></returns>
        public static float DisPoint2Line(Vector3 point, Vector3 linePoint1, Vector3 linePoint2)
        {
            Vector3 vec1 = point - linePoint1;
            Vector3 vec2 = linePoint2 - linePoint1;
            Vector3 vecProj = Vector3.Project(vec1, vec2);
            float dis = Mathf.Sqrt(Mathf.Pow(Vector3.Magnitude(vec1), 2) - Mathf.Pow(Vector3.Magnitude(vecProj), 2));
            return dis;
        }

        public static float DisPoint2Line(Vector3 point, Ray ray)
        {
            Vector3 vec1 = point - ray.origin;
            Vector3 vec2 = ray.direction;
            Vector3 vecProj = Vector3.Project(vec1, vec2);
            float dis = Mathf.Sqrt(Mathf.Pow(Vector3.Magnitude(vec1), 2) - Mathf.Pow(Vector3.Magnitude(vecProj), 2));
            return dis;
        }



        /// <summary>
        /// 点到平面的距离 自行推演函数
        /// </summary>
        /// <param name="point"></param>
        /// <param name="surfacePoint1"></param>
        /// <param name="surfacePoint2"></param>
        /// <param name="surfacePoint3"></param>
        /// <returns></returns>
        public static float DisPoint2Surface(Vector3 point, Vector3 surfacePoint1, Vector3 surfacePoint2, Vector3 surfacePoint3)
        {
            //空间直线一般式方程 Ax + By + Cz + D = 0;
            //假定 A = 1 ，推演B C D用A来表示，约去A，可得方程
            float BNumerator = (surfacePoint1.x - surfacePoint2.x) * (surfacePoint2.z - surfacePoint3.z) - (surfacePoint2.x - surfacePoint3.x) * (surfacePoint1.z - surfacePoint2.z);
            float BDenominator = (surfacePoint2.y - surfacePoint3.y) * (surfacePoint1.z - surfacePoint2.z) - (surfacePoint1.y - surfacePoint2.y) * (surfacePoint2.z - surfacePoint3.z);
            float B = BNumerator / BDenominator;
            float C = (B * (surfacePoint1.y - surfacePoint2.y) + (surfacePoint1.x - surfacePoint2.x)) / (surfacePoint2.z - surfacePoint1.z);
            float D = -surfacePoint1.x - B * surfacePoint1.y - C * surfacePoint1.z;

            return DisPoint2Surface(point, 1f, B, C, D);
        }

        public static float DisPoint2Surface(Vector3 point, float FactorA, float FactorB, float FactorC, float FactorD)
        {
            //点到平面的距离公式 d = |Ax + By + Cz + D|/sqrt(A2 + B2 + C2);
            float numerator = Mathf.Abs(FactorA * point.x + FactorB * point.y + FactorC * point.z + FactorD);
            float denominator = Mathf.Sqrt(Mathf.Pow(FactorA, 2) + Mathf.Pow(FactorB, 2) + Mathf.Pow(FactorC, 2));
            float dis = numerator / denominator;
            return dis;
        }

        /// <summary>
        /// 点到平面距离 调用U3D Plane类处理
        /// </summary>
        /// <param name="point"></param>
        /// <param name="surfacePoint1"></param>
        /// <param name="surfacePoint2"></param>
        /// <param name="surfacePoint3"></param>
        /// <returns></returns>
        public static float DisPoint2Surface2(Vector3 point, Vector3 surfacePoint1, Vector3 surfacePoint2, Vector3 surfacePoint3)
        {
            Plane plane = new Plane(surfacePoint1, surfacePoint2, surfacePoint3);

            return DisPoint2Surface2(point, plane);
        }

        public static float DisPoint2Surface2(Vector3 point, Plane plane)
        {
            return plane.GetDistanceToPoint(point);
        }

        /// <summary>
        /// 平面夹角
        /// </summary>
        /// <param name="surface1Point1"></param>
        /// <param name="surface1Point2"></param>
        /// <param name="surface1Point3"></param>
        /// <param name="surface2Point1"></param>
        /// <param name="surface2Point2"></param>
        /// <param name="surface2Point3"></param>
        /// <returns></returns>
        public static float SurfaceAngle(Vector3 surface1Point1, Vector3 surface1Point2, Vector3 surface1Point3, Vector3 surface2Point1, Vector3 surface2Point2, Vector3 surface2Point3)
        {
            Plane plane1 = new Plane(surface1Point1, surface1Point1, surface1Point1);
            Plane plane2 = new Plane(surface2Point1, surface2Point1, surface2Point1);
            return SurfaceAngle(plane1, plane2);
        }

        public static float SurfaceAngle(Plane plane1, Plane plane2)
        {
            return Vector3.Angle(plane1.normal, plane2.normal);
        }

        /// <summary>
        /// 两个端点的贝塞尔曲线
        /// </summary>
        /// <param name="start">起始节点</param>
        /// <param name="startCorve">起始节点路径偏移</param>
        /// <param name="end">结束节点</param>
        /// <param name="endCorve">结束节点路径偏移</param>
        /// <param name="offset">当前位置（0~1）</param>
        public static Vector3 Bezizer(Vector3 start, Vector3 startCorve, Vector3 end, Vector3 endCorve, float offset)
        {
            Vector3 result = Vector3.zero;
			//--------------------------------------------------------------------
			//--------------------------------------------------------------------
            return result;
        }

		/// <summary>
		/// Round the specified value and digids.
		/// </summary>
		/// <param name="value">Value.</param>
		/// <param name="digids">Digids.</param>
		public static double Round(double value,int digits){
			return Math.Round (value,digits);
		}
		public static float Round(float value, int digits){
			return (float)Round ((double)value, digits);
		}
	}

    // 自定义随机
    public class CustomRandom { 
        // 0~99随机数列整数
        static int[] randomInts_0_99 = {  17,91,63,81,24,34,70,56,92,15,
                                        93,99,41,46,18,40,67,8,78,23,
                                        0,51,53,73,32,29,26,58,10,48,
                                        77,65,55,50,83,54,42,35,49,5,
                                        62,76,16,38,61,75,20,28,59,2,
                                        25,9,71,72,80,7,82,22,12,36,
                                        64,85,60,52,57,96,97,21,94,98,
                                        37,45,68,66,33,27,86,88,47,31,
                                        6,87,43,13,1,74,3,14,11,30,
                                        89,79,19,4,39,90,84,95,44,69 };
        // 随机取位
        static int digits = 0;

        // 从0~99随机数列中获取随机数
        static public int RandomInt_0_99 { 
            get {
                digits++;
                return randomInts_0_99[(digits - 1) % randomInts_0_99.Length];
            } 
        }

        // 创建随机数列
        static public int[] CreateRandomInts(int min, int max)
        {
            bool b = false;
            int i = 0;
            List<int> counts = new List<int>();
            while (counts.Count < max - min)
            {
                i = UnityEngine.Random.Range(min, max);
                b = true;
                foreach (int n in counts)
                {
                    if (n == i) b = false;
                }
                if (b) counts.Add(i);
            }
            return counts.ToArray();
        }

        // 更新0~99随机数列
        static public void UpdateRandomInts_0_99() {
            randomInts_0_99 = CreateRandomInts(0,100);
        }

        /// <summary>
        /// 自定义假随机函数
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        static public float CustomRandomFloat(float min, float max) {
            float a = (float)CustomRandom.RandomInt_0_99 / 99f;
            return (max - min) * a + min; 
        }

        /// <summary>
        /// 自定义随机函数
        /// </summary>
        /// <param name="fate"></param>
        /// <returns></returns>
        static public bool CustomRandomBool(float probability)
        {
            return (float)RandomInt_0_99 * 0.01 <= probability;
        }

        /// <summary>
        /// 自定义随机函数
        /// </summary>
        /// <returns></returns>
        static public int CustomRandomInt(int min, int max) {
            return (int)CustomRandomFloat(min,max);
        }
    }

	// 数组添加元素的方法集合
	public class ArrayFun<T>{
		public static T[] AddElement(T[] array,T element){
			T[] newarray = new T[array.Length+1];
			newarray[0] = element;
			array.CopyTo(newarray,1);
			return newarray;
		}
	}
}