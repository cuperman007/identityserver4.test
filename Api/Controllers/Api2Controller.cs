using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize("Api2Policy")]
    public class Api2Controller : ControllerBase
    {
        private readonly ILogger<Api2Controller> _logger;

        public Api2Controller(ILogger<Api2Controller> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult("I am API 2");
        }
    }
}
