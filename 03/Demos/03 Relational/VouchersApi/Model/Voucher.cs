using System;
using System.Collections.Generic;

namespace Vouchers {
    public class Voucher {
        public int ID { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public bool Paid { get; set; }
        public bool Expense { get; set; }
        public bool Remark { get; set; }

        public ICollection<VoucherDetail> Details { get; set; }
    }

}