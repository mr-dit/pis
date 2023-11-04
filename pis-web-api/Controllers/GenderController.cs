using Microsoft.AspNetCore.Mvc;
using pis_web_api.Services;

namespace pis_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;
        private GenderService _genderService;

        public GenderController(ILogger<UserController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            _genderService = new GenderService();
        }

        [HttpGet("opensRegister")]
        public IActionResult OpensRegister()
        {
            var genders = _genderService.GetGenders();
            return Ok(genders);
        }

        [HttpGet("{id}")]
        public IActionResult GetGender(int id)
        {
            var organisation = _genderService.GetEntry(id);

            if (organisation == null)
            {
                return NotFound();
            }

            return Ok(organisation);
        }
    }
}
