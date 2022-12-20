using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IWeatherForecastServices _service;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastServices service)
        {
            _logger = logger;
            _service = service;
        }


        [HttpPost]
        public ActionResult<string> Hello([FromBody] string name)
        {


            return NotFound($"Hello{name}");
        }

        [HttpPost("/generate")]
        public ActionResult<IEnumerable<WeatherForecast>> Generate([FromQuery] int Count,
            [FromBody] TemperatureRequest request) 
        {
            if (Count < 0 || request.maxTemperature < request.minTemperature)
            {
                return BadRequest();
            }
            var result = _service.Get(Count, request.minTemperature, request.maxTemperature);
            return Ok(result);
        }


    }

}