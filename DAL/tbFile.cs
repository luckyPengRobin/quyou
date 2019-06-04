using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
   public class tbFile
    {
       public bool exist(string pnbr,string fileType)
       {
           using(var _db = new Model.quyouEntities())
           {
               var sid = DBHelper.Get_Status("Active").Id;
               if(!string.IsNullOrEmpty(fileType))
               {
                   return _db.tbFiles.Where(p => p.PNbr.Equals(pnbr) && p.Ftype.Equals(fileType) && p.StatusId == sid).Any();
               }
               return _db.tbFiles.Where(p => p.PNbr.Equals(pnbr) && p.StatusId == sid).Any();
              
           }
       }
       public Model.clsDefine.retutnMsg Add(Model.tbFile obj)
       {
           Model.clsDefine.retutnMsg rtnmsg = new Model.clsDefine.retutnMsg { state = true, msg = "上传成功！" };
           using(var _db = new Model.quyouEntities())
           {
               try
               {
                   _db.tbFiles.Add(obj);
                   _db.SaveChanges();
               }
               catch(Exception ex)
               {
                   rtnmsg.msg = ex.Message;
                   rtnmsg.state = false;
               }

           }
           return rtnmsg;
       }
       public void Delete(int Id)
       {
           using (var _db = new Model.quyouEntities())
           {
               _db.sp_proc_file(Model.clsDefine.TaskCategory.Del, Id, "","");
               //var obj = _db.tbFiles.Where(p => p.Id == Id).Single();
               //_db.tbFiles.Remove(obj);
               //_db.SaveChanges();

               //Model.tbFile obj = new Model.tbFile() { Id = Id };
               //_db.tbFiles.Attach(obj);
               //_db.tbFiles.Remove(obj);
               //_db.SaveChanges();
           }
       }


       public IList<Model.tbFile> list(string pnbr,string fileType,int userId)
       {
           //int sId = DBHelper.Get_Status(Model.clsDefine.StatusName.Active).Id;
           using(var _db = new Model.quyouEntities())
           {             

               if(!string.IsNullOrEmpty(fileType))
               {
                   return _db.tbFiles.Where(p => p.PNbr.Equals(pnbr) && p.ModUserId == userId && p.Ftype.Equals(fileType)).ToList();
               }
               return _db.tbFiles.Where(p => p.PNbr.Equals(pnbr) && p.ModUserId.Equals(userId)).ToList();




           }





       }



       public IList<Model.sp_proc_file_Result> ListDocs(string pnbr,string ftype)
       {
           using(var _db = new Model.quyouEntities())
           {
               var rst = _db.sp_proc_file("", 0, pnbr, ftype).ToList();
               return rst;
           }
       }



    }
}
