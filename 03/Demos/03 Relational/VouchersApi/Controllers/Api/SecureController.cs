using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Vouchers.Controller.Api
{
    [Route("api/[controller]")]
    public class SecureController : Microsoft.AspNetCore.Mvc.Controller
    {
        // GET: api/<controller>
        [Authorize]
        [HttpGet]
        public ActionResult Get()
        {
            return Json("Wow ... this is secure!");
        }        
    }
}
