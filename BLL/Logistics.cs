using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public class Logistics
    {
       public readonly static Logistics Instance = new Logistics();
       private readonly DAL.Logistics dal = new DAL.Logistics();


       /// <summary>
       /// check logistics if not exists then add new record
       /// </summary>
       /// <param name="pnbr"></param>
       public void check_Add(string pnbr, int BPId)
       {
           dal.check_Add(pnbr, BPId);
       }
       /// <summary>
       /// 根据BPID获取tsk信息
       /// </summary>
       /// <param name="BPId"></param>
       /// <returns></returns>
       public Model.LgsticForDisplay Get(int BPId, int userId)
       {
           return dal.Get(BPId,userId);
       }
       public void update(Model.LgsticForDisplay obj, int userId)
        {
            dal.update(obj, userId);
        }
        public IList<Model.LgsticForDisplay> List(string pnbr, string islistsku)
        {
            return dal.List(pnbr, islistsku);
        }
        public IList<Model.LgsticForDisplay> List2(int BPId, int userId)
        {
            return dal.List2(BPId, userId);
        }
    }
}
