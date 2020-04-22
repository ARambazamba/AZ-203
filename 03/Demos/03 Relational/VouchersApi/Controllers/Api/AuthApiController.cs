using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;

namespace Vouchers.Auth
{
    [Route("api/[controller]")]
    public class AuthApiController : Microsoft.AspNetCore.Mvc.Controller
    {
        [HttpGet]
        [Route("getWinUser")]
        public ActionResult Get()
        {
            var identity = HttpContext.User.Identity;
            var wi = identity as WindowsIdentity;
            return identity is WindowsIdentity
                ? Json(identity.Name)
                : Json("Not Authenticated");
        }

        [HttpGet]
        [Route("usefbauth")]
        public string UseFbAuth()
        {
            return HttpContext.User.Identity is WindowsIdentity identity
                ? identity.Name
                : "Not Authenticated";
        }

    }
}