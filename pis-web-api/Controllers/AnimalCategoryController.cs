using Microsoft.AspNetCore.Mvc;
using pis.Models;
using pis_web_api.Services;

namespace pis_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalCategoryController : Controller
    {
        private readonly ILogger<AnimalCategoryController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;
        private AnimalCategoryService _animalCategoryService;

        public AnimalCategoryController(ILogger<AnimalCategoryController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            _animalCategoryService = new AnimalCategoryService();
        }

        [HttpGet("opensRegister")]
        public IActionResult GetCategories()
        {
            var categories = _animalCategoryService.GetAnimalsCategories();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var organisation = _animalCategoryService.GetEntry(id);

            if (organisation == null)
            {
                return NotFound();
            }

            return Ok(organisation);
        }

        [HttpPost("addEntry")]
        public IActionResult AddEntry([FromBody] AnimalCategory category)
        {
            bool status = _animalCategoryService.FillData(category);

            if (status)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Failed to add organisation entry.");
            }
        }

        [HttpPost("deleteEntry/{id}")]
        public IActionResult DeleteEntry(int id)
        {
            var status = _animalCategoryService.DeleteEntry(id);

            if (status)
            {
                return Ok();
            }
            else
            {
                return BadRequest($"Failed to delete organisation entry with ID {id}");
            }
        }

        [HttpPost("changeEntry/{id}")]
        public IActionResult ChangeEntry(int id, [FromBody] AnimalCategory category)
        {
            if (ModelState.IsValid)
            {
                bool status = _animalCategoryService.ChangeEntry(category);

                if (status)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Failed to update organisation entry.");
                }
            }

            return BadRequest(ModelState);
        }
    }
}
