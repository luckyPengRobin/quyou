using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace Model
{
  public  class SKUInfo
    {


        public SKUInfo()
        {
            Files = new List<HttpPostedFileBase>();
        }

        public List<HttpPostedFileBase> Files { get; set; }
        public int Id { get; set; }

        public string sku { get; set; }
        public string chsName { get; set; }

        public string imgUrl { get; set; }

      //  public HttpPostedFileBase[] attach { get; set; }
        public int createId { get;set; }

        public DateTime Cdt{get;set;}
        
        public DateTime Udt{get;set;}

        public string remark { get; set; }

         
    }
}
