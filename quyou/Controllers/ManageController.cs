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
    public class ManageController : Controller
    {
        BLL.Port _port = new BLL.Port();
        BLL.Supplier _supplier = new BLL.Supplier();
        BLL.SKU _sku = new BLL.SKU();




        //SKU management

        [NetAuthorize(userRole = "1")]
        public ActionResult SKU()
        {


            return View();
        }


        [NetAuthorize(userRole = "1")]
        public JsonResult ListSKUs(int pageIndex, int pageSize, string search, string sort, string sortOrder)
        {
            var kvlst = _sku.List(search, pageIndex, pageSize, sort, sortOrder, search);
            //var rows = lst.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();

            return Json(new { total = kvlst.Key, rows = kvlst.Value, state = true, msg = "" }, JsonRequestBehavior.AllowGet);
        }

        [NetAuthorize(userRole = "1")]
        public JsonResult UploadSKUPic()
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
                string imgUrl = saveName;
                return Json(new { state = true, Message = "success", imgUrl }, JsonRequestBehavior.AllowGet);
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


        [NetAuthorize(userRole = "1")]
        [HttpPost]
        public JsonResult EditSKU(Model.SKUInfo skuInfo)
        {

            int count = _sku.Update(skuInfo);

            // return View(skuInfo);
            if (count == 1)
            {

                return Json(new { state = true, msg = "操作成功" }, JsonRequestBehavior.AllowGet);
            }
            else if (count == 2)
            {
                return Json(new { state = false, msg = "SKU不能重复" }, JsonRequestBehavior.AllowGet);

            }

            return Json(new { state = false, msg = "操作失败" }, JsonRequestBehavior.AllowGet);

        }

        [NetAuthorize(userRole = "1")]
        [HttpPost]
        public JsonResult removeSKU(int id)
        {
            _sku.Delete(id);
            return Json(new { state = true, msg = "删除成功" }, JsonRequestBehavior.AllowGet);
        }


        [NetAuthorize(userRole = "1")]
        public ActionResult EditSKU(string id)
        {
            //ViewBag.act = "New";

            //using (CuteWebUI.MvcUploader uploader = new CuteWebUI.MvcUploader(System.Web.HttpContext.Current))
            //{
            //    uploader.UploadUrl = Response.ApplyAppPathModifier("~/UploadHandler.ashx");
            //    uploader.Name = "myuploader";
            //    uploader.AllowedFileExtensions = "*.jpg,*.gif,*.png,*.bmp,*.zip,*.rar";
            //    uploader.InsertText = "Select a file to upload";
            //    ViewData["uploaderhtml"] = uploader.Render();
            //}
            ViewBag.act = "New";
            if (id != null)
            {
                ViewBag.act = "Edit";
                ViewBag.id = id;
                var skuInfos = _sku.GetSKUInfo(id);

                Model.SKUInfo skuInfo = skuInfos[0];
                ViewBag.img = skuInfo.imgUrl;
                return View(skuInfo);
            }
            else
            {
                //ViewBag.TeamId = BLL.DBHelper.Get_RequestNum(Model.clsDefine.EngChangeType.Team);
                // Model.SupplierInfo supplier = new Model.SupplierInfo();
                //  Model.UserInfo user = BLL.UserHelper.Instance.GetUser();
                //    portInfo.UserName = user.name;
                return View();
            }

        }

        //[HttpPost]
        //public JsonResult EditSupplier(Model.SupplierInfo supplier)
        //{

        //    //int count = _port.Update(supplier);


        //    //if (count == 1)
        //    //{

        //    //    return Json(new { state = true, msg = "操作成功" }, JsonRequestBehavior.AllowGet);
        //    //}

        //    return Json(new { state = false, msg = "操作失败" }, JsonRequestBehavior.AllowGet);

        //}

        [NetAuthorize(userRole = "1,5")]

        [HttpPost]
        public JsonResult EditSupplier(Model.SupplierInfo supplier)
        {

            int count = _supplier.Update(supplier);


            if (count == 1)
            {

                return Json(new { state = true, msg = "操作成功" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { state = false, msg = "操作失败" }, JsonRequestBehavior.AllowGet);

        }
        [NetAuthorize(userRole = "1,5")]
        [HttpPost]
        public JsonResult RemoveSupplier(int id)
        {
            _supplier.Delete(id);
            return Json(new { state = true, msg = "删除成功" }, JsonRequestBehavior.AllowGet);
        }


        [NetAuthorize(userRole = "1,5")]
        public ActionResult EditSupplier(string id)
        {
            //ViewBag.act = "New";
            ViewBag.act = "New";
            if (id != null)
            {
                ViewBag.act = "Edit";
                ViewBag.id = id;
                var sppliers = _supplier.GetSupplierInfo(id);
                Model.SupplierInfo supplier = sppliers[0];
                return View(supplier);
            }
            else
            {
                //ViewBag.TeamId = BLL.DBHelper.Get_RequestNum(Model.clsDefine.EngChangeType.Team);
                // Model.SupplierInfo supplier = new Model.SupplierInfo();
                //  Model.UserInfo user = BLL.UserHelper.Instance.GetUser();
                //    portInfo.UserName = user.name;
                return View();
            }

        }



        [NetAuthorize(userRole = "5")]
        public ActionResult EditPort(string id)
        {
            //ViewBag.act = "New";
            ViewBag.act = "New";
            if (id != null)
            {
                ViewBag.act = "Edit";
                ViewBag.id = id;
                var port = BLL.DBHelper.GetPortInfo(id);
                Model.PortInfo portInfo = port[0];
                return View(portInfo);
            }
            else
            {
                //ViewBag.TeamId = BLL.DBHelper.Get_RequestNum(Model.clsDefine.EngChangeType.Team);
                Model.PortInfo portInfo = new Model.PortInfo();
                Model.UserInfo user = BLL.UserHelper.Instance.GetUser();
                portInfo.UserName = user.name;
                return View(portInfo);
            }

        }

        [NetAuthorize(userRole = "1,5")]
        public JsonResult ListSupplier(int pageIndex, int pageSize, string search, string sort, string sortOrder)
        {
            var kvlst = _supplier.List(search, pageIndex, pageSize, sort, sortOrder, search);
            //var rows = lst.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();

            return Json(new { total = kvlst.Key, rows = kvlst.Value, state = true, msg = "" }, JsonRequestBehavior.AllowGet);
        }




        [NetAuthorize(userRole = "5")]
        public JsonResult ListPorts(int pageIndex, int pageSize, string search, string sort, string sortOrder)
        {
            var kvlst = _port.List(search, pageIndex, pageSize, sort, sortOrder, search);
            //var rows = lst.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();

            return Json(new { total = kvlst.Key, rows = kvlst.Value, state = true, msg = "" }, JsonRequestBehavior.AllowGet);
        }

        [NetAuthorize(userRole = "5")]
        public JsonResult ReadPort(string id)
        {
            //string createUser = ToolKits.CookieHelper.GetCookieValue(Model.clsDefine.cookie.cookieName, Model.clsDefine.cookie.Email);
            //var te = _myteam.Read(TeamId, createUser);


            var port = BLL.DBHelper.GetPortInfo(id);

            return Json(port, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [NetAuthorize(userRole = "5")]
        public JsonResult RemovePort(int id)
        {
            _port.Delete(id);
            return Json(new { state = true, msg = "删除成功" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [NetAuthorize(userRole = "5")]
        public JsonResult EditPort(Model.PortInfo portInfo)
        {

            int count = _port.Update(portInfo);


            if (count == 1)
            {

                return Json(new { state = true, msg = "操作成功" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { state = false, msg = "操作失败" }, JsonRequestBehavior.AllowGet);

        }


        [NetAuthorize(userRole = "1,5")]

        public ActionResult Supplier()
        {

            return View();
        }

        [NetAuthorize(userRole = "5")]
        public ActionResult Port()
        {

            return View();

        }
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

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
