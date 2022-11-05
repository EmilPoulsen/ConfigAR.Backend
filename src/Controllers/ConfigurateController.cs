using ConfigAR.Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ConfigAR.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurateController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfigurateService configurateService;

        public ConfigurateController(
            ILogger<WeatherForecastController> logger,
            IConfigurateService configurateService)
        {
            _logger = logger;
            this.configurateService = configurateService;
        }

        [HttpGet(Name = "")]
        public async Task<string> Get()
        {
            await this.configurateService.Execute();
            return "hello";
        }
    }
}