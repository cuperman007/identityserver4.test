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
    [Authorize("GrootPolicy")]
    public class GrootController : ControllerBase
    {
        private readonly ILogger<GrootController> _logger;

        public GrootController(ILogger<GrootController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult("I am groot!");
        }
    }
}
