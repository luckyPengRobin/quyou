using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;   //linq 动态查询
using System.Linq.Expressions;
using System.Text;
using System.Data;
namespace DAL
{
    public class Supplier
    {
        public KeyValuePair<int, IList<Model.SupplierInfo>> List(string whereclause, int pageIndex, int pageSize, string sortName, string sortType, string search)
        {


           int id= (new UserHelper()).GetUser().Id;

            string searchCon = whereclause == null ? "1=1" : " (EngName like '%" + whereclause + "%' or Contact like '%" + whereclause + "%' )";

            string sqlStr = "select id, EngName,ChsName,Contact,QQ, Email, Rcpt_Bank_Nbr,Phone   from tbSupplier where moduserid=" + id +" and "+searchCon;
            //  return ToolKits.ModelConvertHelper<Model.PortInfo>.ConvertToModel(ToolKits.DbHelperSQL.Query(sqlStr).Tables[0]);

            DataSet ds = ToolKits.DbHelperSQL.Query(sqlStr);
            if (ds.Tables[0].Rows.Count > 0)
            {
                var rst = ToolKits.ModelConvertHelper<Model.SupplierInfo>.ConvertToModel(ds.Tables[0]);
                //if (!string.IsNullOrEmpty(search))
                //{
                //    rst = rst.Where(p => p.Title.Contains(search) || p.MbrName.Contains(search)).ToList();
                //}
                int rowcount = rst.Count();

                if (!string.IsNullOrEmpty(sortName))
                {
                    var orderExpression = string.Format("{0} {1}", sortName, sortType); //sortName排序的名称 sortType排序类型 （desc asc）
                    rst = rst.OrderBy(orderExpression).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                    return new KeyValuePair<int, IList<Model.SupplierInfo>>(rowcount, rst);
                }
                rst = rst.OrderByDescending(o => o.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return new KeyValuePair<int, IList<Model.SupplierInfo>>(rowcount, rst);

            }
            return new KeyValuePair<int, IList<Model.SupplierInfo>>(0, null);
        }


        public  IList<Model.SupplierInfo> GetSupplierInfo(string id)
        {
          

            string sqlStr = "select id, EngName,ChsName,Contact,QQ, Email, Rcpt_Bank_Nbr,Phone from tbSupplier   where id=" + id ;
            return ToolKits.ModelConvertHelper<Model.SupplierInfo>.ConvertToModel(ToolKits.DbHelperSQL.Query(sqlStr).Tables[0]);



        }

        //public int Update(Model.PortInfo port)
        //{

        //    Model.UserInfo user = (new UserHelper()).GetUser();
        //    string sqlStr = "";
        //    if (port.Id == 0)
        //    {
        //        sqlStr = "insert into tbPort(portname,chsportname,country,chscountry,userid) values( '" + port.PortName + "','" + port.ChsPortName + "','" + port.Country + "','" + port.ChsCountry + "'," + user.Id + ")";

        //    }
        //    else
        //    {
        //        sqlStr = "update tbPort set PortName='" + port.PortName + "' , chsPortName='" + port.ChsPortName + "' , chsCountry='" + port.ChsCountry + "' , country='" + port.Country + "' , userid=" + user.Id + "  where id=" + port.Id;

        //    }


        //    int count = ToolKits.DbHelperSQL.ExecuteSql(sqlStr);



        //    return count;
        //}

        //public void Delete(int id)
        //{

        //    string sqlStr = "";

        //    sqlStr = "delete from tbport where id=" + id;

        //    ToolKits.DbHelperSQL.ExecuteSql(sqlStr);

        //}

       

        public int Update(Model.SupplierInfo supplier)
        {

            //Model.UserInfo user = (new UserHelper()).GetUser();
            string sqlStr = "";
            if (supplier.Id == 0)
            {
                int id= (new UserHelper()).GetUser().Id;

                sqlStr = "insert into tbSupplier(EngName,ChsName,Contact,QQ, Email, Rcpt_Bank_Nbr,Phone,moduserid) values( '" + supplier.EngName + "','" + supplier.ChsName + "','" + supplier.Contact + "','" + supplier.QQ + "','" + supplier.Email+"','" +supplier.Rcpt_Bank_Nbr+ "','"+supplier.Phone+"',"+id+")";

            }
            else
            {
                sqlStr = "update tbSupplier set EngName='" +supplier.EngName + "' , ChsName='" + supplier.ChsName + "' , Contact='" + supplier.Contact + "' , QQ='" + supplier.QQ + "' , Email='" + supplier.Email + "' ,Rcpt_Bank_Nbr='" + supplier.Rcpt_Bank_Nbr + "', Phone='"+ supplier.Phone+"'  where id=" + supplier.Id;

            }


            int count = ToolKits.DbHelperSQL.ExecuteSql(sqlStr);



            return count;
        }

        public void Delete(int id)
        {

            string sqlStr = "";

            sqlStr = "delete from tbSupplier where id=" + id;

            ToolKits.DbHelperSQL.ExecuteSql(sqlStr);

        }
    }
}
