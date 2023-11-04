using Microsoft.AspNetCore.Mvc;
using pis_web_api.Models.db;
using pis_web_api.Services;

namespace pis_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrgTypeController : Controller
    {
        private readonly ILogger<OrgTypeController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;
        private OrgTypeService _orgTypeService;

        public OrgTypeController(ILogger<OrgTypeController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            _orgTypeService = new OrgTypeService();
        }

        [HttpGet("opensRegister")]
        public IActionResult OpensRegister(string filterValue = "", int pageNumber = 1, int pageSize = 10)
        {
            var (users, totalItems) = _orgTypeService.GetOrgTypes(filterValue, pageNumber, pageSize);
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var result = new
            {
                FilterValue = filterValue,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Users = users
            };

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrgType(int id)
        {
            var organisation = _orgTypeService.GetEntry(id);

            if (organisation == null)
            {
                return NotFound();
            }

            return Ok(organisation);
        }

        [HttpPost("addEntry")]
        public IActionResult AddEntry([FromBody] OrgType orgType)
        {
            bool status = _orgTypeService.AddEntry(orgType);

            if (status)
            {
                return Ok(orgType.IdOrgType);
            }
            else
            {
                return BadRequest("Failed to add organisation entry.");
            }
        }

        [HttpPost("deleteEntry/{id}")]
        public IActionResult DeleteEntry(int id)
        {
            var status = _orgTypeService.DeleteEntry(id);

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
        public IActionResult ChangeEntry(int id, [FromBody] OrgType user)
        {
            if (ModelState.IsValid)
            {
                bool status = _orgTypeService.ChangeEntry(user);

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
