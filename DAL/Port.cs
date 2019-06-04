using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;   //linq 动态查询
using System.Linq.Expressions;
using System.Text;
using System.Data;
namespace DAL
{
    public class Port
    {
        public KeyValuePair<int, IList<Model.PortInfo>> List(string whereclause, int pageIndex, int pageSize, string sortName, string sortType, string search)
        {

            string searchCon = whereclause.Equals("") ? "1=1" : " (p.portcode like '%" + whereclause + "%' or p.country like '%" + whereclause + "%' )";



            string sqlStr = "select p.id,p.portcode,p.area,p.Country,p.postcode,p.zone,u.name as username from tbPort as p,tbUser as u where p.userid=u.id and "+searchCon;
          //  return ToolKits.ModelConvertHelper<Model.PortInfo>.ConvertToModel(ToolKits.DbHelperSQL.Query(sqlStr).Tables[0]);

            DataSet ds = ToolKits.DbHelperSQL.Query(sqlStr);
            if (ds.Tables[0].Rows.Count > 0)
            {
                var rst = ToolKits.ModelConvertHelper<Model.PortInfo>.ConvertToModel(ds.Tables[0]);
                //if (!string.IsNullOrEmpty(search))
                //{
                //    rst = rst.Where(p => p.Title.Contains(search) || p.MbrName.Contains(search)).ToList();
                //}
                int rowcount = rst.Count();

                if (!string.IsNullOrEmpty(sortName))
                {
                    var orderExpression = string.Format("{0} {1}", sortName, sortType); //sortName排序的名称 sortType排序类型 （desc asc）
                    rst = rst.OrderBy(orderExpression).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    
                    return new KeyValuePair<int, IList<Model.PortInfo>>(rowcount, rst);
                }
                rst = rst.OrderByDescending(o => o.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return new KeyValuePair<int, IList<Model.PortInfo>>(rowcount, rst);

            }
            return new KeyValuePair<int, IList<Model.PortInfo>>(0, null);
        }

        public int Update(Model.PortInfo port)
        {

                 Model.UserInfo user = (new UserHelper()).GetUser();
                 string sqlStr = "";
                 if (port.Id == 0)
                 {
                     sqlStr = "insert into tbPort(portcode,area,country,postcode,zone,userid) values( '" + port.PortCode + "','" + port.area + "','" + port.Country + "','" + port.PostCode + "','" + port.zone + "'," + user.Id + ")";
            
                 }
                 else {

                      sqlStr = "update tbPort set portcode='" + port.PortCode + "' , area='" + port.area + "' , postcode='" + port.PostCode + "' , country='" + port.Country + "', zone='"+ port.zone +"' , userid=" + user.Id + "  where id=" + port.Id;
            
                 }

               
                 int count=  ToolKits.DbHelperSQL.ExecuteSql(sqlStr); 
            
            

            return   count;     
        }

        public void Delete(int id)
        {

            string sqlStr = "";

             sqlStr = "delete from tbport where id=" + id;
  
             ToolKits.DbHelperSQL.ExecuteSql(sqlStr);           

        }
    }
}
