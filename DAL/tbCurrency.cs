using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
   public class tbCurrency
    {
       public Model.tbCurrency Get_by_name(string cyname)
       {
           using(var _db = new Model.quyouEntities())
           {
               return _db.tbCurrencies.Where(p => p.CyCat.Equals(cyname) || p.Descr.Equals(cyname)).FirstOrDefault();
           }
       }
    }
}
