using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;   //linq 动态查询
using System.Linq.Expressions;
using System.Text;
using System.Data;
using ToolKits;

namespace DAL
{
    public  class SKU
    {
        public KeyValuePair<int, IList<Model.SKUInfo>> List(string whereclause, int pageIndex, int pageSize, string sortName, string sortType, string search)
        {

            UserHelper userHelper = new UserHelper();
            Model.UserInfo user = userHelper.GetUser();

            string searchCon = whereclause==null ? "1=1" : " (sku like '%"+whereclause+"%' or chsname like '%"+whereclause +"%' )";

            string sqlStr = "select id,sku,chsName,imgUrl, Cdt  from tbSKU where createid=" + user.Id + " and " + searchCon;

                //" and "+ whereclause.Equals("")? "1=1: (sku like '%"+whereclause+"%' or chsname like '%"+ whereclause;


            //  return ToolKits.ModelConvertHelper<Model.PortInfo>.ConvertToModel(ToolKits.DbHelperSQL.Query(sqlStr).Tables[0]);

            DataSet ds = ToolKits.DbHelperSQL.Query(sqlStr);
            if (ds.Tables[0].Rows.Count > 0)
            {
                var rst = ToolKits.ModelConvertHelper<Model.SKUInfo>.ConvertToModel(ds.Tables[0]);
                //if (!string.IsNullOrEmpty(search))
                //{
                //    rst = rst.Where(p => p.Title.Contains(search) || p.MbrName.Contains(search)).ToList();
                //}
                int rowcount = rst.Count();

                if (!string.IsNullOrEmpty(sortName))
                {
                    var orderExpression = string.Format("{0} {1}", sortName, sortType); //sortName排序的名称 sortType排序类型 （desc asc）
                    rst = rst.OrderBy(orderExpression).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                    return new KeyValuePair<int, IList<Model.SKUInfo>>(rowcount, rst);
                }
                rst = rst.OrderByDescending(o => o.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return new KeyValuePair<int, IList<Model.SKUInfo>>(rowcount, rst);

            }
            return new KeyValuePair<int, IList<Model.SKUInfo>>(0, null);
        }


        public int Update(Model.SKUInfo sku)
        {

            UserHelper userHelper = new UserHelper();
            Model.UserInfo user = userHelper.GetUser();
            string sqlStr = "";
            int count = 0;
            if (sku.Id == 0)
            {
                sqlStr = "select count(*) from tbSKU where createId=" + user.Id + " and sku='" + sku.sku + "'";
                DataSet ds = ToolKits.DbHelperSQL.Query(sqlStr);
                if (ds.Tables[0].Rows.Count > 0 && (!ds.Tables[0].Rows[0].ItemArray[0].ToString().Equals("0")))
                {
                    return 2;
                }

                sqlStr = "insert into tbSKU(sku,chsname,imgurl,createid) values( '" + sku.sku + "','" + sku.chsName + "','" + sku.imgUrl + "'," + user.Id + ")";

            }
            else
            {
               

                sqlStr = "update tbSKU set chsname='"+ sku.chsName+"' , imgUrl='"+sku.imgUrl+"' where id="+sku.Id;
               // sqlStr = "update tbSupplier set EngName='" + supplier.EngName + "' , ChsName='" + supplier.ChsName + "' , Contact='" + supplier.Contact + "' , QQ='" + supplier.QQ + "' , Email='" + supplier.Email + "' ,Rcpt_Bank_Nbr='" + supplier.Rcpt_Bank_Nbr + "', Phone='" + supplier.Phone + "'  where id=" + supplier.Id;

            }


             count = ToolKits.DbHelperSQL.ExecuteSql(sqlStr);

             return count;

        }

        public IList<Model.SKUInfo> GetSKUInfo(string id)
        {

            string sqlStr = "select id,sku, chsName,imgUrl from tbSKU   where id=" + id;
            return ToolKits.ModelConvertHelper<Model.SKUInfo>.ConvertToModel(ToolKits.DbHelperSQL.Query(sqlStr).Tables[0]);

        }

        public void Delete(int id)
        {
            string sqlStr = "";

            sqlStr = "delete from tbSKU where id=" + id;

            ToolKits.DbHelperSQL.ExecuteSql(sqlStr);
        }
    }
}
