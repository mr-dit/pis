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

        public VaccineController(ILogger<VaccineController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
        }

        [HttpGet("opensRegister")]
        public IActionResult OpensRegister(string filterValue = "", string filterField = "", string sortBy = nameof(Vaccine.NameVaccine), bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var vaccines = VaccineService.GetVaccines(filterField, filterValue, sortBy, isAscending, pageNumber, pageSize);
            var totalItems = VaccineService.GetTotalVaccines(filterField, filterValue);
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
            var organisation = VaccineService.GetEntry(id);

            if (organisation == null)
            {
                return NotFound();
            }

            return Ok(organisation);
        }

        [HttpPost("addEntry")]
        public IActionResult AddEntry([FromBody] Vaccine organisation)
        {
            bool status = VaccineService.FillData(organisation);

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
            var status = VaccineService.DeleteEntry(id);

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
                bool status = VaccineService.ChangeEntry(vaccine);

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
