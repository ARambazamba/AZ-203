using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using Vouchers

namespace Vouchers
{
    [Route("api/[controller]")]
    public class DemoController : ControllerBase
    {
        public DemoController() { }

        // GET api/demo
        [HttpGet("double/{nbr}")]
        public ActionResult<int> DoDouble(int nbr)
        {
            return nbr * 2;
        }

        // GET api/demo
        [HttpGet("substractOne/{nbr}")]
        public ActionResult<int> SubtractOne(int nbr)
        {
            return nbr * 2;
        }
    }
}