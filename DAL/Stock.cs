﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections;



namespace DAL
{
   public class Stock
    {


       public IList<Model.clsDefine.selectOption> list_Users(string name)
       {
           string strSql ="";
           if (!name.Trim().Equals(""))
           {
               strSql = "select Id as id,name as text from [dbo].[tbUser] where name like '%" + name + "%'";

           }
           else {
               strSql = "select Id as id,name as text from [dbo].[tbUser]";
           }
         

           return ToolKits.ModelConvertHelper<Model.clsDefine.selectOption>.ConvertToModel(ToolKits.DbHelperSQL.Query(strSql).Tables[0]);
       }


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

       public IList<Model.TskForDisplay> SearchList(string pnbr,string lnbr,string status,int userId, int pageIndex, int pageSize)
       {
           string strSql = "exec [dbo].[List_Search] @userId ='" + userId + "',@pnbr  = '" + pnbr + "',@lnbr  = '" + lnbr + "',@state ='" + status + "',@pageIndex ='" + pageIndex + "',@PageSize ='" + pageSize + "'";

           DataSet ds = ToolKits.DbHelperSQL.Query(strSql);
           if (ds != null && ds.Tables[0].Rows.Count > 0)
           {
               IList<Model.TskForDisplay> mytasks = ToolKits.ModelConvertHelper<Model.TskForDisplay>.ConvertToModel(ds.Tables[0]);
               return mytasks;
           }
           return new List<Model.TskForDisplay>();

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


       public List<Model.ProfitInfo> calculateInterest(DataTable dt, string rate, int userId)
       {

           DataTable newDt = dt.Copy();
           newDt.Clear();
           ArrayList skuLst = new ArrayList();


           for (int i = 0; i < dt.Rows.Count; i++) { 
           
               if(dt.Rows[i]["交易类型"].ToString().Equals("订单付款") &&( dt.Rows[i]["付款类型"].ToString().Equals("亚马逊所收费用") || dt.Rows[i]["付款类型"].ToString().Equals("促销返点") ||dt.Rows[i]["付款类型"].ToString().Equals("商品价格") || dt.Rows[i]["付款类型"].ToString().Equals("其他")   )){

                   string sku = dt.Rows[i]["SKU"].ToString();                   

                   if (!skuLst.Contains(sku))
                   {

                       skuLst.Add(sku);
                   }

                   DataRow dr = dt.Rows[i];

                   newDt.Rows.Add(dr.ItemArray);

               
               }

           
           }


           List<Model.ProfitInfo> profitLst = new List<Model.ProfitInfo>();

           foreach (string sku in skuLst) {

               Model.ProfitInfo profit = new Model.ProfitInfo();
               profit.sku = sku;

               var priceLst = newDt.AsEnumerable().Where(p => p.Field<string>("SKU").Equals(sku) && p.Field<string>("付款类型").Equals("商品价格"));

               int num = 0;

               decimal totalPrice = 0;
               
               foreach (var item in priceLst) {

                   string strPrice = item.Field<string>("金额").ToString();

                   decimal price =Math.Round( Convert.ToDecimal(strPrice),2);

                   string strNum = item.Field<string>("数量") == null ? "1" : item.Field<string>("数量").ToString();

                   int numTmp = strNum.Trim().Equals("")?1:  Convert.ToInt32(strNum);

                   decimal totalPriceTmp = numTmp * price;

                   
                   totalPrice = totalPrice + totalPriceTmp;

                   num = num + numTmp;
               
               }
               profit.price = Math.Round(totalPrice,2);

               profit.number = num;

               var amazonLst = newDt.AsEnumerable().Where(p => p.Field<string>("SKU").Equals(sku) && p.Field<string>("付款类型").Equals("亚马逊所收费用"));

               decimal amazonTotal = 0;

               foreach (var item in amazonLst)
               {

                   string strPrice = item.Field<string>("金额").ToString();

                   decimal price = Math.Round(Convert.ToDecimal(strPrice), 2);

                   string strNum = item.Field<string>("数量") == null ? "1" : item.Field<string>("数量").ToString();

                   int numTmp = strNum.Trim().Equals("") ? 1 : Convert.ToInt32(strNum);

                   decimal totalPriceTmp = numTmp * price;

                   amazonTotal = amazonTotal + totalPriceTmp;                  

               }

               profit.amazonCost =Math.Round( amazonTotal,2);


               var rebateLst = newDt.AsEnumerable().Where(p => p.Field<string>("SKU").Equals(sku) && p.Field<string>("付款类型").Equals("促销返点"));

               decimal rebateTotal = 0;

               foreach (var item in rebateLst)
               {

                   string strPrice = item.Field<string>("金额").ToString();

                   decimal price = Math.Round(Convert.ToDecimal(strPrice), 2);

                   string strNum =  item.Field<string>("数量") == null ? "1" : item.Field<string>("数量").ToString();

                   int numTmp = strNum.Trim().Equals("") ? 1 : Convert.ToInt32(strNum);

                   decimal totalPriceTmp = numTmp * price;

                   rebateTotal = rebateTotal + totalPriceTmp;

               }

               profit.proRebate =Math.Round( rebateTotal,2);

               var otherLst = newDt.AsEnumerable().Where(p => p.Field<string>("SKU").Equals(sku) && p.Field<string>("付款类型").Equals("其他"));

               decimal otherTotal = 0;

               foreach (var item in otherLst)
               {

                   string strPrice = item.Field<string>("金额").ToString();

                   decimal price = Math.Round(Convert.ToDecimal(strPrice),2);

                   string strNum = item.Field<string>("数量") == null ? "1" : item.Field<string>("数量").ToString();

                   int numTmp = strNum.Trim().Equals("") ? 1 : Convert.ToInt32(strNum);

                   decimal totalPriceTmp = numTmp * price;

                   otherTotal = otherTotal + totalPriceTmp;

               }

               profit.other = Math.Round( otherTotal,2);


               profit.grandTotal = Math.Round( profit.price + profit.proRebate + profit.other + profit.amazonCost,2);

               profit.rmbTotal =Math.Round(profit.grandTotal * Convert.ToDecimal(rate),2);

               profitLst.Add(profit);
           
           }


          // return ToolKits.ModelConvertHelper<Model.clsDefine.selectOption>.ConvertToModel(ToolKits.DbHelperSQL.Query(strSql).Tables[0]);
    

           foreach (Model.ProfitInfo profit in profitLst) {

               string sku = profit.sku;
               int num = profit.number;

               string sql = "select id, PNbr,FBA,SKU,CurrQty,unitCost from vi_tbStock where moduserid="+userId +" and currQty>0 and sku='"+ sku+"' order by cdt asc ";

               DataSet ds=  ToolKits.DbHelperSQL.Query(sql);

               decimal TotalunitCost = 0;

                
               if (ds.Tables[0].Rows.Count > 0)
                 {
                     DataTable tb1 = ds.Tables[0];

                     for (int j = 0; j < tb1.Rows.Count; j++) {

                         int stockQty = Convert.ToInt32( tb1.Rows[j]["CurrQty"].ToString());
                         int id=Convert.ToInt32( tb1.Rows[j]["id"].ToString());
                         decimal unitCostTmp = Convert.ToDecimal(tb1.Rows[j]["unitCost"].ToString());
                         if (stockQty >= num)
                         {

                             stockQty = stockQty - num;
                             TotalunitCost = TotalunitCost + (num * unitCostTmp);
                             num = 0;

                         }
                         else {
                             num =num- stockQty;
                             TotalunitCost = TotalunitCost + (stockQty * unitCostTmp);
                             stockQty = 0;
                           
                           
                         }
                         sql = "update vi_tbStock set currqty=" + stockQty + " where id=" + id;

                         ToolKits.DbHelperSQL.ExecuteSql(sql);

                         if (num == 0) {

                             break;
                         }
                     
                     }

                   
                 }


               profit.totalCost =Math.Round( TotalunitCost,2);

               profit.profit = Math.Round( profit.rmbTotal - profit.totalCost,2);

               DateTime dateTime = DateTime.Now;

               String  strDate= DateTime.Parse( dateTime.ToString("yy-MM-dd")).AddMonths(-1).ToShortDateString();
            

               //int month = dateTime.Month.ToString().Equals("1") ? 12 : (Convert.ToInt32(dateTime.Month.ToString())-1);

               profit.profitMonth = dateTime;
               sql = "insert into tbprofit(sku,prorebate,other,price,amazonCost,grandTotal,rmbTotal,number,totalCost,profit,profitMonth,userid) values('" + sku + "'," + profit.proRebate + "," + profit.other + "," + profit.price + "," + profit.amazonCost + "," + profit.grandTotal + "," + profit.rmbTotal + "," + profit.number + "," + profit.totalCost + "," + profit.profit + ",'" + strDate + "'," + userId + ")";


               ToolKits.DbHelperSQL.ExecuteSql(sql);
           
           }


           //insert data to profit 






           return profitLst;


               
       }

       public KeyValuePair<int, IList<Model.ProfitInfo>> getProfit(int userId,int pageIndex,int pageSize,string startDate,string endDate)
       {

           string sql = "";
           string where = " and 1=1 ";
           if (startDate.Equals("") && endDate.Equals(""))
           {
               DateTime dateTime = DateTime.Now;
               dateTime = DateTime.Parse(dateTime.ToString("yy-MM-dd")).AddMonths(-1);

               int year = Convert.ToInt32(dateTime.Year.ToString());

               int month = Convert.ToInt32(dateTime.Month.ToString());

               sql = "select id,sku,prorebate,other,price,amazonCost,profitMonth,grandTotal,number,totalCost,profit,rmbTotal from tbProfit where userid=" + userId + " and year(profitMonth)= " + year + " and Month(profitMonth)=" + month;


           }
           else {

               if (!startDate.Equals(""))
               {
                   string[] date = startDate.Split('-');
                   int year = Convert.ToInt32(date[0]);
                   int month = Convert.ToInt32(date[1]);
                   where = where + " and year(profitMonth)>=" + year + " and month(profitMonth)>=" + month;

               }
               if (!endDate.Equals(""))
               {
                   string[] date = endDate.Split('-');
                   int year = Convert.ToInt32(date[0]);
                   int month = Convert.ToInt32(date[1]);
                   where = where + " and year(profitMonth)<=" + year + " and month(profitMonth)<=" + month;

               }
               sql = "select id,sku,prorebate,other,price,amazonCost,profitMonth,grandTotal,number,totalCost,profit,rmbTotal from tbProfit where userid=" + userId;

               sql = sql + where;
           }


          
           DataSet ds= ToolKits.DbHelperSQL.Query(sql);
           if (ds.Tables[0].Rows.Count > 0)
           {
               DataTable tb1 = ds.Tables[0];

               var rst = ToolKits.ModelConvertHelper<Model.ProfitInfo>.ConvertToModel(ds.Tables[0]);

               int rowcount = rst.Count();

               
               rst = rst.OrderByDescending(o => o.id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
               return new KeyValuePair<int, IList<Model.ProfitInfo>>(rowcount, rst);

           //    List<Model.ProfitInfo> profitLst = rst.ToList();

             
           }
           return new KeyValuePair<int, IList<Model.ProfitInfo>>(0, null);

       



       }

       public KeyValuePair<int, IList<Model.ProfitInfo>> getAllProfit(int pageIndex, int pageSize, string startDate,string endDate, int userid)
       {

         

           string where=userid==0?" and 1=1 ":" and userid="+userid;
           
           if(!startDate.Equals(""))
           {
                  string[] date = startDate.Split('-');
                  int year=Convert.ToInt32(date[0]);
                  int month=Convert.ToInt32( date[1]);
                  where = where + " and year(profitMonth)>=" + year + " and month(profitMonth)>=" + month;

           }
           if (!endDate.Equals(""))
           {
               string[] date = endDate.Split('-');
               int year = Convert.ToInt32(date[0]);
               int month = Convert.ToInt32(date[1]);
               where = where + " and year(profitMonth)<=" + year + " and month(profitMonth)<=" + month;

           }

           string sql = "select p.id,p.sku,p.prorebate,p.other,p.price,p.profitMonth,p.amazonCost,p.grandTotal,p.number,p.totalCost,p.profit,p.rmbTotal,u.name as username from tbprofit as p,tbUser as u where p.userid=u.id " + where;



           DataSet ds = ToolKits.DbHelperSQL.Query(sql);
           if (ds.Tables[0].Rows.Count > 0)
           {
               DataTable tb1 = ds.Tables[0];

               var rst = ToolKits.ModelConvertHelper<Model.ProfitInfo>.ConvertToModel(ds.Tables[0]);

               int rowcount = rst.Count();


               rst = rst.OrderByDescending(o => o.id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
               return new KeyValuePair<int, IList<Model.ProfitInfo>>(rowcount, rst);

               //    List<Model.ProfitInfo> profitLst = rst.ToList();


           }
           return new KeyValuePair<int, IList<Model.ProfitInfo>>(0, null);



       }

       public bool checkData(int userid)
       {

          

           DateTime dateTime = DateTime.Now;
           dateTime = DateTime.Parse(dateTime.ToString("yy-MM-dd")).AddMonths(-1);

           int year = Convert.ToInt32(dateTime.Year.ToString());

           int month = Convert.ToInt32(dateTime.Month.ToString());

           string sql = "select count(*) from tbProfit where userid=" + userid + " and year(profitMonth)= " + year + " and Month(profitMonth)=" + month;

          DataSet ds= ToolKits.DbHelperSQL.Query(sql);

          if (ds.Tables[0].Rows.Count > 0)
          {
              DataTable tb1 = ds.Tables[0];

             if( tb1.Rows[0][0].ToString().Equals("0")){

                 return false;
             }else{
             
                 return true;
             }
          }

          return true;

       }

       public void Update(int id, double profit)
       {
           string sql = "update tbprofit set profit=" + profit + " where id="+id;

           ToolKits.DbHelperSQL.ExecuteSql(sql);

       }

       public KeyValuePair<int, IList<Model.StockInfo>> getAllStock(int pageIndex, int pageSize, int userId)
       {

           string where = " and " +( userId == 0 ? " 1=1 " :" moduserid=" +userId);

           string sql = "select s.sku,p.moduserid,sum(s.currqty) as currqty from tbStock as s,tbPO as p  where   1=1 and s.pnbr=p.pnbr  "+where +" group by p.moduserid,s.sku";

           DataSet ds = ToolKits.DbHelperSQL.Query(sql);

         
            if (ds.Tables[0].Rows.Count > 0)
            {
               // DataTable tb1 = ds.Tables[0];
                var rst = ToolKits.ModelConvertHelper<Model.StockInfo>.ConvertToModel(ds.Tables[0]);

                int rowcount = rst.Count();

                rst = rst.OrderByDescending(o => o.modUserid).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return new KeyValuePair<int, IList<Model.StockInfo>>(rowcount, rst);

              
                //for (int i = 0; i < tb1.Rows.Count; i++) { 
                

                
                //}


                //if (tb1.Rows[0][0].ToString().Equals("0")) { }
            }


            return new KeyValuePair<int, IList<Model.StockInfo>>(0, null);

       }

       public Dictionary<string, string> GetUserInfo()
       {

           string sql = "select id ,name from tbuser ";
           Dictionary<string, string> dic = new Dictionary<string, string>();
           DataSet ds = ToolKits.DbHelperSQL.Query(sql);

           if (ds.Tables[0].Rows.Count > 0)
            {
               DataTable tb1 = ds.Tables[0];

                dic = tb1.Rows.Cast<DataRow>().ToDictionary(x => x[0].ToString(), x => x[1].ToString());

               

               return dic;

               

           }

           return dic;


       }

       public int Update(int userid, int stock, string sku)
       {

          // string sql = "update tbstock set currqty=currqty+" + stock + "where (select top 1 * from tbStock where moduserid=" + userid + " and sku='"+ sku+ "' and currqty<>0 order by cdt asc  ) ";



           bool flag = stock >= 0 ? true : false;


           if (stock > 0)
           {//入库，加回去


               //判断库存是否超过进货量

               
              


               string sql = "select top 1 id, pnbr,fba,sku,currQty,qty,cdt from vi_tbUpdateStock where moduserid=" + userid + " and sku='" + sku + "' and currqty<>0 order by cdt asc";
               DataSet ds = ToolKits.DbHelperSQL.Query(sql);
               if (ds.Tables[0].Rows.Count > 0)
               {
                   DataTable dt=ds.Tables[0];
                   int id=Convert.ToInt32( dt.Rows[0][0].ToString());
                   int currQty = Convert.ToInt32(dt.Rows[0][4].ToString());
                   int qty = Convert.ToInt32(dt.Rows[0][5]);

                   if ((qty - currQty) >= stock) {

                       int newCurrQty = currQty + stock;

                       string sql1="update vi_tbUpdateStock set currQty="+newCurrQty+" where id="+id;

                       ToolKits.DbHelperSQL.ExecuteSql(sql1);

                       stock = 0;

                   }
                   else
                   {

                     



                       string sql1 = "update vi_tbUpdateStock set currQty=" + qty + " where id=" + id;

                       ToolKits.DbHelperSQL.ExecuteSql(sql1);

                       stock = stock - (qty - currQty);

                       //判断进货量能否满足库存
                       string sq2 = "select sum(qty) from vi_tbUpdateStock where moduserid=" + userid + " and sku='" + sku + "' and currqty=0  ";
                       DataSet ds2 = ToolKits.DbHelperSQL.Query(sq2);

                       if (ds2.Tables[0].Rows.Count > 0)
                         {
                             DataTable dt1 = ds2.Tables[0];

                           int sumQty = Convert.ToInt32(dt1.Rows[0][0].ToString().Equals("")?0:dt1.Rows[0][0]);

                             if (sumQty - stock < 0) {

                                 return stock;
                             }
                         }


                       //----------------------------------

                       string sql2 = "select  id, pnbr,fba,sku,currQty,qty,cdt from vi_tbUpdateStock where moduserid=" + userid + " and sku='" + sku + "' and currqty=0 order by cdt desc";

                       DataSet ds1 = ToolKits.DbHelperSQL.Query(sql2);

                       if (ds1.Tables[0].Rows.Count > 0)
                       { 
                           DataTable dt1=ds1.Tables[0];
                           for (int i = 0; i < dt1.Rows.Count ; i++) {

                               if (stock == 0) {

                                   break;
                               }

                               int id1 = Convert.ToInt32(dt1.Rows[i][0].ToString());
                              /// int currQty1 = Convert.ToInt32(dt.Rows[0][4].ToString());
                               int qty1 = Convert.ToInt32(dt1.Rows[i][5]);

                               if (qty1 >= stock)
                               {
                                   int newCurrQty =  stock;

                                   string sql3 = "update vi_tbUpdateStock set currQty=" + newCurrQty + " where id=" + id1;

                                   ToolKits.DbHelperSQL.ExecuteSql(sql3);

                                   stock = 0;

                               }
                               else {
                                   string sql3 = "update vi_tbUpdateStock set currQty=" + qty1 + " where id=" + id1;

                                   ToolKits.DbHelperSQL.ExecuteSql(sql3);

                                   stock = stock - qty1;

                               }


                           
                           }
                       
                       
                       }
                    

                   }

                 

               }

           }else if (stock<0){//出库

               stock = Math.Abs(stock);
               string sql= "select  id, pnbr,fba,sku,currQty,qty,cdt from vi_tbUpdateStock where moduserid=" + userid + " and sku='" + sku + "' and currqty>0 order by cdt asc";

               DataSet ds = ToolKits.DbHelperSQL.Query(sql);

               if (ds.Tables[0].Rows.Count > 0)
               {
                   DataTable dt = ds.Tables[0];

                   for (int i = 0; i < dt.Rows.Count; i++)
                   {

                       if (stock == 0)
                       {

                           break;
                       }

                       int id1 = Convert.ToInt32(dt.Rows[i][0].ToString());
                       int currQty1 = Convert.ToInt32(dt.Rows[i][4].ToString());
                     //  int qty1 = Convert.ToInt32(dt.Rows[0][5].ToString());

                       if (currQty1 >= stock)
                       {
                           int newCurrQty = currQty1 - stock;

                           string sql3 = "update vi_tbUpdateStock set currQty=" + newCurrQty + " where id=" + id1;

                           ToolKits.DbHelperSQL.ExecuteSql(sql3);

                           stock = 0;

                       }
                       else {

                           stock = stock - currQty1;

                           string sql3 = "update vi_tbUpdateStock set currQty=" + 0 + " where id=" + id1;

                           ToolKits.DbHelperSQL.ExecuteSql(sql3);
                       }
                   }

               }
            
           
           }

           if (!flag) {

               stock = -stock;
           }

           return stock;
       
       }
    }
}
