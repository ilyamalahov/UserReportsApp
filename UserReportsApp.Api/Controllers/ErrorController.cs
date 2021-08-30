using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UserReportsApp.Api.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet("error")]
        public ActionResult Error() => Problem();
    }
}
