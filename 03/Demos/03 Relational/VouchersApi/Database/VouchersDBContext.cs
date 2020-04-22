using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Vouchers {
    //To manage Migrations & create the DB go to console:
    //[dotnet restore]
    //dotnet ef migrations add MIGRATION-NAME
    //dotnet ef database update

    public class VouchersDBContext : IdentityDbContext //Use DbContext if not using Identity
    {
        public VouchersDBContext (DbContextOptions<VouchersDBContext> options) : base (options) { }

        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<VoucherDetail> VoucherDetails { get; set; }
        public DbSet<BalanceAccount> BalanceAccounts { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {

            List<BalanceAccount> accounts = new List<BalanceAccount> ();
            var a1 = new BalanceAccount { Name = "Depreciation", Expense = true, ActivatedOn = DateTime.Now.AddYears (-1) };
            var a2 = new BalanceAccount { Name = "Car Maintenance", Expense = true, ActivatedOn = DateTime.Now.AddYears (-1) };
            var a3 = new BalanceAccount { Name = "Development", Expense = false, ActivatedOn = DateTime.Now.AddYears (-1) };
            var a4 = new BalanceAccount { Name = "Consulting", Expense = false, ActivatedOn = DateTime.Now.AddYears (-1) };
            var a5 = new BalanceAccount { Name = "Training", Expense = false, ActivatedOn = DateTime.Now.AddYears (-1) };
            var a6 = new BalanceAccount { Name = "Software", Expense = true, ActivatedOn = DateTime.Now.AddYears (-1) };
            var a7 = new BalanceAccount { Name = "Hosting & Internet", Expense = true, ActivatedOn = DateTime.Now.AddYears (-1) };
            modelBuilder.Entity<BalanceAccount> ().HasData (accounts.ToArray ());
        }
    }
}