using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.CMS.Framework
{
  public  class DesOrRsaEncrypt
    {
        /// <summary>
        /// RSA和DES混合加密
        /// </summary>
        /// <param name="data">待加密数据</param>
        /// <param name="publicKey">RSA公钥</param>
        /// <returns></returns>
      public Param Encrypt(string data, string publicKey, string DESKey = "seiTnAvM")
        {
            //加密数据
            DESSecurity DES = new DESSecurity();
           // string DESKey = DES.GenerateKey();//产生deskey
            string encryptData = DES.Encrypt(data, DESKey);

            //加密DESkey
            RSASecurity RSA = new RSASecurity();
            string encryptDESKey = RSA.Encrypt(DESKey, publicKey);

            Param mixParam = new Param();
            mixParam.DESKey = encryptDESKey;
            mixParam.Data = encryptData;
            return mixParam;
        }

        /// <summary>
        /// RSA和DES混合解密
        /// </summary>
        /// <param name="data">待解密数据</param>
        /// <param name="key">带解密的DESKey</param>
        /// <param name="privateKey">RSA私钥</param>
        /// <returns></returns>
        public string Decrypt(string data, string key, string privateKey)
        {
            //解密DESKey
            RSASecurity RSA = new RSASecurity();
            string DESKey = RSA.Decrypt(key, privateKey);

            //解密数据
            DESSecurity DES = new DESSecurity();
            return DES.Decrypt(data, DESKey);
        }
    }
}
