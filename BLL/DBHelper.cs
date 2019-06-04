using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public class DBHelper
    {
        /// <summary>
        /// Get a new request Number
        /// </summary>
        /// <param name="reqstType"></param>
        /// <returns></returns>
       public static string Get_IndexNbr(string reqstType)
        {
            return DAL.DBHelper.Get_IndexNbr(reqstType);
        }
       /// <summary>
       /// list SKU for select
       /// </summary>
       /// <param name="sku"></param>
       /// <param name="userId"></param>
       /// <returns></returns>
       public static IList<Model.clsDefine.selectOption> List_SKU(string sku, int userId)
        {
            return DAL.DBHelper.List_SKU(sku, userId);
        }
       /// <summary>
       /// list Port for select
       /// </summary>
       /// <param name="sku"></param>
       /// <param name="userId"></param>
       /// <returns></returns>
       public static IList<Model.clsDefine.selectOption> List_Port(string port)
       {
           return DAL.DBHelper.List_Port(port);
       }

        public static IList<Model.clsDefine.selectOption> List_Supplier(string name)
       {
           return DAL.DBHelper.List_Supplier(name);
       }
       /// <summary>
       /// Get supplier by id
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
       public static Model.tbSupplier Get_by_Id(int id)
        {
            return DAL.DBHelper.Get_by_Id(id);
        }

        /// <summary>
       /// list Currency
       /// </summary>
       /// <param name="sku"></param>
       /// <returns></returns>
        public static IList<Model.clsDefine.selectOption> List_Currency(string cyCat)
        {
            return DAL.DBHelper.List_Currency(cyCat);
        }


        /// <summary>
       /// 根据PO号查找供应商信息
       /// </summary>
       /// <param name="sku"></param>
       /// <returns></returns>
        public static Model.clsDefine.selectOption Get_Supplier_by_PNbr(string pnbr)
        {
            return DAL.DBHelper.Get_Supplier_by_PNbr(pnbr);
        }

        public static Model.tbStatu Get_Status(string name)
        {
            return DAL.DBHelper.Get_Status(name);
        }

        public static IList<Model.PortInfo> GetPortInfo(string id)
        {
            return DAL.DBHelper.GetPortInfo(id);
        }


    }
}
