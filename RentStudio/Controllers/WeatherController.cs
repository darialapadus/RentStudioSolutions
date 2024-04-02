using Microsoft.AspNetCore.Mvc;
using RentStudio.Models.DTOs;
using RentStudio.Services.WeatherService;

namespace RentStudio.Controllers
{
    public class WeatherController : BaseController
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("weather/{city}")]
        public IActionResult GetWeather(string city)
        {
            var weather = _weatherService.GetWeatherAsync(city);
            return Ok(weather);
        }
    }

}
