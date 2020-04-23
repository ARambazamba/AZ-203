using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MSALDaemon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        AILogger logger;
        GraphCfg config;
        public MailController(AILogger Logger, GraphCfg cfg)
        {
            logger = Logger;
        }

        [HttpGet("")]
        public ActionResult SendMail()
        {

            GraphHelper.Send("Hello World", "A msg from me", new[] { "alexander.pajer@sighthounds.at" }, config, logger);
            return Ok();
        }

    }
}