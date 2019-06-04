using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using quyou.Models;

namespace quyou.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class NetAuthorizeAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string userRole { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //HttpContext.Current.Request.IsAuthenticated
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                ContentResult Content = new ContentResult();
                Content.Content = string.Format("<script type='text/javascript'>alert('请先登录！');window.location.href='{0}';</script>", FormsAuthentication.LoginUrl);
                filterContext.Result = Content;
            }
            else
            {
                string[] Role = BLL.UserHelper.Instance.GetUser().roles.Split(',');//获取所有角色

                if (!string.IsNullOrEmpty(userRole))
                {
                    string[] userRoles = userRole.Split(',');
                    bool check = false;

                    foreach (string v in userRoles) {

                        if (Role.Contains(v)) {
                            check = true;
                            break;
                        }

                    }

                    if (!check){

                        //验证不通过
                        ContentResult Content = new ContentResult();
                        Content.Content = "<script type='text/javascript'>alert('权限验证不通过！');history.go(-1);</script>";
                        filterContext.Result = Content;
                    }

                    //if (!Role.Contains(userRole))//验证权限
                    //{
                    //    //验证不通过
                    //    ContentResult Content = new ContentResult();
                    //    Content.Content = "<script type='text/javascript'>alert('权限验证不通过！');history.go(-1);</script>";
                    //    filterContext.Result = Content;
                    //}
                }
            }
            base.OnActionExecuting(filterContext);
        }


    }
}