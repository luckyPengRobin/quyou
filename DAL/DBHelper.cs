using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToolKits;
namespace DAL
{
   public class DBHelper
    {
       public static string Get_IndexNbr(string reqstType)
       {
           return ToolKits.DbHelperSQL.GetSingle("exec [dbo].[Get_Request_Num] @ReqstType = '" + reqstType + "'").ToString();
       }

       public static IList<Model.PortInfo> GetPortInfo(string id)
       {

           string sqlStr = "select p.id,p.portcode,p.area,p.Country,p.postcode,p.zone,u.name as username from tbPort as p,tbUser as u where p.id=" + id + " and p.userid=u.id ";
           return ToolKits.ModelConvertHelper<Model.PortInfo>.ConvertToModel(ToolKits.DbHelperSQL.Query(sqlStr).Tables[0]);



       }


       /// <summary>
       /// list sku for select2
       /// </summary>
       /// <param name="sku"></param>
       /// <returns></returns>
       public static IList<Model.clsDefine.selectOption> List_SKU(string sku,int userId)
       {
           string sqlStr = "select distinct SKU as id,SKU as text from  [dbo].[tbSKU] where CreateId = " + userId + " and SKU like '%" + sku + "%' order by id";
           return ToolKits.ModelConvertHelper<Model.clsDefine.selectOption>.ConvertToModel(ToolKits.DbHelperSQL.Query(sqlStr).Tables[0]);
       }


       /// <summary>
       /// list Port for select2
       /// </summary>
       /// <param name="sku"></param>
       /// <returns></returns>
       public static IList<Model.clsDefine.selectOption> List_Port(string Port)
       {
           string sqlStr = "select distinct portCode as id,portCode as text from  [dbo].[tbPort] where portCode like '%" + Port + "%' order by id";
           return ToolKits.ModelConvertHelper<Model.clsDefine.selectOption>.ConvertToModel(ToolKits.DbHelperSQL.Query(sqlStr).Tables[0]);
       }

       /// <summary>
       /// list Currency
       /// </summary>
       /// <param name="sku"></param>
       /// <returns></returns>
       public static IList<Model.clsDefine.selectOption> List_Currency(string cyCat)
       {
           string sqlStr = "select Descr as id,Descr as text from  [dbo].[tbCurrency] ";

           if (!string.IsNullOrEmpty(cyCat))
           {
               sqlStr += " where Descr like '%" + cyCat + "%'";
           }

           return ToolKits.ModelConvertHelper<Model.clsDefine.selectOption>.ConvertToModel(ToolKits.DbHelperSQL.Query(sqlStr).Tables[0]);
       }

       /// <summary>
       /// list Supplier for select2
       /// </summary>
       /// <param name="sku"></param>
       /// <returns></returns>
       public static IList<Model.clsDefine.selectOption> List_Supplier(string name)
       {
           string sqlStr = "select distinct Id as id,ChsName as text from  [dbo].[tbSupplier] where ChsName like '%" + name + "%' order by id";
           return ToolKits.ModelConvertHelper<Model.clsDefine.selectOption>.ConvertToModel(ToolKits.DbHelperSQL.Query(sqlStr).Tables[0]);
       }

       public static Model.tbSupplier Get_by_Id(int id)
       {
           using(var _db =new Model.quyouEntities())
           {
               return _db.tbSuppliers.Where(p => p.Id == id).FirstOrDefault();
           }
       }



       /// <summary>
       /// 根据PO号查找供应商信息
       /// </summary>
       /// <param name="sku"></param>
       /// <returns></returns>
       public static Model.clsDefine.selectOption Get_Supplier_by_PNbr(string pnbr)
       {
           string sqlStr = "select distinct a.Id as id,a.ChsName as text from  [dbo].[tbSupplier] a, [dbo].[tbMain] b where b.PNbr = '" + pnbr + "' and b.SupplierId = a.Id";
           return ToolKits.ModelConvertHelper<Model.clsDefine.selectOption>.ConvertToEntity(ToolKits.DbHelperSQL.Query(sqlStr).Tables[0]);
       }

       public static Model.tbStatu Get_Status(string name)
       {
           using(var _db = new Model.quyouEntities())
           {
               return _db.tbStatus.Where(p => p.Name.Equals(name)).FirstOrDefault();
           }
       }
  
    }
}
