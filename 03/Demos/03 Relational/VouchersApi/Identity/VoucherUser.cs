using System;
using Microsoft.AspNetCore.Identity;

namespace Vouchers
{
    public class VoucherUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Weight { get; set; }
    }
}