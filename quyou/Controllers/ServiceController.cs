using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using quyou.Filters;
using System.IO;
using System.Data;
namespace quyou.Controllers
{
    [NetAuthorize]
    public class ServiceController : Controller
    {
        //
        // GET: /Service/

        /// <summary>
        /// 采购数据批量上传by po number
        /// </summary>
        /// <param name="PNbr"></param>
        /// <param name="actType"></param>
        /// <returns></returns>
        public JsonResult UploadPO(string PNbr, string actType)
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

                string saveName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName); // 保存文件名称
                string fileFullPath = string.Format("{0}{1}", filePath, saveName);
                file.SaveAs(fileFullPath);

                DataTable tb = ToolKits.ExcelHelper.Read_PO(fileFullPath);
                try
                {
                    tb.Columns.Add("SupplierId", typeof(Int32));
                    foreach (DataRow dr in tb.Rows)
                    {
                        dr["SupplierId"] = BLL.MgrMent.Instance.Get_Supplier(dr["Supplier"].ToString()).Id;
                    }

                    var lst = ToolKits.ModelConvertHelper<Model.tbMain>.ConvertToModel(tb);
                    foreach (var obj in lst)
                    {
                        obj.PNbr = PNbr;

                    }

                   var rst= BLL.Purchse.Instance.Add_tbMain_list(lst);
                    if(!string.IsNullOrEmpty(rst))
                    {
                        return Json(new { state = false, msg = rst }, JsonRequestBehavior.AllowGet);
                    }

                    Model.tbPO mod = new Model.tbPO { PNbr = PNbr, ModUserId = BLL.UserHelper.Instance.GetUser().Id };   
                    BLL.Purchse.Instance.act_yy(mod,0, actType, string.Empty);   //上传PO后自动保存
                   
                    return Json(new { state = true, msg = "Success" }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { state = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        /// <summary>
        /// 上传文档
        /// </summary>
        /// <param name="ChgId"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Upload(string PNbr, string filetype)
        {
            string filePath = Server.MapPath("~/UploadFiles/");
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            HttpFileCollection upload = System.Web.HttpContext.Current.Request.Files;
            try
            {
                Model.clsDefine.retutnMsg rtnmsg = new Model.clsDefine.retutnMsg { state = true, msg = "上传成功！" };
                for (int i = 0; i < upload.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];

                    string ext = Path.GetExtension(file.FileName).ToLower();

                    //string saveName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName); // 保存文件名称
                    string saveName = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + Path.GetFileName(file.FileName);
                    string fileFullPath = string.Format("{0}{1}", filePath, saveName);
                    file.SaveAs(fileFullPath);

                    Model.tbFile mod = new Model.tbFile { PNbr = PNbr, Ftype = filetype, FName = saveName, ModUserId = BLL.UserHelper.Instance.GetUser().Id, StatusId = BLL.DBHelper.Get_Status(Model.clsDefine.StatusName.Active).Id, Cdt = System.DateTime.Now };
                    rtnmsg = BLL.tbFile.Instance.Add(mod);
          
                }
                return Json(rtnmsg, JsonRequestBehavior.AllowGet);           
            }
            catch (Exception ex)
            {
                return Json(new Model.clsDefine.retutnMsg { state = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        public JsonResult DelFile(int id, string fileName)
        {
            //string filePath = Server.MapPath("~/UploadFiles/DCR/");

            string filePath = System.Web.HttpContext.Current.Server.MapPath("~/UploadFiles/");
            string fileFullPath = string.Format("{0}{1}", filePath, fileName);
            //string filepathtest = Path.Combine(filePath, fileName);
            if (System.IO.File.Exists(fileFullPath))
            {
                try
                {
                    //System.IO.File.Delete(fileFullPath);
                    BLL.tbFile.Instance.Delete(id);
                    return Json(new { state = true, msg = string.Empty }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    return Json(new { state = false, msg = ex.Message }, JsonRequestBehavior.AllowGet);
                }

            }
            return Json(new { state = false, msg = "file is not exist." }, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public JsonResult ListDocs(string RqstNum, string fileType, int userId, int pageIndex, int pageSize)
        {
            IList<Model.sp_proc_file_Result> lst = BLL.tbFile.Instance.ListDocs(RqstNum, fileType);

            var psz = lst.Skip(pageSize * (pageIndex - 1)).Take(pageSize);


            return Json(new { total = lst.Count, rows = psz, state = true, msg = "" }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// list SKU for select
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public JsonResult List_SKUs(string query)
        {
           
            IList<Model.clsDefine.selectOption> customers = BLL.DBHelper.List_SKU(query,BLL.UserHelper.Instance.GetUser().Id);
            return Json(customers, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// list Port for select
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public JsonResult List_Port(string query)
        {
            IList<Model.clsDefine.selectOption> portlist = BLL.DBHelper.List_Port(query);
            return Json(portlist, JsonRequestBehavior.AllowGet);
        }


        public JsonResult list_Supplier(string query)
        {
            IList<Model.clsDefine.selectOption> suplist = BLL.DBHelper.List_Supplier(query);
            return Json(suplist, JsonRequestBehavior.AllowGet);
        }
        public JsonResult get_supplier_by_id(int id)
        {
            var obj = BLL.DBHelper.Get_by_Id(id);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// list Currency
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public JsonResult List_Currency(string query)
        {
            IList<Model.clsDefine.selectOption> portlist = BLL.DBHelper.List_Currency(query);
            return Json(portlist, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ListTask(string state, int pageIndex, int pageSize)
        {
            int userId = BLL.UserHelper.Instance.GetUser().Id;
            List<Model.TskForDisplay> lst = BLL.Purchse.Instance.list_tasks(userId, state, pageIndex, pageSize).ToList();
            //List<Model.TskForDisplay> lst = BLL.Purchse.Instance.list_tasks(userId,Model.clsDefine.TaskCategory.POSave, pageIndex, pageSize).ToList();
            var total = lst.Count > 0 ? lst[0].rowCnt : 0;
            return Json(new { total = total, rows = lst, state = true, msg = "" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取指定采购单号的详细信息
        /// </summary>
        /// <param name="RqstNum"></param>
        /// <returns></returns>
        public JsonResult List_PO(string pnbr)
        {
            return Json(BLL.Purchse.Instance.List_tbMain(pnbr), JsonRequestBehavior.AllowGet);
        }

        public JsonResult List_logistics(string pnbr,string islistSKU)
        {          
            var lgcs = BLL.Logistics.Instance.List(pnbr, islistSKU);
            return Json(lgcs, JsonRequestBehavior.AllowGet);
        }

        public JsonResult List_logistics2(int BPId)
        {
            return Json(BLL.Logistics.Instance.List2(BPId, BLL.UserHelper.Instance.GetUser().Id), JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 获取指定采购单号的详细信息
        /// </summary>
        /// <param name="RqstNum"></param>
        /// <returns></returns>
        public JsonResult List_POForDisplay(string pnbr)
        {
            return Json(BLL.Purchse.Instance.List_PO(pnbr), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取历史操作记录
        /// </summary>
        /// <param name="RqstNum"></param>
        /// <returns></returns>
        public JsonResult List_LogForDisplay(int BPId,string tsk)
        {
            return Json(BLL.Purchse.Instance.Get_Log_By_tsk(BPId, tsk), JsonRequestBehavior.AllowGet);
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
        public void deletePO(string pnbr)
        {
            BLL.Purchse.Instance.delPO(pnbr, BLL.UserHelper.Instance.GetUser().Id);
        }



    }
}
