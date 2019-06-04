using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using quyou.Filters;
namespace quyou.Controllers
{
    [NetAuthorize(userRole = "6")]
    public class UserController : Controller
    {
        //
        // GET: /User/
       
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit(Nullable<int> id)
        {
            var mod = new Model.UserInfo();
            if (id != null && id != 0)
            {
                mod = BLL.tbUser.Instance.Get(Convert.ToInt32(id));

                return View(mod);
            }
            else
            {                
                return View(mod);
            }

        }
        
        [HttpPost]
        public JsonResult Edit(FormCollection collection)
        {
            Model.UserInfo obj = new Model.UserInfo();
            TryUpdateModel<Model.UserInfo>(obj, collection);
            var rst = BLL.tbUser.Instance.Save(obj, Model.clsDefine.TaskCategory.Save, BLL.UserHelper.Instance.GetUser().Id);

            return Json(rst, JsonRequestBehavior.AllowGet);


 

        }


        [HttpPost]
        public void remove(int id)
        {
            BLL.tbUser.Instance.Delte(id, BLL.UserHelper.Instance.GetUser().Id);
        }
        public JsonResult ListUsers(int pageIndex, int pageSize, string search, string sort, string sortOrder)
        {
            var kvlst = BLL.tbUser.Instance.List2(pageIndex, pageSize, sort, sortOrder, search,Model.clsDefine.StatusName.Active);

            //var rows = lst.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();

            return Json(new { total = kvlst.Key, rows = kvlst.Value, state = true, msg = "" }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Get list users by name <NetId,Name>
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public JsonResult ListUsersForSelect(string query)
        {
            var users = BLL.tbUser.Instance.list_Users(query);
        
            //2.2返回json  
            return Json(users, JsonRequestBehavior.AllowGet);
        }
   

    }
}
