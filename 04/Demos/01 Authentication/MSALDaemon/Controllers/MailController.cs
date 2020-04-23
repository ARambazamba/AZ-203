using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MSALDaemon.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase {

        AILogger logger;
        public MailController (AILogger Logger, ) {
            logger = Logger;
        }

        // GET api/mail
        [HttpGet ("")]
        public ActionResult SendMail () {

            // GraphHelper.Send ("Hello World", "A msg from me", new [] { "alexander.pajer@sighthounds.at" }, config, Receiver, logger);
            return Ok ();
        }

    }
}