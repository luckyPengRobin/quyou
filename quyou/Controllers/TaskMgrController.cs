using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using quyou.Filters;
using Model;
namespace quyou.Controllers
{
    [NetAuthorize]
    [Authorize]
    public class TaskMgrController : Controller
    {
        //
        // GET: /TaskMgr/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult InProcess()
        {
            return View();
        }
        public ActionResult Draft()
        {
            return View();
        }


        #region .....审批....
        public ActionResult ApprovalAct(int BPId)
        {         
            var mod = BLL.Purchse.Instance.Get("",BPId);
            return View(mod);   
         
        }


        [HttpPost]
        public JsonResult ApprovalAct(int BPId, string pnbr, string coments, string actType)
        {
           
            if (BPId > 0 || !string.IsNullOrEmpty(pnbr)) 
            {
                Model.tbPO mod = new tbPO { PNbr = pnbr, ModUserId = BLL.UserHelper.Instance.GetUser().Id };
                var rst = BLL.Purchse.Instance.task_Act(mod, BPId, actType, coments);
                return Json(rst, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Model.clsDefine.retutnMsg rst = new clsDefine.retutnMsg { state = false, msg = "采购单号未发现或流程问题" };
                return Json(rst, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion


        #region  ...采购...
        public ActionResult purchase(int BPId)
        {
            var mod = BLL.Purchse.Instance.Get("", BPId);
            return View(mod);  
        }
        [HttpPost]
        public JsonResult purchase(int BPId, string pnbr, string coments, string actType,string finanlqtylist)
        {

            if (BPId > 0 || !string.IsNullOrEmpty(pnbr))
            {

                #region 更新实际采购数量
                IList<Model.clsDefine.selectOption> objList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.clsDefine.selectOption>>(finanlqtylist);

                foreach(var it in objList)
                {
                    BLL.Purchse.Instance.update_FinalQty(int.Parse(it.id.ToString()), it.text);
                }

                #endregion


                Model.tbPO mod = new tbPO { PNbr = pnbr, ModUserId = BLL.UserHelper.Instance.GetUser().Id };
                var rst = BLL.Purchse.Instance.task_Act(mod, BPId, actType, coments);               
                return Json(rst, JsonRequestBehavior.AllowGet);



            }
            else
            {
                Model.clsDefine.retutnMsg rst = new clsDefine.retutnMsg { state = false, msg = "采购单号未发现或流程问题" };
                return Json(rst, JsonRequestBehavior.AllowGet);
            }
        }



        #endregion

        #region   .....账务采购支付....

         [NetAuthorize(userRole = "4")]
        public ActionResult POPay(int BPId)
        {
            var mod = BLL.Purchse.Instance.Get("", BPId);

            //ViewBag.amount = BLL.Purchse.Instance.Get_PO_Amount(mod.PNbr);

            return View(mod);


        }

         [HttpPost]
         [NetAuthorize(userRole = "4")]
         public JsonResult POPay(int BPId, string pnbr, string coments, string actType)
         {
            
             Model.tbPO mod = new tbPO { PNbr = pnbr, ModUserId = BLL.UserHelper.Instance.GetUser().Id };
             var rst = BLL.Purchse.Instance.task_Act(mod, BPId, actType, coments);
             return Json(rst, JsonRequestBehavior.AllowGet);


         }


        /// <summary>
        /// 账务物流关税支付
        /// </summary>
        /// <param name="BPId"></param>
        /// <returns></returns>
         [NetAuthorize(userRole = "4")]
        public ActionResult TaxPay(int BPId)
         {
             var mod = BLL.Logistics.Instance.Get(BPId, BLL.UserHelper.Instance.GetUser().Id);
             return View(mod);
         }


         [HttpPost]
         [NetAuthorize(userRole = "4")]
         public JsonResult Finance(int BPId, string pnbr, string coments, string actType)
         {
             Model.tbPO mod = new tbPO { PNbr = pnbr, ModUserId = BLL.UserHelper.Instance.GetUser().Id };
             var rst = BLL.Purchse.Instance.task_Act(mod, BPId, actType, coments);
             return Json(rst, JsonRequestBehavior.AllowGet);
         }


        #endregion


         #region   .....物流 发货....

         #region 发货
         [NetAuthorize(userRole = "5")]
         public ActionResult logistics(int BPId)
         {
             BLL.Logistics.Instance.check_Add("", BPId);   //检查物流数据
             var mod = BLL.Purchse.Instance.Get("", BPId);
             return View(mod);
         }

         [HttpPost]
         [NetAuthorize(userRole = "5")]
         public JsonResult logistics(int BPId, string pnbr, string coments, string actType,string LSupps)
         {


             #region 更新货运公司
             IList<Model.clsDefine.selectOption> objList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.clsDefine.selectOption>>(LSupps);

             foreach (var it in objList)
             {
                 BLL.Purchse.Instance.update_LgstcSupplier(int.Parse(it.id.ToString()), int.Parse(it.text));
             }

             #endregion



             Model.tbPO mod = new tbPO { PNbr = pnbr, ModUserId = BLL.UserHelper.Instance.GetUser().Id };
             var rst = BLL.Purchse.Instance.task_Act(mod, BPId, actType, coments);
             return Json(rst, JsonRequestBehavior.AllowGet);


         }
         #endregion
        

         #region 到货确认
         [NetAuthorize(userRole = "5")]
         public ActionResult logicConfirm(int BPId)
         {
             var mod = BLL.Logistics.Instance.Get(BPId, BLL.UserHelper.Instance.GetUser().Id);
             return View(mod);


         }

         [HttpPost]
         [NetAuthorize(userRole = "5")]
         public JsonResult logicConfirm(int BPId, string pnbr, string actType, string lgcChrgs)
         {

             #region 更新物流费用
             if(lgcChrgs != null)
             {
                 //IList<Model.LgsticForDisplay> objList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.LgsticForDisplay>>(lgcChrgs);
                 Model.LgsticForDisplay obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.LgsticForDisplay>(lgcChrgs);

                 BLL.Logistics.Instance.update(obj, BLL.UserHelper.Instance.GetUser().Id);
                 
             }

             #endregion


             Model.tbPO mod = new tbPO { PNbr = pnbr, ModUserId = BLL.UserHelper.Instance.GetUser().Id };
             var rst = BLL.Purchse.Instance.task_Act(mod, BPId, actType, "logicConfirm");
             return Json(rst, JsonRequestBehavior.AllowGet);


         }
         //public JsonResult logicConfirm(FormCollection collection, string actType)
         //{

         //    Model.LgsticForDisplay obj = new LgsticForDisplay();
         //    TryUpdateModel<Model.LgsticForDisplay>(obj, collection);
         //    BLL.Logistics.Instance.update(obj);


         //    Model.tbPO mod = new tbPO { PNbr = obj.PNbr, ModUserId = BLL.UserHelper.Instance.GetUser().Id };
         //    var rst = BLL.Purchse.Instance.task_Act(mod, obj.BPId, actType, obj.confirmComments);
         //    return Json(rst, JsonRequestBehavior.AllowGet);


         //}

         #endregion

         #endregion



         /// <summary>
         /// 运营对采购单进行确认
         /// </summary>
         /// <param name="BPId"></param>
         /// <returns></returns>
         [NetAuthorize(userRole = "1")]
         public ActionResult ConfirmPO(int BPId)
         {
             var mod = BLL.Purchse.Instance.Get("", BPId);
             return View(mod);
         }

         [NetAuthorize(userRole = "1")]
         [HttpPost]
         public JsonResult ConfirmPO(int BPId, string pnbr, string coments, string actType)
         {
             Model.tbPO mod = new tbPO { PNbr = pnbr, ModUserId = BLL.UserHelper.Instance.GetUser().Id };
             var rst = BLL.Purchse.Instance.task_Act(mod, BPId, actType, coments);
             return Json(rst, JsonRequestBehavior.AllowGet);

         }





    }
}
