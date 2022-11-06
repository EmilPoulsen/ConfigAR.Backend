using ConfigAR.Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

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
            IConfigurateService configurateService,
            IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            this.configurateService = configurateService;
        }

        [HttpGet(Name = "")]
        public async Task<FileStreamResult> Get()
        {
            string modelId = "shapemakersample-5";
            Stream glb = await this.configurateService.Execute(modelId);

            return new FileStreamResult(glb, new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream"))
            {
                FileDownloadName = "test.glb"
            };
        }
    }
}