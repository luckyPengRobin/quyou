using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace quyou.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<Model.TskForDisplay> lst = BLL.Purchse.Instance.list_tasks(BLL.UserHelper.Instance.GetUser().Id, "InProcess", 1, 10).ToList();
            ViewBag.count = lst.Count > 0 ? lst[0].rowCnt : 0;
            lst = null;
            return View();
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
