using Microsoft.AspNetCore.Mvc;

namespace ConfigAR.Backend.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet(Name = "/")]
        public string    Get()
        {
            return "Welcome to ConfigAR Backend v.0.3!";
        }
    }
}