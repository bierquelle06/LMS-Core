using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMS.Common.Core.Authentication;

namespace LMS.Services.Core.Webapi.Controllers
{
    [Route("")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public Guid UserId => (Guid)HttpContext.Items["User"];
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get() => Ok("LMS Core Service");
    }
}