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
    public class SaleController : Controller
    {

        BLL.Sale _sale = new BLL.Sale();
        public JsonResult ListSales(string query)
        {
            var users = _sale.List_Sales(query);

            //2.2返回json  
            return Json(users, JsonRequestBehavior.AllowGet);
        }


        public JsonResult UploadExcel()
        {
            string filePath = Server.MapPath("~/UploadFiles/");
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            HttpFileCollection upload = System.Web.HttpContext.Current.Request.Files;
            try
            {
                HttpPostedFileBase file = Request.Files[0];
                string ext = Path.GetExtension(file.FileName).ToLower();

                

                string saveName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName).Replace("_", ""); // 保存文件名称
                string fileFullPath = string.Format("{0}{1}", filePath, saveName);
                file.SaveAs(fileFullPath);
                string excelUrl = saveName;
                return Json(new { state = true, Message = "success", excelUrl }, JsonRequestBehavior.AllowGet);
                //DataTable tb = ToolKits.ExcelHelper.Read_BOM(fileFullPath);
                //try
                //{
                //    var lst = ToolKits.ModelConvertHelper<Model.tbBOM>.ConvertToModel(tb);
                //    foreach (var obj in lst)
                //    {
                //        obj.ChgNum = ChgId;
                //        obj.ModUser = ToolKits.CookieHelper.GetCookieValue(Model.clsDefine.cookie.cookieName, Model.clsDefine.cookie.UserId);

                //    }

                //    _bom.Add_AffectedParts(lst.ToList()); //affectedPNs

                //    return Json(new { state = true, msg = "Success", rows = lst }, JsonRequestBehavior.AllowGet);
                //}
                //catch (Exception ex)
                //{
                //    return Json(new { state = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                //}

            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

       [NetAuthorize(userRole = "10")] 
        public ActionResult AllProfit()
        {
            return View();

        }

        [NetAuthorize(userRole = "1")] 
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


        [NetAuthorize(userRole = "1")]
        [HttpPost]
        public JsonResult editProfit(int id,double profit)
        {
            _sale.Update(id,profit);
            return Json(new { state = true, msg = "更新成功" }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ListAllSalesData(int pageIndex, int pageSize, string startDate,string endDate, string userid)
        {
            int userId = userid.Equals("")?0: Convert.ToInt32(userid);

            pageSize = pageSize - 1;

            var profitData = _sale.getAllProfit(pageIndex, pageSize, startDate,endDate, userId);

            Model.ProfitInfo profit = new Model.ProfitInfo();

            if (profitData.Key > 0)
            {
                //profit.userName = "合计";
                List<Model.ProfitInfo> profitLst = profitData.Value.ToList();
                foreach (Model.ProfitInfo pro in profitLst)
                {

                    //   totalProfit = totalProfit + pro.profit;

                    profit.amazonCost = profit.amazonCost + pro.amazonCost;
                    profit.amazonDamage = profit.amazonDamage + pro.amazonDamage;
                    profit.amazonLost = profit.amazonLost + pro.amazonLost;
                    profit.amazonReturning = profit.amazonReturning + pro.amazonReturning;
                    profit.goodReturn = profit.goodReturn + pro.goodReturn;
                    profit.grandTotal = profit.grandTotal + pro.grandTotal;
                    profit.number = profit.number + pro.number;
                    profit.other = profit.other + pro.other;
                    profit.price = profit.price + pro.price;
                    profit.profit = profit.profit + pro.profit;
                    profit.proRebate = profit.proRebate + pro.proRebate;
                    profit.rmbTotal = profit.rmbTotal + pro.rmbTotal;
                    profit.totalCost = profit.totalCost + pro.totalCost;
                    profit.profitMonth = pro.profitMonth;
                    profit.transDetails = profit.transDetails + pro.transDetails;

                    profit.sku = "合计";



                }



                profitData.Value.Add(profit);
            }

            //decimal totalProfit = 0;

            //if (profitData.Key > 0) {
            //    List<Model.ProfitInfo> profitLst = profitData.Value.ToList();
            //    foreach (Model.ProfitInfo pro in profitLst)
            //    {

            //        totalProfit = totalProfit + pro.profit;

            //    }
            //}

          

            return Json(new { total = profitData.Key, rows = profitData.Value,  state = true, msg = "" }, JsonRequestBehavior.AllowGet);

        
        
        }


        [NetAuthorize(userRole = "1")] 
        public JsonResult ListSalesData(string fileUrl, string saler,string rate,int pageIndex,int PageSize,string startDate,string endDate)
        {
            int userId = BLL.UserHelper.Instance.GetUser().Id;

            if (!fileUrl.Equals(""))
            {
                if (rate.Equals(""))
                {

                    return Json(new { state = false, msg = "利率为空" }, JsonRequestBehavior.AllowGet);
                }


                bool hasData = _sale.checkData(userId);

                if (hasData) {

                    return Json(new { state = false, msg = "上月销售数据已上传，不能重复上传" }, JsonRequestBehavior.AllowGet); 
                }

                //return null;
             //   int userId = BLL.UserHelper.Instance.GetUser().Id;
                // var kvlst = _sale.calculateInterest(fileUrl, rate, userId);
                string fullname = Path.Combine(Server.MapPath("~/uploadfiles/"), fileUrl);
                DataTable dt = ToolKits.ExcelHelper.Read(fullname, 4);

                var kvlst = _sale.calculateInterest(dt, rate, userId);


              //  return Json(new { total = kvlst.Count, rows = kvlst, state = true, msg = "" }, JsonRequestBehavior.AllowGet);


            }



             PageSize = PageSize - 1;

             var profitData = _sale.getProfit(userId,pageIndex,PageSize,startDate,endDate);

             Model.ProfitInfo profit = new Model.ProfitInfo();


             if (profitData.Key > 0)
             {
                 //profit.userName = "合计";
                 List<Model.ProfitInfo> profitLst = profitData.Value.ToList();
                 foreach (Model.ProfitInfo pro in profitLst)
                 {

                  //   totalProfit = totalProfit + pro.profit;

                     profit.amazonCost = profit.amazonCost + pro.amazonCost;
                     profit.amazonDamage = profit.amazonDamage + pro.amazonDamage;
                     profit.amazonLost = profit.amazonLost + pro.amazonLost;
                     profit.amazonReturning = profit.amazonReturning + pro.amazonReturning;
                     profit.goodReturn = profit.goodReturn + pro.goodReturn;
                     profit.grandTotal = profit.grandTotal + pro.grandTotal;
                     profit.number = profit.number + pro.number;
                     profit.other = profit.other + pro.other;
                     profit.price = profit.price + pro.price;
                     profit.profit = profit.profit + pro.profit;
                     profit.proRebate = profit.proRebate + pro.proRebate;
                     profit.rmbTotal = profit.rmbTotal + pro.rmbTotal;
                     profit.totalCost = profit.totalCost + pro.totalCost;
                     profit.profitMonth = pro.profitMonth;
                     profit.transDetails = profit.transDetails + pro.transDetails;

                     profit.sku = "合计";
                     


                 }



                 profitData.Value.Add(profit);
             }

            // profitData.Key = profitData.Key + 1;
          
            
            //decimal totalProfit=0;

            //if (profitData.Key > 0) {

            //    List<Model.ProfitInfo> profitLst = profitData.Value.ToList();
            //    foreach (Model.ProfitInfo pro in profitLst)
            //    {

            //        totalProfit = totalProfit + pro.profit;

            //    }




            //}

           
               
             return Json(new { total = profitData.Key+1, rows = profitData.Value,state = true, msg = "" }, JsonRequestBehavior.AllowGet);

                

            
            
          //  return Json(new { total = 0, state = true, msg = "" }, JsonRequestBehavior.AllowGet);
    

         
           // return null;
            //List<Model.ProfitInfo> lst = _sale.calculateInterest(fileUrl, rate, userId).ToList();

            //var total = lst.Count > 0 ? lst[0].rowCnt : 0;
           // return Json(new { total = kvlst.Key, rows = kvlst.Value, state = true, msg = "" }, JsonRequestBehavior.AllowGet);
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
