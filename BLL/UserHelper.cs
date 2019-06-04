using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class UserHelper
    {
        public readonly static UserHelper Instance = new UserHelper();
        private readonly DAL.UserHelper dal = new DAL.UserHelper();  
        public Model.UserInfo Get_LoginUser(string LoginId, string psw)
        {
            return dal.Get_LoginUser(LoginId, psw);
        }
        /// <summary>
        ///  根据用户名和密码判断登陆是否成功
        /// </summary>
        /// <param name="LoginId"></param>
        /// <param name="psw"></param>
        /// <returns></returns>
         public bool Check_Login(string LoginId, string psw)
        {
            return dal.Check_Login(LoginId, psw);
        }
        /// <summary>
        /// 从cookie中获取用户信息
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public Model.UserInfo GetUser()
        {
            return dal.GetUser();
        }

        public bool UpdatePassword(string pwd, string newPwd)
        {
            return dal.UpdatePassword(pwd, newPwd);
        }


    }
}
