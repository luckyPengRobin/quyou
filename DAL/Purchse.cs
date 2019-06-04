using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
namespace DAL
{
   public class Purchse
    {
       /// <summary>
       /// 运营操作事件
       /// </summary>
       /// <param name="obj"></param>
       /// <param name="actType"></param>
       /// <param name="notes"></param>
       /// <returns></returns>
       public bool act_yy (Model.tbPO obj,int BPId,string actType,string notes)
       {
           try
           {
                using (var _db = new Model.quyouEntities())
                {
                    _db.sp_taskAction(obj.PNbr, BPId, obj.ModUserId, actType, notes);
                }
               return true;
           }
           catch(Exception ex)
           {
               return false;
           }
       }


       public static IList<Model.PortInfo> GetPortInfo(string id)
       {
           return DAL.DBHelper.GetPortInfo(id);
       }
       public Model.clsDefine.retutnMsg task_Act(Model.tbPO obj, int BPId, string actType, string notes)
       {
           Model.clsDefine.retutnMsg rst = new Model.clsDefine.retutnMsg { state = true, msg = "操作成功" };
           try
           {
               using (var _db = new Model.quyouEntities())
               {
                   _db.sp_taskAction(obj.PNbr, BPId, obj.ModUserId, actType, notes);
               }
               
           }
           catch (Exception ex)
           {
               rst.state = false;
               rst.msg = ex.Message;
           }

           return rst;

       } 




       /// <summary>
       /// 检查采购单号是否存在
       /// </summary>
       /// <param name="pnbr"></param>
       /// <returns></returns>
       public bool Is_exist_PO(string pnbr)
       {
           using(var _db = new Model.quyouEntities())
           {
               return _db.tbMains.Where(p => p.PNbr.Equals(pnbr)).Any();
           }
       }
       /// <summary>
       /// 删除指定采购单下的一条数据
       /// </summary>
       /// <param name="obj"></param>
       public void delete_po(Model.tbMain obj,int userId)
       {
           using (var _db = new Model.quyouEntities())
           {
               _db.sp_proc_del_PO(Model.clsDefine.TaskCategory.Del, obj.PNbr, obj.FBA, obj.Sku,userId);
           }
       }

       public Model.TskForDisplay Get(string pnbr,int BPId)
       {
           DataSet ds = ToolKits.DbHelperSQL.Query("exec Get_POForDisplay @pnbr= '" + pnbr + "',@BPId=" + BPId + "");
           if(ds!=null && ds.Tables[0].Rows.Count>0)
           {
               return ToolKits.ModelConvertHelper<Model.TskForDisplay>.ConvertToEntity(ds.Tables[0]);
           }
           return new Model.TskForDisplay { PNbr = pnbr };

       }
       /// <summary>
       /// 显示采购单基本信息
       /// </summary>
       /// <param name="pnbr"></param>
       /// <returns></returns>
       public Model.IsTskInLog Get_by_PNbr(string pnbr,int userId)
       {
           DataSet ds = ToolKits.DbHelperSQL.Query("exec sp_isTsk_in_log @pnbr= '" + pnbr + "',@userId=" + userId + "");
           if (ds != null && ds.Tables[0].Rows.Count > 0)
           {
               return ToolKits.ModelConvertHelper<Model.IsTskInLog>.ConvertToEntity(ds.Tables[0]);
           }
           return new Model.IsTskInLog { PNbr = pnbr };

       }

       /// <summary>
       /// 保存采购单-lis
       /// </summary>
       /// <param name="lstobj"></param>
       /// <returns></returns>
       public string Add_tbMain_list(IList<Model.tbMain> lstobj)
       {
           var rst = string.Empty;
           foreach (var obj in lstobj)
           {
               rst = Add_tbMain(obj);
               if (!string.IsNullOrEmpty(rst))
               {
                   break;
               }
           }
           return rst;
       }


       /// <summary>
       /// 保存采购单
       /// </summary>
       /// <param name="obj"></param>
       public string Add_tbMain(Model.tbMain obj)
       {
           try
           {
               using (var _db = new Model.quyouEntities())
               {
                   bool exists = _db.tbMains.Where(p => p.Sku.Equals(obj.Sku) && p.PNbr.Equals(obj.PNbr) && p.FBA.Equals(obj.FBA)).Any();
                   if (exists)
                   {
                       obj.Id = _db.tbMains.Where(p => p.Sku.Equals(obj.Sku) && p.PNbr.Equals(obj.PNbr) && p.FBA.Equals(obj.FBA)).Select(p => p.Id).FirstOrDefault();
                       obj.Udt = System.DateTime.Now;
                       _db.Entry<Model.tbMain>(obj).State = System.Data.Entity.EntityState.Modified;
                       _db.Entry(obj).Property(m => m.Cdt).IsModified = false;
                       _db.Entry(obj).Property(m => m.FinalQty).IsModified = false;
                       _db.Entry(obj).Property(m => m.LSupplierId).IsModified = false;
                       _db.SaveChanges();                    
                   }
                   else
                   {
                       
                       obj.Cdt = System.DateTime.Now;
                       _db.tbMains.Add(obj);
                      _db.SaveChanges(); 

                   }

               }
               return string.Empty;
           }
           catch (Exception ex)
           {
               return ex.Message;
           }


       }


       /// <summary>
       /// get PO
       /// </summary>
       /// <param name="pnbr"></param>
       /// <returns></returns>
       public Model.tbPO Get_PO(string pnbr)
       {
           using(var _db = new Model.quyouEntities())
           {
               return _db.tbPOes.Where(p => p.PNbr.Equals(pnbr)).FirstOrDefault();
           }
       }
       /// <summary>
       /// get PO
       /// </summary>
       /// <param name="pnbr"></param>
       /// <returns></returns>
       public Model.tbPO Get_PO_By_Id(int Id)
       {
           using (var _db = new Model.quyouEntities())
           {
               return _db.tbPOes.Where(p => p.Id.Equals(Id)).FirstOrDefault();
           }
       }


       /// <summary>
       /// 获取指定采购单号的详细信息
       /// </summary>
       /// <param name="PNbr"></param>
       /// <returns></returns>
       public IList<Model.sp_proc_Read_PO_Result> List_PO(string PNbr)
       {
           //using (var _db = new Model.quyouEntities())
           //{
           //    var rst = _db.sp_proc_Read_PO(PNbr);
           //    return rst.ToList();
           //} 

           string strSql = "exec dbo.sp_proc_Read_PO @pnbr = '" + PNbr + "'";

           DataSet ds = ToolKits.DbHelperSQL.Query(strSql);
           if(ds != null && ds.Tables[0].Rows.Count>0)
           {
               var rst = ToolKits.ModelConvertHelper<Model.sp_proc_Read_PO_Result>.ConvertToModel(ds.Tables[0]);
               return rst;

           }
           return new List<Model.sp_proc_Read_PO_Result>();

       }

       public void update_FinalQty(int Id,string qty)
       {
           if (!string.IsNullOrEmpty(qty))
           {
               string sqlStr = "update [dbo].[tbMain] set [FinalQty] = " + qty + " where id =" + Id + "";
               ToolKits.DbHelperSQL.ExecuteSql(sqlStr);
           }



       }

       /// <summary>
       /// 获取一个采购单的总金额
       /// </summary>
       /// <param name="pnbr"></param>
       /// <returns></returns>
       public string Get_PO_Amount(string pnbr)
       {
           using (var _db = new Model.quyouEntities())
           {
               var st = _db.tbMains.Where(p => p.PNbr.Equals(pnbr)).Sum(p => p.FinalQty * p.UnitPrice).Value;
               return Math.Round(st, 2, MidpointRounding.AwayFromZero).ToString();
           }
       }




       /// <summary>
       /// 获取指定采购单号的详细信息
       /// </summary>
       /// <param name="PNbr"></param>
       /// <returns></returns>
       public IList<Model.tbMain> List_tbMain(string PNbr)
       {
           using (var _db = new Model.quyouEntities())
           {
               return _db.tbMains.Where(w => w.PNbr.Equals(PNbr)).OrderBy(p => p.Id).ToList();
           }
       }




       /// <summary>
       /// 更新货运公司信息
       /// </summary>
       /// <param name="mainId"></param>
       /// <param name="supplierId"></param>
       public void update_LgstcSupplier(int mainId,int supplierId)
       {
           string sqlstr = "update [dbo].[tbMain] set [LSupplierId] = " + supplierId + " where [Id] = " + mainId + "";
           ToolKits.DbHelperSQL.ExecuteSql(sqlstr);
       }
       /// <summary>
       /// 根據BPID和TSK信息获取历史操作记录
       /// </summary>
       /// <param name="PNbr"></param>
       /// <returns></returns>
       public IList<Model.TskForDisplay> Get_Log_By_tsk(int BPId, string tsk)
       {

           string strSql = "exec [dbo].[sp_get_log_by_tsk]@BPId =" + BPId + ",@tsk ='" + tsk + "'";

           DataSet ds = ToolKits.DbHelperSQL.Query(strSql);
           if (ds != null && ds.Tables[0].Rows.Count > 0)
           {
               var rst = ToolKits.ModelConvertHelper<Model.TskForDisplay>.ConvertToModel(ds.Tables[0]);
               return rst;

           }
           return new List<Model.TskForDisplay>();

       }


       public IList<Model.TskForDisplay> list_tasks(int userId, string tskName, int pageIndex, int pageSize)
       {
           string strSql = "exec [dbo].[sp_proc_List_tsks_by_tskStatus] @userId=" + userId + ",@tskStatus ='" + tskName + "',@pageIndex=" + pageIndex + ",@PageSize=" + pageSize + "";

           DataSet ds = ToolKits.DbHelperSQL.Query(strSql);
           if (ds != null && ds.Tables.Count >0 && ds.Tables[0].Rows.Count > 0)
           {
               IList<Model.TskForDisplay> mytasks = ToolKits.ModelConvertHelper<Model.TskForDisplay>.ConvertToModel(ds.Tables[0]);
               return mytasks;
           }
           return new List<Model.TskForDisplay>();


           //SqlParameter[] prmrs = {
           //         new SqlParameter("@userId",SqlDbType.Int),
           //         new SqlParameter("@tskName",SqlDbType.VarChar,50),					
           //         new SqlParameter("@pageIndex",SqlDbType.Int),
           //         new SqlParameter("@PageSize", SqlDbType.Int)};

           //prmrs[0].Value = userId;
           //prmrs[1].Value = tskName;
           //prmrs[2].Value = pageIndex;
           //prmrs[3].Value = pageSize;


           //var rst = ToolKits.DbHelperSQL.RunProcedure("quyou.[dbo].[sp_proc_List_tsks] ", prmrs, "tb");

         


       }

       public IList<Model.TskForDisplay> SearchList(string pnbr,string lnbr,string status,object userId, int pageIndex, int pageSize)
       {
           string strSql = "exec [dbo].[List_Super_Search] @userId ='" + userId + "',@pnbr  = '" + pnbr + "',@lnbr  = '" + lnbr + "',@state ='" + status + "',@pageIndex ='" + pageIndex + "',@PageSize ='" + pageSize + "'";
           //string strSql = "exec [dbo].[List_Search] @userId ='" + userId + "',@pnbr  = '" + pnbr + "',@lnbr  = '" + lnbr + "',@state ='" + status + "',@pageIndex ='" + pageIndex + "',@PageSize ='" + pageSize + "'";

           DataSet ds = ToolKits.DbHelperSQL.Query(strSql);
           if (ds != null && ds.Tables[0].Rows.Count > 0)
           {
               IList<Model.TskForDisplay> mytasks = ToolKits.ModelConvertHelper<Model.TskForDisplay>.ConvertToModel(ds.Tables[0]);
               return mytasks;
           }
           return new List<Model.TskForDisplay>();

       }

       /// <summary>
       /// 删除指定采购单
       /// </summary>
       /// <param name="pnbr"></param>
       /// <param name="userId"></param>
       public void delPO(string pnbr, int userId)
       {
           string strSql = "exec [dbo].[sp_proc_deletePO] @pnbr='" + pnbr + "',@userId=" + userId + "";
           ToolKits.DbHelperSQL.ExecuteSql(strSql);
       }


       /// <summary>
       /// 新增采购单 --old
       /// </summary>
       /// <param name="tb"></param>
       /// <param name="PNbr"></param>
       /// <param name="ownerId"></param>
       /// <param name="actType"></param>
       /// <returns></returns>
       public string Add_PO(DataTable tb,string PNbr,string actType)
       {
           var rst = string.Empty;
          
           foreach (DataRow r in tb.Rows)
           {
               Model.tbMain mod = new Model.tbMain();
               mod.PNbr = PNbr;
               mod.Sku = r["SKU"].ToString();
               mod.Qty = Convert.ToInt32(r["Quantity"].ToString());
               mod.UnitPrice = Convert.ToDecimal(r["UnitPrice"].ToString());
               mod.Currency = r["Currency"].ToString();
               mod.OuterBoxDim = Convert.ToDecimal(r["OutBoxDim"].ToString());
               mod.OuterBoxQty = Convert.ToInt32(r["OutBoxQTY"].ToString());
               mod.Weight = Convert.ToDecimal(r["Weight"].ToString());
               mod.DestPort = r["DestPort"].ToString();
               mod.Comments = r["Comments"].ToString();



               SqlParameter[] parameters = {
                    new SqlParameter("@actType",actType),
					new SqlParameter("@pnbr",mod.PNbr),					
					new SqlParameter("@sku",mod.Sku),
					new SqlParameter("@qty", mod.Qty),
					new SqlParameter("@unitPrice", mod.UnitPrice),
                    new SqlParameter("@OboxDim", mod.OuterBoxDim),
					new SqlParameter("@OboxQty", mod.OuterBoxQty),
					new SqlParameter("@weight", mod.Weight),
					new SqlParameter("@destPort", mod.DestPort),
					new SqlParameter("@comns", mod.Comments)};

               ToolKits.DbHelperSQL.ExecuteSql("exec [dbo].[sp_AddPO] ", parameters);

           }


           


           return rst;
       }

    }
}
