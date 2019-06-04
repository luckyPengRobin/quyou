using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public class tbUser
    {
       public readonly static tbUser Instance = new tbUser();
       private readonly DAL.tbUser dal = new DAL.tbUser();
       public IList<Model.UserInfo> list(string status)
       {
           return dal.list(status);
       }

       public Model.UserInfo Get(int id)
       {
           return dal.Get(id);
       }

       public KeyValuePair<int, IList<Model.UserInfo>> List2(int pageIndex, int pageSize, string sortName, string sortType, string search, string UserStatus)
       {
           return dal.List2(pageIndex, pageSize, sortName, sortType, search, UserStatus);
       }

       public IList<Model.clsDefine.selectOption> list_Users(string name)
        {
            return dal.list_Users(name);
        }
        public void Delte(int id,int modUserId)
       {
           dal.Delte(id, modUserId);
       }
        public Model.clsDefine.retutnMsg Save(Model.UserInfo mod, string actType, int modUserId)
        {
            return dal.Save(mod, actType, modUserId);
        }

    }
}
