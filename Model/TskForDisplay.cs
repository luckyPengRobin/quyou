using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
   public class TskForDisplay
    {
       public long rownum { get; set; }
       public int BPId { get; set; }
       public string PNbr { get; set; }
       public string engTskName { get; set; }
       public string chsTskName { get; set; }
       public string EngRole { get; set; }
       public string ChsRole { get; set; }
       public string Cdt { get; set; }
       public string ModUser { get; set; }
       public string amount { get; set; }
       public string coments { get; set; }
       public string Status { get; set; }
       public string chsStatus { get; set; }
       public int rowCnt { get; set; }
    }
}
