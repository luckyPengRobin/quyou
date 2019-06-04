using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace DAL
{
   public class Logistics
    {
       /// <summary>
        /// check logistics if not exists then add new record for logistics
       /// </summary>
       /// <param name="pnbr"></param>
       public void check_Add(string pnbr,int BPId)
       {
           ToolKits.DbHelperSQL.ExecuteSql("exec [dbo].[sp_proc_add_logistics] @pnbr = '" + pnbr + "',@BPId = " + BPId + "");
       }

       public Model.LgsticForDisplay Get(int BPId,int userId)
       {
           DataSet ds = ToolKits.DbHelperSQL.Query("exec sp_proc_logistics @BPId=" + BPId + ",@userId = " + userId + "");
           if (ds != null && ds.Tables[0].Rows.Count > 0)
           {
               return ToolKits.ModelConvertHelper<Model.LgsticForDisplay>.ConvertToEntity(ds.Tables[0]);
           }
           return new Model.LgsticForDisplay();

       }
       public IList<Model.LgsticForDisplay> List(string pnbr, string islistsku)
       {
           string strSql = "exec [dbo].[list_logistics] @pnbr ='" + pnbr + "',@islistsku = '" + islistsku + "'";
           //string strSql = "exec [dbo].[list_logistics] @pnbr ='" + pnbr + "'";
           DataSet ds = ToolKits.DbHelperSQL.Query(strSql);
           if (ds != null && ds.Tables[0].Rows.Count > 0)
           {
               IList<Model.LgsticForDisplay> lst = ToolKits.ModelConvertHelper<Model.LgsticForDisplay>.ConvertToModel(ds.Tables[0]);
               return lst;

           }
           return new List<Model.LgsticForDisplay>();

       }
       public IList<Model.LgsticForDisplay> List2(int BPId, int userId)
       {
           string strSql = "exec [dbo].[sp_proc_logistics] @BPId ='" + BPId + "',@userId = '" + userId + "'";
           //string strSql = "exec [dbo].[list_logistics] @pnbr ='" + pnbr + "'";
           DataSet ds = ToolKits.DbHelperSQL.Query(strSql);
           if (ds != null && ds.Tables[0].Rows.Count > 0)
           {
               IList<Model.LgsticForDisplay> lst = ToolKits.ModelConvertHelper<Model.LgsticForDisplay>.ConvertToModel(ds.Tables[0]);
               return lst;
           }
           return new List<Model.LgsticForDisplay>();

       }
       public void update(Model.LgsticForDisplay obj,int userId)
       {
           using (var _db = new Model.quyouEntities())
           {
               _db.sp_proc_logistics(Model.clsDefine.TaskCategory.Save, obj.BPId, obj.yundanNbr, obj.carriageChrge.ToString(), obj.traiffChrge.ToString(), obj.InspecChrge.ToString(), obj.confirmComments.ToString(), obj.others.ToString(), obj.Id, userId);
           }
       }







    }
}
