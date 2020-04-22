using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VouchersNetCore.Common;

namespace Vouchers.Api {
    //[Authorize]
    [EnableCors ("AllowAll")]
    [Route ("api/[controller]")]
    public class VouchersController : Microsoft.AspNetCore.Mvc.Controller {
        private VouchersDBContext ctx;

        public VouchersController (VouchersDBContext context) {
            ctx = context;
        }

        // http://localhost:PORT/api/vouchers        
        [HttpGet]
        public async Task<IEnumerable<Voucher>> Get () {
            var vouchers = await ctx.Vouchers.Include (f => f.Details).OrderByDescending (v => v.Date).ToListAsync ();
            return vouchers;
        }

        // http://localhost:PORT/vouchers/3
        [HttpGet ("{id}")]
        public Voucher Get (int id) {
            return id == 0 ? new Voucher { Date = DateTime.Now } : ctx.Vouchers.Include (f => f.Details).FirstOrDefault (v => v.ID == id);
        }

        // http://localhost:PORT/api/vouchers -> Create
        [HttpPost]
        public ActionResult Post ([FromBody] Voucher value) {
            if (value.Details != null) {
                foreach (VoucherDetail vd in value.Details) {
                    vd.Account = null;
                }
            }
            ctx.Vouchers.Add (value);
            ctx.SaveChanges ();
            return Ok (value);
        }

        // http://localhost:PORT/api/vouchers -> Update
        [HttpPut ()]
        public ActionResult Put ([FromBody] Voucher value) //Classic .NET Core WebApi pattern: public void Put(int id, [FromBody]Voucher value)
        {
            ctx.Vouchers.Attach (value);
            ctx.Entry (value).State = EntityState.Modified;

            if (value.Details != null) {
                foreach (VoucherDetail vd in value.Details) {
                    switch (ctx.Entry (vd).State) {
                        case EntityState.Added:
                            ctx.VoucherDetails.Add (vd);
                            break;
                        case EntityState.Deleted:
                            ctx.VoucherDetails.Remove (vd);
                            break;
                        default:
                            ctx.VoucherDetails.Attach (vd);
                            ctx.Entry (vd).State = EntityState.Modified;
                            break;
                    }
                }
            }

            ctx.SaveChanges ();
            return Ok (value);
        }

        [HttpDelete ("{id}")]
        public ActionResult Delete (int id) {
            var v = Get (id);
            if (v != null) {
                ctx.Remove (v);
                ctx.SaveChanges ();
            }
            return Ok ();
        }

        // Get implemented using Task Pattern - should be default
        // http://localhost:PORT/api/vouchers/asyncArray        
        [HttpGet]
        [Route ("asyncArray")]
        [ProducesResponseType (typeof (Voucher[]), 200)]
        [ProducesResponseType (typeof (ApiResponse), 400)]
        public async Task<ActionResult> GetResponse () {
            try {
                var vouchers = await ctx.Vouchers.OrderByDescending (v => v.Date).ToArrayAsync ();
                return Ok (vouchers);
            } catch (Exception exp) {
                return BadRequest (new ApiResponse { Status = false, Data = exp.Message });
            }
        }

        //Custom Routes

        // GET: http://localhost:PORT/api/vouchers/getsum/false | true
        [HttpGet]
        [Route ("GetSum/{expenses}")]
        public async Task<string> GetSum (bool expenses) {
            var accts = await ctx.BalanceAccounts.Where (f => f.Expense == expenses).Select (f => f.ID).ToListAsync ();
            var vds = await ctx.VoucherDetails.Where (f => f.Account != null && accts.Contains (f.AccountID)).SumAsync (f => f.Amount);
            return expenses ? "Total Expenses: " : "Total Income: " + vds;
        }

        // GET: http://localhost:PORT/api/vouchers/getvm/1
        [HttpGet]
        [Route ("getvm/{id}")]
        public async Task<VoucherViewModel> GetVoucherViewModelAsync (int ID) {
            VoucherViewModel result = new VoucherViewModel {
                CurrentVoucher = ID == 0 ? null : await ctx.Vouchers.Include (v => v.Details).FirstOrDefaultAsync (f => f.ID == ID),
                Accounts = await ctx.BalanceAccounts.ToListAsync ()
            };
            return result;
        }

        //Save implemented as one method
        // POST: http://localhost:PORT/api/vouchers/save
        [HttpPost]
        [Route ("Save")]
        public int Save ([FromBody] Voucher value) {
            if (value.ID == 0) {
                ctx.Vouchers.Add (value);
            } else {
                //Update using attach and entity state pattern
                ctx.Vouchers.Attach (value);
                ctx.Entry (value).State = EntityState.Modified;

                if (value.Details != null) {
                    foreach (VoucherDetail vd in value.Details) {
                        switch (ctx.Entry (vd).State) {
                            case EntityState.Added:
                                ctx.VoucherDetails.Add (vd);
                                break;
                            case EntityState.Deleted:
                                ctx.VoucherDetails.Remove (vd);
                                break;
                            default:
                                ctx.VoucherDetails.Attach (vd);
                                ctx.Entry (vd).State = EntityState.Modified;
                                break;
                        }
                    }
                }
            }
            ctx.SaveChanges ();
            return value.ID;
        }
    }
}