using Microsoft.AspNetCore.Mvc;
using pis.Controllers;
using pis.Models;
using pis_web_api.Services;

namespace pis_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineController : Controller
    {
        private readonly ILogger<VaccineController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;
        private VaccineService _vaccineService;

        public VaccineController(ILogger<VaccineController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            _vaccineService = new VaccineService();
        }

        [HttpGet("opensRegister")]
        public IActionResult OpensRegister(string filterValue = "", string filterField = "", string sortBy = nameof(Vaccine.NameVaccine), bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var (vaccines, totalItems) = _vaccineService.GetVaccines(filterField, filterValue, sortBy, isAscending, pageNumber, pageSize);
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var result = new
            {
                FilterValue = filterValue,
                FilterField = filterField,
                SortBy = sortBy,
                IsAscending = isAscending,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Vaccines = vaccines
            };

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetVaccine(int id)
        {
            var organisation = _vaccineService.GetEntry(id);

            if (organisation == null)
            {
                return NotFound();
            }

            return Ok(organisation);
        }

        [HttpPost("addEntry")]
        public IActionResult AddEntry([FromBody] Vaccine organisation)
        {
            bool status = _vaccineService.AddEntry(organisation);

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
            var status = _vaccineService.DeleteEntry(id);

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
        public IActionResult ChangeEntry(int id, [FromBody] Vaccine vaccine)
        {
            if (ModelState.IsValid)
            {
                bool status = _vaccineService.ChangeEntry(vaccine);

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
