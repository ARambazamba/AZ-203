using Microsoft.AspNetCore.Identity;

namespace Vouchers
{
    public class VoucherRole : IdentityRole
    {
        public string Description { get; set; }
    }
}