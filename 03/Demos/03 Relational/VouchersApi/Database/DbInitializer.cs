using System;
using System.Linq;

namespace Vouchers
{
    public static class DbInitializer
    {
        public static void Initialize(VouchersDBContext context)
        {
            context.Database.EnsureCreated();

            if (context.BalanceAccounts.FirstOrDefault() == null)
            {
                var a1 = new BalanceAccount { Name = "Depreciation", Expense = true, ActivatedOn = DateTime.Now.AddYears(-1)};
                var a2 = new BalanceAccount { Name = "Car Maintenance", Expense = true, ActivatedOn = DateTime.Now.AddYears(-1) };
                var a3 = new BalanceAccount { Name = "Development", Expense = false, ActivatedOn = DateTime.Now.AddYears(-1) };
                var a4 = new BalanceAccount { Name = "Consulting", Expense = false, ActivatedOn = DateTime.Now.AddYears(-1) };
                var a5 = new BalanceAccount { Name = "Training", Expense = false, ActivatedOn = DateTime.Now.AddYears(-1) };
                var a6 = new BalanceAccount { Name = "Software", Expense = true, ActivatedOn = DateTime.Now.AddYears(-1) };
                var a7 = new BalanceAccount { Name = "Hosting & Internet", Expense = true, ActivatedOn = DateTime.Now.AddYears(-1) };

                context.BalanceAccounts.AddRange(a1, a2, a3, a4, a5, a6, a7);
                context.SaveChanges();

                var v1 = new Voucher
                {
                    Date = DateTime.Now.AddDays(-2),
                    Amount = 800,
                    Text = "Bogus AG",
                    Paid = false,
                    Expense = false,
                    Remark = true
                };
                var v2 = new Voucher
                {
                    Date = DateTime.Now.AddDays(-2),
                    Amount = 65,
                    Text = "BP Tankstelle",
                    Paid = false,
                    Expense = true,
                    Remark = true
                };
                var v3 = new Voucher
                {
                    Date = DateTime.Now.AddDays(-2),
                    Amount = 56,
                    Text = "Amazon",
                    Paid = false,
                    Expense = true
                };
                var v4 = new Voucher
                {
                    Date = DateTime.Now.AddDays(-3),
                    Amount = 100,
                    Text = "Media Markt",
                    Paid = true,
                    Expense = true
                };
                context.Vouchers.AddRange(v1, v2, v3, v4);
                context.SaveChanges();

                var vd1 = new VoucherDetail { VoucherID = v4.ID, Text = "Ladekabel", Amount = 100, Account = a1 };
                var vd7 = new VoucherDetail
                {
                    VoucherID = v3.ID,
                    Text = "Game of Thrones, Season 6",
                    Amount = 29,
                    Account = a6
                };
                var vd2 = new VoucherDetail { VoucherID = v3.ID, Text = "USB Stick", Amount = 11, Account = a1 };
                var vd3 = new VoucherDetail { VoucherID = v3.ID, Text = "DVI Kabel", Amount = 45, Account = a1 };
                var vd4 = new VoucherDetail { VoucherID = v2.ID, Text = "Diesel", Amount = 45, Account = a2 };
                var vd6 = new VoucherDetail { VoucherID = v2.ID, Text = "Reifenwechsel", Amount = 20, Account = a2 };
                var vd5 = new VoucherDetail { VoucherID = v1.ID, Text = "Remote Support", Amount = 800, Account = a4 };

                context.VoucherDetails.AddRange(vd1, vd2, vd3, vd4, vd5, vd6, vd7);
                context.SaveChanges();
            }

        }
    }
}