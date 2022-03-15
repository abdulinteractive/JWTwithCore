using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InventoryService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        [BindProperty(Name="city", SupportsGet = true)]
        public string cityName { get; set; }
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet]
        [Route("getcity/{id}")]
        public ActionResult<string> GetCity(int id)
        {
            return $"Id : {id}, CityName : {cityName}";
        }
        [HttpGet]
        [Route("getcity1")]
        public ActionResult<string> GetCity1([FromQuery]CParams cParams)
        {
            return $"Id : {cParams.Id}, CityName : {cParams.CityName}";
        }

    }

    public class CParams
    {
        public int Id { get; set; }

        [FromQuery(Name = "city")]
        public string CityName { get; set; }

        // ...
    }
}
