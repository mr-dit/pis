using Microsoft.AspNetCore.Mvc;
using pis_web_api.Models.db;
using pis_web_api.Models.post;
using pis_web_api.Services;

namespace pis_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalityController : Controller
    {
        private readonly ILogger<LocalityController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;
        private LocalityService _localityService;

        public LocalityController(ILogger<LocalityController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            _localityService = new LocalityService();
        }

        [HttpGet("opensRegister")]
        public IActionResult OpensRegister(string filterValue = "", int pageNumber = 1, int pageSize = 100)
        {
            var (localities, totalItems) = _localityService.GetLocalities(filterValue, pageNumber, pageSize);
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var result = new
            {
                FilterValue = filterValue,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Localities = localities
            };

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var organisation = _localityService.GetEntry(id);

            if (organisation == null)
            {
                return NotFound();
            }

            return Ok(organisation);
        }

        [HttpPost("addEntry")]
        public IActionResult AddEntry([FromBody] LocalityPost localityPost)
        {
            if (ModelState.IsValid)
            {
                var locality = localityPost.ConvertToLocality();
                bool status = _localityService.AddEntry(locality);

                if (status)
                {
                    return Ok(locality.IdLocality);
                }
                else
                {
                    return BadRequest("Failed to add organisation entry.");
                }
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpPost("deleteEntry/{id}")]
        public IActionResult DeleteEntry(int id)
        {
            var status = _localityService.DeleteEntry(id);

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
        public IActionResult ChangeEntry(int id, [FromBody] LocalityPost localityPost)
        {
            if (ModelState.IsValid)
            {
                var locality = localityPost.ConvertToLocalityWithId(id);
                bool status = _localityService.ChangeEntry(locality);

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
