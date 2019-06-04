using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data;
using System.Reflection;
namespace ToolKits
{
    public class ModelConvertHelper
    {
 
    }
 

    /// <summary>
    /// DataTable转换为任何模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ModelConvertHelper<T> where T : new()
    {
        /// <summary>    
        /// 实体转换辅助类    
        /// </summary>    

            /// <summary>
            /// Data table to list   使用方法:
            /// DataTable dt = DbHelper.ExecuteDataTable(...);  
            /// 把DataTable转换为IList<UserInfo>  
            ///IList<UserInfo> users = ModelConvertHelper<UserInfo>.ConvertToModel(dt);
            /// </summary>
            /// <param name="dt"></param>
            /// <returns></returns>
        public static IList<T> ConvertToModel(DataTable dt)
        {
            // 定义集合    
            IList<T> ts = new List<T>();

            // 获得此模型的类型   
            Type type = typeof(T);
            string tempName = "";

            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性      
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;  // 检查DataTable是否包含此列    

                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter      
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                ts.Add(t);
            }
            return ts;
        }
        /// <summary>
        /// Data table to entity
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static T ConvertToEntity(DataTable dt)
        {
            // 获得此模型的类型   
            Type type = typeof(T);
            string tempName = "";
            T t = new T();
            foreach (DataRow dr in dt.Rows)
            {
               
                // 获得此模型的公共属性      
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;  // 检查DataTable是否包含此列    

                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter      
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
            
            }
            return t;


        }

      
    }
}
