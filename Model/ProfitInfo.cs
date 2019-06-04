using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
  public  class ProfitInfo
    {
      public int id { get; set; }

      public string sku { get; set; }

      public string userName { get; set; }

      public decimal proRebate { get; set; }
      public decimal other { get; set; }
      public decimal price { get; set; }

      public int goodReturn { get; set; }
      public decimal amazonCost { get; set; }
      public decimal grandTotal { get; set; }

      public decimal rmbTotal { get; set; }

      public int number { get; set; }

      public decimal totalCost { get; set; }

      public decimal profit { get; set; }

      public DateTime profitMonth { get; set; }

      public DateTime saleDate { get; set; }

      public decimal amazonDamage { get; set; }

      public decimal amazonLost { get; set; }
      public decimal amazonReturning { get; set; }

      public decimal transDetails { get; set; }

    }
}
