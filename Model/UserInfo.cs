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
    public class UserInfo
    {
        public int Id { get; set; }
        public string name { get; set; }
        [Required]
        [Display(Name = "用户邮箱")]
        public string email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "用户密码")]
        public string Password { get; set; }
        public int status { get; set; }
        public string statusName { get; set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        public string roles { get; set; }
        public string strRoles { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }




        public string JobTitle { get; set; }
        public Nullable<int> SupervisorId { get; set; }
        public string Supervisor { get; set; }
        public string Cdt { get; set; }
        public string StartDate { get; set; }
        public string LeaveDate { get; set; }

        public string hidenRoles { get; set; }

        public string yunying { get; set; }
        public string Approval { get; set; }
        public string Purchase { get; set; }
        public string Finance { get; set; }
        public string Logistic { get; set; }
        public string HR { get; set; }
        public string Sales { get; set; }

        public string Del { get; set; }

        public string ManageStock { get; set; }
        public string ManageProfit { get; set; }

    }
}
