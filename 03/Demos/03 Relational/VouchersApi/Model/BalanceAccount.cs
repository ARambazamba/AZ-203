using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Vouchers
{
    //Don't use Accounts as Class name because the Accounts Tbl is used for ASP.NET Identity
    public class BalanceAccount
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required, StringLength(50, ErrorMessage = "Account name must not exceed 50 chars.")]
        [MinLength(4, ErrorMessage = "Please use at least 4 chars ...")]
        public string Name { get; set; } 

        public bool Expense { get; set; }

        public DateTime ActivatedOn {get; set; }

        public bool Deprecated { get; set; }

        [JsonIgnore]
        public ICollection<VoucherDetail> VoucherDetails { get; set; }

    }
}
