using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
  public  class MgrMent
    {
      public Model.tbCurrency Get_Currency(string cyname)
      {
          using (var _db = new Model.quyouEntities())
          {
              return _db.tbCurrencies.Where(p => p.CyCat.Equals(cyname) || p.Descr.Equals(cyname)).FirstOrDefault();
          }
      }
      public Model.tbSupplier Get_Supplier(string suppliername)
      {
          using (var _db = new Model.quyouEntities())
          {
              return _db.tbSuppliers.Where(p => p.ChsName.Equals(suppliername) || p.EngName.Equals(suppliername)).FirstOrDefault();
          }
      }


    }
}
