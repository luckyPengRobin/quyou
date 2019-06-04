using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
  public  class LgsticForDisplay
    {
        public int BPId { get; set; }
        public string PNbr { get; set; }
        public string engTskName { get; set; }
        public string chsTskName { get; set; }
        public string EngRole { get; set; }
        public string ChsRole { get; set; }
        public string Cdt { get; set; }
        public string ModUser { get; set; }
        public string comments { get; set; }
        public string confirmComments { get; set; }   //到货确认备注
        public string ArrvialDate { get; set; }   //到货日期

        //采购单信息
        public int mainId { get; set; }
        public string FBA { get; set; }
        public string Sku { get; set; }
        public Nullable<decimal> OuterBoxDim { get; set; }
        public Nullable<decimal> OuterBoxQty { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public string DestPort { get; set; }
        public string ShipCat { get; set; }
        public Nullable<decimal> FinalQty { get; set; }


        //物流信息
        public Nullable<int> Id { get; set; }
        public string Financecomments { get; set; }
        public string LgcNbr { get; set; }   //物流单号
        public string yundanNbr { get; set; }   //运单号
        public decimal carriageChrge { get; set; }   //运费
        public decimal traiffChrge { get; set; }   //关税
        public decimal InspecChrge { get; set; }   //检查费
        public decimal others { get; set; }   //其它费用
        public decimal amount { get; set; }   //一个采购单下的总物流费用
        public decimal totalChrg { get; set; } //一个FBA下的总物流费用
      

        public int SupplierId { get; set; } //付款方/供应商Id
        public string Supplier { get; set; }//付款方/供应商
        public Nullable<int> LSupplierId { get; set; }

    }
}
