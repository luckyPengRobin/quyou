using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public class MgrMent
    {
       public readonly static MgrMent Instance = new MgrMent();
       private readonly DAL.MgrMent dal = new DAL.MgrMent();

       public Model.tbCurrency Get_Currency(string cyname)
        {
            return dal.Get_Currency(cyname);
        }
        public Model.tbSupplier Get_Supplier(string name)
        {
            return dal.Get_Supplier(name);
        }
    }
}
