using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    
   public class tbCurrency
    {
       public readonly static tbCurrency Instance = new tbCurrency();
       private readonly DAL.tbCurrency dal = new DAL.tbCurrency();  

       public Model.tbCurrency Get_by_name(string cyname)
       {
           return dal.Get_by_name(cyname);
       }



    }
}
