using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace Model
{
    //[Serializable]
    public class PortInfo
    {
        public int Id { get; set; }
        public string PortName { get; set; }

        public string PortCode { get; set; }

        public string area { get; set; }

        public string PostCode { get; set; }

        public string zone { get; set; }
     
        public string ChsPortName { get; set; }
       
        public string Country { get; set; }
        public string ChsCountry { get; set; }      
        public string UserName { get; set; }       
        //public bool RememberMe { get; set; }


    }
}
