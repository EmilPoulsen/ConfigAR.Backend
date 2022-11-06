using ConfigAR.Backend.Models;
using ConfigAR.Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace ConfigAR.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LibraryController : ControllerBase
    {
        public LibraryController()
        {

        }

        [HttpGet]
        public IActionResult Get()
        {
            List<LibraryItem> lib = new List<LibraryItem>()
            {
                new LibraryItem() { Name = "Office Layout", ModelId = "tthackathonofficelayout-4"},
                new LibraryItem() { Name = "Installation", ModelId = "installationpipes-2"},
                new LibraryItem() { Name = "Rebar Floor", ModelId = "shapemakersample-rebar-4"},
                new LibraryItem() { Name = "Building Gen", ModelId = "building-13"},
            };
            return Ok(lib);
        }
    }

    public class LibraryItem
    {
        public string Name { get; set; }
        public string ModelId { get; set; }
    }
}