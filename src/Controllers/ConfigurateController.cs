using ConfigAR.Backend.Models;
using ConfigAR.Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace ConfigAR.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigurateController : ControllerBase
    {
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

            //string points = "{\"points\":[[1,0,0],[0.5,0,0],[1.5,1,0],[-0.5,1.5,0]]}";
            string modelId = "shapemakersample-5";
            ConfigarInput input = new ConfigarInput()
            {
                Model = modelId,
                Points = new List<Point>()
             {
                 new Point(1, 0),
                 new Point(0.5, 0),
                 new Point(1.5, 1),
                 new Point(-0.5, 1.5),
             }
            };

            Stream glb = await this.configurateService.Execute(input);

            return new FileStreamResult(glb, new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream"))
            {
                FileDownloadName = "test.glb"
            };
        }

        [HttpPost(Name = "")]
        public async Task<FileStreamResult> Post(ConfigarInput input)
        {
            Stream glb = await this.configurateService.Execute(input);

            return new FileStreamResult(glb, new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream"))
            {
                FileDownloadName = "test.glb"
            };
        }
    }
}