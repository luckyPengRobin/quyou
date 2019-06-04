using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using quyou.Filters;


namespace quyou.Controllers
{
    [Authorize]
    public class StockController : Controller
    {

        BLL.Stock _stock = new BLL.Stock();
       

       //[NetAuthorize(userRole = "1")] 
       // public ActionResult AllProfit()
       // {
       //     return View();

       // }

        [NetAuthorize(userRole = "9")] 
        public ActionResult Index()
        {
                     

            return View();
            

            //List<Model.TskForDisplay> lst = BLL.Purchse.Instance.list_tasks(BLL.UserHelper.Instance.GetUser().Id, "InProcess", 1, 10).ToList();
            //ViewBag.count = lst.Count > 0 ? lst[0].rowCnt : 0;
            //lst = null;

            //int userId = BLL.UserHelper.Instance.GetUser().Id;

            //var profitData = _sale.getProfit(userId);

            //if (profitData.Count > 0)
            //{
            //    return Json(new { total = profitData.Count, rows = profitData, state = true, msg = "" }, JsonRequestBehavior.AllowGet);

         
            //}
            //else {
            //    return Json(new { state = true, msg = "" }, JsonRequestBehavior.AllowGet);

            //}
            //int userId = BLL.UserHelper.Instance.GetUser().Id;
            //var profitData = _sale.getProfit(userId);
            //ViewBag.data=profitData;

            //return View();
        }



        //public    ActionResult ListSalesDataClient(string fileUrl, string saler, string rate)
        //{

        //    int userId = BLL.UserHelper.Instance.GetUser().Id;

        //    if (!fileUrl.Equals(""))
        //    {
        //        if (rate.Equals(""))
        //        {

        //            return null;
        //        }

        //        //return null;
        //        //   int userId = BLL.UserHelper.Instance.GetUser().Id;
        //        // var kvlst = _sale.calculateInterest(fileUrl, rate, userId);
        //        string fullname = Path.Combine(Server.MapPath("~/uploadfiles/"), fileUrl);
        //        DataTable dt = ToolKits.ExcelHelper.Read(fullname, 4);

        //        var kvlst = _sale.calculateInterest(dt, rate, userId);


        //        return kvlst;
        //    }
        //    else
        //    {


        //        var profitData = _sale.getProfit(userId);
        //        return profitData;

        //        //if (profitData.Count > 0)
        //        //{
        //        //    r

        //        //}


        //    }
        //   // return Json(new { total = 0, state = true, msg = "" }, JsonRequestBehavior.AllowGet);
    
        //}


        [NetAuthorize(userRole = "9")] 
        //[NetAuthorize(userRole = "1")]
        [HttpPost]
        public JsonResult editStock(string userid, int qtyGap, string sku)
        {

            int intUserid = Convert.ToInt32(userid);

            int stock = _stock.Update(intUserid, qtyGap, sku);

          
           
           if (stock > 0)
           {
               return Json(new { state = false, msg = "更新失败，历史进货量无法满足更新库存要求" }, JsonRequestBehavior.AllowGet);

           }

           return Json(new { state = true, msg = "更新成功" }, JsonRequestBehavior.AllowGet);

          
        }

        [NetAuthorize(userRole = "9")] 
        public JsonResult ListAllStock(int pageIndex, int pageSize,  string saler)
        {

            int userId = saler.Equals("") ? 0 : Convert.ToInt32(saler);

            var stockData = _stock.GetAllStock(pageIndex, pageSize, userId);
            Dictionary<string,string> userData = _stock.GetUserInfo();
            if (stockData.Key > 0) {

               // List<Model.ProfitInfo> profitLst = profitData.Value.ToList();
                foreach (Model.StockInfo stock in stockData.Value) {

                    if (userData.ContainsKey(Convert.ToString( stock.modUserid))) {

                        stock.userName = userData[Convert.ToString(stock.modUserid)];
                    }
                }
            
            }

            return Json(new { total = stockData.Key, rows = stockData.Value, state = true, msg = "" }, JsonRequestBehavior.AllowGet);

        }



        //public JsonResult ListAllSalesData(int pageIndex, int pageSize, string startDate,string endDate, string userid)
        //{
        //    int userId = userid.Equals("")?0: Convert.ToInt32(userid);

        //    var profitData = _sale.getAllProfit(pageIndex, pageSize, startDate,endDate, userId);

           

        //    decimal totalProfit = 0;

        //    if (profitData.Key > 0) {
        //        List<Model.ProfitInfo> profitLst = profitData.Value.ToList();
        //        foreach (Model.ProfitInfo pro in profitLst)
        //        {

        //            totalProfit = totalProfit + pro.profit;

        //        }
        //    }

          

        //    return Json(new { total = profitData.Key, rows = profitData.Value, totalProfit = totalProfit, state = true, msg = "" }, JsonRequestBehavior.AllowGet);

        
        
        //}


        //public JsonResult ListSalesData(string fileUrl, string saler,string rate,int pageIndex,int PageSize,string startDate,string endDate)
        //{
        //    int userId = BLL.UserHelper.Instance.GetUser().Id;

        //    if (!fileUrl.Equals(""))
        //    {
        //        if (rate.Equals(""))
        //        {

        //            return Json(new { state = false, msg = "利率为空" }, JsonRequestBehavior.AllowGet);
        //        }


        //        bool hasData = _sale.checkData(userId);

        //        if (hasData) {

        //            return Json(new { state = false, msg = "上月销售数据已上传，不能重复上传" }, JsonRequestBehavior.AllowGet); 
        //        }

        //        //return null;
        //     //   int userId = BLL.UserHelper.Instance.GetUser().Id;
        //        // var kvlst = _sale.calculateInterest(fileUrl, rate, userId);
        //        string fullname = Path.Combine(Server.MapPath("~/uploadfiles/"), fileUrl);
        //        DataTable dt = ToolKits.ExcelHelper.Read(fullname, 4);

        //        var kvlst = _sale.calculateInterest(dt, rate, userId);


        //      //  return Json(new { total = kvlst.Count, rows = kvlst, state = true, msg = "" }, JsonRequestBehavior.AllowGet);


        //    }
           
          

        //     var profitData = _sale.getProfit(userId,pageIndex,PageSize,startDate,endDate);

            
        //    decimal totalProfit=0;

        //    if (profitData.Key > 0) {

        //        List<Model.ProfitInfo> profitLst = profitData.Value.ToList();
        //        foreach (Model.ProfitInfo pro in profitLst)
        //        {

        //            totalProfit = totalProfit + pro.profit;

        //        }




        //    }

           
               
        //     return Json(new { total = profitData.Key, rows = profitData.Value,totalProfit=totalProfit, state = true, msg = "" }, JsonRequestBehavior.AllowGet);

                

            
            
        //  //  return Json(new { total = 0, state = true, msg = "" }, JsonRequestBehavior.AllowGet);
    

         
        //   // return null;
        //    //List<Model.ProfitInfo> lst = _sale.calculateInterest(fileUrl, rate, userId).ToList();

        //    //var total = lst.Count > 0 ? lst[0].rowCnt : 0;
        //   // return Json(new { total = kvlst.Key, rows = kvlst.Value, state = true, msg = "" }, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your app description page.";

        //    return View();
        //}

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
