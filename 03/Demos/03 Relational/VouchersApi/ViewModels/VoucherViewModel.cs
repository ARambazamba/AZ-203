using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vouchers
{
    public class VoucherViewModel
    {
        public Voucher CurrentVoucher { get; set; }

        public List<BalanceAccount> Accounts { get; set; }        
    }
}
