using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Vouchers
{
    [Route("api/[controller]")]
    public class AccountsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly VouchersDBContext ctx;

        public AccountsController(VouchersDBContext context)
        {
            ctx = context;
        }

        [HttpGet]
        public IEnumerable<BalanceAccount> Get()
        {
            return ctx.BalanceAccounts;
        }

        [HttpGet("{id}")]
        public BalanceAccount Get(int id)
        {
            return id == 0 ? new BalanceAccount() : ctx.BalanceAccounts.FirstOrDefault(v => v.ID == id);
        }

        [HttpPost]
        public void Post([FromBody]BalanceAccount value)
        {
            ctx.BalanceAccounts.Add(value);
            ctx.SaveChanges();
        }

        [HttpPut]
        public void Put([FromBody]BalanceAccount value)
        {
            ctx.BalanceAccounts.Attach(value);
            ctx.Entry(value).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var v = Get(id);
            if (v != null)
            {
                ctx.Remove(v);
                ctx.SaveChanges();
            }
        }
    }
}