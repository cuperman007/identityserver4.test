using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize("Api1Policy")]
    public class Api1Controller : ControllerBase
    {
        private readonly ILogger<Api1Controller> _logger;

        public Api1Controller(ILogger<Api1Controller> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult("I am API 1");
        }
    }
}
