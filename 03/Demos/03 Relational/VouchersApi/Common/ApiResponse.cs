using Vouchers;

namespace VouchersNetCore.Common {
    public class ApiResponse {
        public bool Status { get; set; }
        public Voucher Voucher { get; set; }
        public object Data { get; set; }
    }

}