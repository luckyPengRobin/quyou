using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace NHibernate.CMS.Framework
{
    public class DESSecurity
    {
        /// <summary>
        /// 随机生成KEY
        /// </summary>
        /// <returns></returns>
        public string GenerateKey()
        {
            int _len = 8;
            Random random = new Random(DateTime.Now.Millisecond);
            byte[] keybyte = new byte[_len];
            for (int i = 0; i < _len; i++)
            {
                keybyte[i] = (byte)random.Next(65, 122);
            }
            return ASCIIEncoding.ASCII.GetString(keybyte);
        }

        /// <summary>
        /// DES 加密过程
        /// </summary>
        /// <param name="dataToEncrypt">待加密数据</param>
        /// <param name="DESKey"></param>
        /// <returns></returns>
        public string Encrypt(string dataToEncrypt, string DESKey)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] inputByteArray = Encoding.Default.GetBytes(dataToEncrypt);//把字符串放到byte数组中
                des.Key = ASCIIEncoding.ASCII.GetBytes(DESKey); //建立加密对象的密钥和偏移量
                des.IV = ASCIIEncoding.ASCII.GetBytes(DESKey);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        StringBuilder ret = new StringBuilder();
                        foreach (byte b in ms.ToArray())
                        {
                            ret.AppendFormat("{0:x2}", b);
                        }
                        return ret.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// DES 解密过程
        /// </summary>
        /// <param name="dataToDecrypt">待解密数据</param>
        /// <param name="DESKey"></param>
        /// <returns></returns>
        public string Decrypt(string dataToDecrypt, string DESKey)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] inputByteArray = new byte[dataToDecrypt.Length / 2];
                for (int x = 0; x < dataToDecrypt.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(dataToDecrypt.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }
                des.Key = ASCIIEncoding.ASCII.GetBytes(DESKey); //建立加密对象的密钥和偏移量，此值重要，不能修改
                des.IV = ASCIIEncoding.ASCII.GetBytes(DESKey);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        return System.Text.Encoding.Default.GetString(ms.ToArray());
                    }
                }
            }
        }
    }
}
