using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;   //linq 动态查询
using System.Linq.Expressions;
using System.Text;
using System.Data;
namespace DAL
{
   public class tbUser
    {
       /// <summary>
       /// 根据状态获取用户信息
       /// </summary>
       /// <param name="status"></param>
       /// <returns></returns>
       public IList<Model.UserInfo> list(string status)
       {
           string strSql = "exec [dbo].[sp_proc_list_users] @status = '" + status + "'";
           DataSet ds = ToolKits.DbHelperSQL.Query(strSql);
           if(ds != null && ds.Tables[0].Rows.Count>0)
           {
               return ToolKits.ModelConvertHelper<Model.UserInfo>.ConvertToModel(ds.Tables[0]);
           }
           return new List<Model.UserInfo>();


       }
       public Model.UserInfo Get(int id)
       {
           string strSql = "exec [dbo].[sp_proc_list_users] @Id = " + id + "";
           DataSet ds = ToolKits.DbHelperSQL.Query(strSql);
           if (ds != null && ds.Tables[0].Rows.Count > 0)
           {
               return ToolKits.ModelConvertHelper<Model.UserInfo>.ConvertToEntity(ds.Tables[0]);
           }
           return new Model.UserInfo();

       }

       public void Delte(int id,int modUserId)
       {
           string strSql = "exec [dbo].[sp_proc_user] @actType='" + Model.clsDefine.TaskCategory.Del + "',@userId = " + id + ",@modUserId=" + modUserId + "";
           ToolKits.DbHelperSQL.ExecuteSql(strSql);
       }








       public KeyValuePair<int, IList<Model.UserInfo>> List2(int pageIndex, int pageSize, string sortName, string sortType, string search, string UserStatus)
       {

           string strSql = "exec [dbo].[sp_proc_list_users] @status = '" + UserStatus + "'";
           DataSet ds = ToolKits.DbHelperSQL.Query(strSql);
           if (ds.Tables[0].Rows.Count > 0)
           {
               var rst = ToolKits.ModelConvertHelper<Model.UserInfo>.ConvertToModel(ds.Tables[0]);
               if (!string.IsNullOrEmpty(search))
               {
                   rst = rst.Where(p => p.name.Contains(search)).ToList();
               }
               int rowcount = rst.Count();

               if (!string.IsNullOrEmpty(sortName))
               {
                   var orderExpression = string.Format("{0} {1}", sortName, sortType); //sortName排序的名称 sortType排序类型 （desc asc）
                   rst = rst.OrderBy(orderExpression).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                   return new KeyValuePair<int, IList<Model.UserInfo>>(rowcount, rst);
               }
               rst = rst.OrderByDescending(o => o.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
               return new KeyValuePair<int, IList<Model.UserInfo>>(rowcount, rst);

           }
           return new KeyValuePair<int, IList<Model.UserInfo>>(0, null);

       }

       public IList<Model.clsDefine.selectOption> list_Users(string name)
       {
           string strSql = "select top 10 Id as id,name as text from [dbo].[tbUser] where name like '%" + name + "%'";
           return ToolKits.ModelConvertHelper<Model.clsDefine.selectOption>.ConvertToModel(ToolKits.DbHelperSQL.Query(strSql).Tables[0]);
       }
       /// <summary>
       /// 保存和更新
       /// </summary>
       /// <param name="mod"></param>
       /// <param name="actType"></param>
       /// <param name="modUserId"></param>
       public Model.clsDefine.retutnMsg Save(Model.UserInfo mod, string actType, int modUserId)
       {
           try
           {
               mod.SupervisorId = mod.SupervisorId != null ? mod.SupervisorId : 0;
               string strSql = "exec [dbo].[sp_proc_user] @actType = '" + actType + "',@userId = " + mod.Id + ",@modUserId = " + modUserId + ",@name = N'" + mod.name + "',@email = N'" + mod.email + "',@roleIds = '" + mod.hidenRoles + "',@supId = " + mod.SupervisorId + ",@job = N'" + mod.JobTitle + "'";
               ToolKits.DbHelperSQL.ExecuteSql(strSql);
               return new Model.clsDefine.retutnMsg { state = true, msg = "保存成功" };
           }
           catch(Exception ex)
           {
               return new Model.clsDefine.retutnMsg { state = true, msg = ex.Message };
           }

       }



    }
}
