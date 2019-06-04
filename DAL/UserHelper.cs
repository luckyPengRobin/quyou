using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Data;
using ToolKits;
namespace DAL
{
    public class UserHelper
    {
        /// <summary>
        /// 根据用户名和密码获取用户登录信息
        /// </summary>
        /// <param name="LoginId"></param>
        /// <param name="psw"></param>
        /// <returns></returns>
        public Model.UserInfo Get_LoginUser(string LoginId, string psw)
        {
            string sqlStr = "select Id,name,email,roles,status from quyou.[dbo].[tbUser] where email = '" + LoginId + "' and [password] = '" + psw + "'";

            Model.UserInfo userInfo = new Model.UserInfo();
            DataSet ds =ToolKits.DbHelperSQL.Query(sqlStr);
            if (ds.Tables[0].Rows.Count > 0)
            {
                userInfo = ToolKits.ModelConvertHelper<Model.UserInfo>.ConvertToEntity(ds.Tables[0]);
                //ToolKits.CookieHelper.Save2Cookie(clsDefine.cookie.cookieName, userInfo); //save user information to cookie

                CreateTiket(userInfo);   //
                return userInfo;
            }
            return null;

        }        
        /// <summary>
        /// 根据用户名和密码判断登陆是否成功
        /// </summary>
        /// <param name="LoginId"></param>
        /// <param name="psw"></param>
        /// <returns></returns>
        public bool Check_Login(string LoginId, string psw)
        {
            string sqlStr = "select Id,name,email,roles,status from quyou.[dbo].[tbUser] where email = '" + LoginId + "' and [password] = '" + psw + "'";
           
            DataSet ds = ToolKits.DbHelperSQL.Query(sqlStr);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //Model.UserInfo userInfo = new Model.UserInfo();
                var userInfo = ToolKits.ModelConvertHelper<Model.UserInfo>.ConvertToEntity(ds.Tables[0]);
                CreateTiket(userInfo);   
                return true;
            }
            return false;

        }

        /// <summary>
        /// 获取用户登录信息
        /// </summary>
        /// <returns></returns>
        public Model.UserInfo GetUser()
        {
            if (HttpContext.Current.Request.IsAuthenticated)//是否通过身份验证
            {
                HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];//获取cookie
                FormsAuthenticationTicket Ticket = FormsAuthentication.Decrypt(authCookie.Value);//解密
                return Newtonsoft.Json.JsonConvert.DeserializeObject<Model.UserInfo>(Ticket.UserData);
                //return SerializeHelper.Instance.JsonDeserialize<Users>(Ticket.UserData);//反序列化
            }
            return null;
        }




        //创建票据
        public void CreateTiket(Model.UserInfo u)
        {
           
            //序列化用户对象
            var userData = Newtonsoft.Json.JsonConvert.SerializeObject(u);
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(0, FormsAuthentication.FormsCookieName, DateTime.Now, DateTime.Now.AddDays(1), false, userData, "/");
            var encryTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryTicket);
            cookie.Expires = ticket.Expiration;
            cookie.HttpOnly = true;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// Get User Information by loginName
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        //public Model.UserInfo get_user_Info(string loginName)
        //{
        //    if (string.IsNullOrEmpty(loginName))
        //        return null;
        //    loginName = StringUtil.RightSplit(loginName, '\\');
        //    string strSql = "select*from APDB.dbo._employee_info where LoginID = '" + loginName + "'";

        //    Model.clsUser userInfo = new Model.clsUser();
        //    DataSet ds = DbHelperSQL.Query("select LoginId,DisplayName,Email from APDB.dbo._employee_info where LoginID = '" + loginName + "'");
        //    //DataSet ds = DbHelperSQL.Query("select LoginId,DisplayName,Email from [ECM].[dbo].[tbUser] where LoginID = '" + loginName + "'");    //local
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        userInfo.NetId = ds.Tables[0].Rows[0]["LoginId"].ToString();
        //        userInfo.Name = ds.Tables[0].Rows[0]["DisplayName"].ToString();
        //        userInfo.Email = ds.Tables[0].Rows[0]["Email"].ToString();

        //        ToolKits.CookieHelper.Save2Cookie(clsDefine.cookie.cookieName, userInfo); //save user information to cookie
        //    }
        //    else
        //    { return null; }
        //    return userInfo;

        //}
        /// <summary>
        /// get user information by where clause
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        //public Model.clsUser get_user_Information(string whereClause)
        //{
        //    DataSet ds = DbHelperSQL.Query("select LoginID as NetId,DisplayName as Name,Email  from ecnsys_online.[dbo].[employee_info]  where " + whereClause + " ");
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        Model.clsUser userInfo = new Model.clsUser();
        //        userInfo = ToolKits.ModelConvertHelper<Model.clsUser>.ConvertToEntity(ds.Tables[0]);
        //        return userInfo;
        //    }
        //    return null;

        //}



        //public IList<Model.clsUser> Get_list_Users(string name)
        //{
        //    string strSql = "select top 10 LoginID as NetId,DisplayName as Name, Email from ecnsys_online.dbo.employee_info where DisplayName like '%" + name + "%'";
        //    return ToolKits.ModelConvertHelper<Model.clsUser>.ConvertToModel(DbHelperSQL.Query(strSql).Tables[0]);
        //}
        //public IList<Model.clsUser> Get_list_PE_Users(string name)
        //{
        //    string strSql = "select top 10 a.UserId as NetId,a.UserName as Name,a.UserMail as Email from [dbo].[tbUser] a,dbo.UserRole b where a.RoleId = b.Id and [Status] =1 and b.RoleName = 'PE' and a.UserName like '%" + name + "%'";
        //    return ToolKits.ModelConvertHelper<Model.clsUser>.ConvertToModel(DbHelperSQL.Query(strSql).Tables[0]);
        //}


        /// <summary>
        /// get user information by where clause
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        //public Model.clsUser get_PE(string whereClause)
        //{
        //    DataSet ds = DbHelperSQL.Query("select [UserId] as NetId,[UserName] as Name,[UserMail] as Email  from [FECM].[dbo].[tbUser]  where " + whereClause + " ");
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        Model.clsUser userInfo = new Model.clsUser();
        //        userInfo = ToolKits.ModelConvertHelper<Model.clsUser>.ConvertToEntity(ds.Tables[0]);
        //        return userInfo;
        //    }
        //    return null;

        //} 




        public bool UpdatePassword(string pwd, string newPwd)
        {
            Model.UserInfo user = GetUser();
            int id = user.Id;
            string sqlStr = "select password from quyou.[dbo].[tbUser] where id= " + id;
            DataSet ds = ToolKits.DbHelperSQL.Query(sqlStr);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //Model.UserInfo userInfo = new Model.UserInfo();
                string currentPwd = ds.Tables[0].Rows[0].ItemArray[0].ToString();




                if (currentPwd.Equals(pwd))
                {

                    sqlStr = "update quyou.[dbo].[tbUser] set password='" + newPwd + "' where email='" + user.email + "'";


                    int count = ToolKits.DbHelperSQL.ExecuteSql(sqlStr);

                    if (count == 1)
                    {

                        return true;
                    }

                }
                else
                {
                    return false;
                }

            }





            return false;

        }
    }
}
