using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.CMS.Framework
{
    public class LogtsticEncrypt
    {
        public LogtsticEncrypt() { }

        /// <summary>
        /// 混沌加解密 加密/解密 图文一起加密
        /// </summary>
        /// <param name="data">要处理的数据</param>
        /// <param name="u">应大于3.57</param>
        /// <param name="x0">应属于(0,1)</param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] data, double u, double x0)
        {
            byte[] res = new byte[data.Length];
            double x = logistic(u, x0, 2000);

            for (int i = 0; i < data.Length; i++)
            {
                x = logistic(u, x, 5);
                res[i] = Convert.ToByte(Convert.ToInt32(Math.Floor(x * 1000)) % 256 ^ data[i]);
            }

            return res;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strData"></param>
        /// <param name="u"></param>
        /// <param name="x0"></param>
        /// <returns></returns>
        public static string Encrypt(string strData, double u, double x0)
        {
            byte[] data = Encoding.Default.GetBytes(strData);
            byte[] res = new byte[data.Length];
            double x = logistic(u, x0, 2000);

            for (int i = 0; i < data.Length; i++)
            {
                x = logistic(u, x, 5);
                res[i] = Convert.ToByte(Convert.ToInt32(Math.Floor(x * 1000)) % 256 ^ data[i]);
            }
            string encrStr = Encoding.Default.GetString(res);
            return encrStr;
        }




        //Logistic模型：X_n+1=u*Xn(1-Xn)
        /// <summary>
        /// 计算Logtstic公式
        /// </summary>
        /// <param name="a">基数</param>
        /// <param name="x">参数</param>
        /// <param name="n">迭代次数</param>
        /// <returns>返回一个double的结果</returns>

        private static double logistic(double a, double x, int n)
        {
            for (int i = 0; i < n; i++)
            {
                x = a * x * (1 - x);
            }
            return x;
        }
    }
}
