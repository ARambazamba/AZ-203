using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MSALDaemon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        GraphCfg config;
        public MailController(GraphCfg cfg)
        {
            config = cfg;
        }

        [HttpGet]
        public ActionResult SendMail()
        {
            GraphHelper.Send("Hello World", "A msg from me", new[] { "alexander.pajer@sighthounds.at" }, config);
            return Ok();
        }

    }
}