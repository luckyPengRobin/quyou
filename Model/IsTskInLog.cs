using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
   public class IsTskInLog
    {
       //基本信息
       public string PNbr { get; set; }
       public string ModUser { get; set; }
       public string Cdt { get; set; }
       public string Status { get; set; }
       public string coments { get; set; }

       //判断是否存在操作记录 1-->存在; 0-->不存在
       public bool Approval { get; set; }
       public bool Purchase { get; set; }
       public bool FinPOPay { get; set; }
       public bool FinTaxPay { get; set; }
       public bool LgcOutOfStock { get; set; }
       public bool LgcConfirmArrival { get; set; }

       //根据权限判断是否显示数据 1-->显示; 0-->不显示
       public bool showApp { get; set; }
       public bool showPurchs { get; set; }
       public bool showFinance { get; set; }
       public bool showLgc { get; set; }


       //BPId
       public int BPIdApp { get; set; }
       public int BPIdPurchs { get; set; }
       public int BPIdPOPay { get; set; }
       public int BPIdTaxPay { get; set; }
       public int BPIdOutOfStock { get; set; }
       public int BPIdConfirmArrival { get; set; }
    }
}
