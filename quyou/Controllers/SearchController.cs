using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace quyou.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/

        public ActionResult Index()
        {
            ViewBag.createby = BLL.UserHelper.Instance.GetUser().Id + "|" + BLL.UserHelper.Instance.GetUser().name;
            return View();
        }
        public ActionResult Infor(string pnbr)
        {
            var mod = BLL.Purchse.Instance.Get_by_PNbr(pnbr, BLL.UserHelper.Instance.GetUser().Id);
            return View(mod);
        }

        public JsonResult ListSearch(string pnbr, string lnbr, string state, string userId, int pageIndex, int pageSize)
        {
            //int userId = BLL.UserHelper.Instance.GetUser().Id;
            //int uId = !string.IsNullOrEmpty(userId) ? Convert.ToInt32(userId) : BLL.UserHelper.Instance.GetUser().Id;
            List<Model.TskForDisplay> lst = BLL.Purchse.Instance.SearchList(pnbr, lnbr, state, userId, pageIndex, pageSize).ToList();

            var total = lst.Count > 0 ? lst[0].rowCnt : 0;
            return Json(new { total = total, rows = lst, state = true, msg = "" }, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult listDataForDisplay(string roleName,string pnbr)
        //{

        //}




    }
}
