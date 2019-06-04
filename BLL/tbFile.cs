using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public class tbFile
    {
       public readonly static tbFile Instance = new tbFile();
       private readonly DAL.tbFile dal = new DAL.tbFile();
       public bool exist(string pnbr, string fileType)
       {
           return dal.exist(pnbr, fileType);
       }
       public Model.clsDefine.retutnMsg Add(Model.tbFile obj)
       {
           return dal.Add(obj);
       }
       public IList<Model.tbFile> list(string pnbr, string fileType, int userId)
       {
           return dal.list(pnbr, fileType, userId);
       }
       public IList<Model.sp_proc_file_Result> ListDocs(string pnbr,string ftype)
       {
           return dal.ListDocs(pnbr, ftype);
       }
       public void Delete(int Id)
       {
           dal.Delete(Id);
       }
    }

}
