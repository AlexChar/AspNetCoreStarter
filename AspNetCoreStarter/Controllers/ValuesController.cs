using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreStarter.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new [] { "value1", "value2" });
        }

        [HttpGet("{id:min(5)}")]
        public IActionResult Get(int id)
        {
            return Ok($"value{id}");
        }
    }
}
