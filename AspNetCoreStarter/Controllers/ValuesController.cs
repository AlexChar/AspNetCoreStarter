using AspNetCoreStarter.Services.Calculations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreStarter.Controllers
{
    [Route("api/[controller]"), AllowAnonymous]
    public class ValuesController : Controller
    {
        private readonly ICounterService<int> _counterService;

        public ValuesController(ICounterService<int> counterService)
        {
            _counterService = counterService;
        }

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

        [HttpGet("increment")]
        public IActionResult GetIncrement()
        {
            _counterService.Increment();

            return Ok(_counterService.GetValue());
        }
    }
}
