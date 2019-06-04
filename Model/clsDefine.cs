using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
   public class clsDefine
    {

       public class retutnMsg
       {
           public bool state { get; set; }
           public string msg { get; set; }
       }


       public class selectOption
       {
           public object id { get; set; }
           public string text { get; set; }
       }
       public enum EmType { Purchase}

       public class RqstType
       {
           public const string Purchase = "Purchase";
           public const string Logistics = "Logistics";      
       }

       public class TaskCategory
       {
          
           public const string POSave = "POSave";
           public const string POSubmit = "POSubmit";

           public const string ApprovalSave = "AppSave";
           public const string ApprovalSubmit = "AppSubmit";
           public const string ApprovalRework = "AppRework";

           public const string PurchsSave = "PurchsSave";
           public const string PurchsSubmit = "PurchsSubmit";

           public const string POPaySave = "POPaySave";
           public const string POPaySubmit = "POPaySubmit";

           public const string TaxPaySave = "TaxPaySave";
           public const string TaxPaySubmit = "TaxPaySubmit";


           public const string OutOfStockSave = "OutOfStockSave";
           public const string OutOfStockSubmit = "OutOfStockSubmit";


           public const string ConfirmArrivalSave = "ConfirmArrivalSave";    
           public const string ConfirmArrivalSubmit = "ConfirmArrivalSubmit";


           public const string Cancelled = "Cancelled";

           public const string Del = "del";

           public const string New = "New";
           public const string Edit = "Edit";
           public const string Save = "Save";
           public const string COSaveForLater = "COSaveFLter";
           public const string COSubmit = "COSubmit";
           public const string PETeamSubmit = "PETeamSubmit";
           public const string PESubmit = "PESubmit";
           public const string DCUSubmit = "DCUSubmit";




       }


        /// <summary>
        /// 注意状态ID必须和数据库里状态ID想对应
        /// </summary>
        public class StatusName
        {
            //Change Request Status
            public const string Draft = "Draft";
            public const string InProcess = "InProcess";


            public const string Finished = "Finished";
            public const string Active = "Active";
            public const string InActive = "InActive";


            public const string Reject = "Reject";
            public const string Cancelled = "Cancelled";
      
      
        

        }

       public class FileType
       {
           public const string POTicket = "POTicket";
           public const string TaxTicket = "TaxTicket";
       }




    }
}
