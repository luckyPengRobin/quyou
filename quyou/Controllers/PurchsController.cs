using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using quyou.Filters;
using Model;
namespace quyou.Controllers
{
    [Authorize]
   [NetAuthorize]
    public class PurchsController : Controller
    {
        //
        // GET: /Purchs/

        public ActionResult Index()
        {
            return View();
        }

       [NetAuthorize(userRole="1")]
        public ActionResult New(string PNbr)
        {
            var mod = new Model.tbPO();

            if (!string.IsNullOrEmpty(PNbr))
            {
                mod = BLL.Purchse.Instance.Get_PO(PNbr);
                return View(mod);   //for edit
            }
            mod.PNbr = BLL.DBHelper.Get_IndexNbr(Model.clsDefine.RqstType.Purchase);
            return View(mod);   //New

        }

        /// <summary>
        /// 新增采购单
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult New(string pnbr, string PurchsData, string actType)
        {
            Model.tbPO mod = new tbPO { PNbr = pnbr, ModUserId = BLL.UserHelper.Instance.GetUser().Id };             
            IList<Model.tbMain> POList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.tbMain>>(PurchsData);
            bool flag = false;
            if(POList.Count >0)
            {
                BLL.Purchse.Instance.Add_tbMain_list(POList); flag = true;   
            }
            else if(BLL.Purchse.Instance.Is_exist_PO(pnbr))
            {
                flag = true;
            }

            if(flag)
            {
                BLL.Purchse.Instance.act_yy(mod,0, actType, string.Empty);
                return Json(new { state = true, msg = "Success" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { state = false, msg = "请先填写数据" }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除指定采购单下的一条数据
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public void delte_po(string obj)
        {
            Model.tbMain mod = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.tbMain>(obj);
            BLL.Purchse.Instance.delete_po(mod, BLL.UserHelper.Instance.GetUser().Id);
        }
        /// <summary>
        /// Cancel PO
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public void Cancel_po(string pnbr,string actType)
        {
            Model.tbPO mod = new tbPO { PNbr = pnbr, ModUserId = BLL.UserHelper.Instance.GetUser().Id };          
            BLL.Purchse.Instance.act_yy(mod, 0, actType, string.Empty);
        }
        /// <summary>
        /// 更新货运公司信息
        /// </summary>
        /// <param name="mainId"></param>
        /// <param name="supplierId"></param>
        public void update_lgcSupplier(int mainId,int supplierId)
        {
            BLL.Purchse.Instance.update_LgstcSupplier(mainId, supplierId);
        }





    }
}
