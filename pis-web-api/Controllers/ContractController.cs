using Microsoft.AspNetCore.Mvc;
using pis.Services;
using pis_web_api.Models.db;
using pis_web_api.Services;

namespace pis_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : Controller
    {
        private readonly ILogger<ContractController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;
        private ContractService _contractService;

        public ContractController(ILogger<ContractController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            _contractService = new ContractService();
        }


        [HttpGet("opensRegisterByFilter")]
        public IActionResult OpensRegisterByFilter(string filterValue = "", string filterField = "", string sortBy = nameof(Contract.Customer), bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var (contracts, totalItems) = _contractService.GetContractsByFilter(filterField, filterValue, sortBy, isAscending, pageNumber, pageSize);
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
                Contracts = contracts
            };

            return Ok(result);
        }

        [HttpGet("opensRegisterByDate")]
        public IActionResult OpensRegisterByDate(DateOnly? startDateFilter, DateOnly? endDateFilter, string filterField = "", string sortBy = nameof(Contract.ConclusionDate), bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var (contracts, totalItems) = _contractService.GetContractsByDate(filterField, startDateFilter, endDateFilter, sortBy, isAscending, pageNumber, pageSize);
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var result = new
            {
                StartDateFilter = startDateFilter,
                EndDateFilter = endDateFilter,
                FilterField = filterField,
                SortBy = sortBy,
                IsAscending = isAscending,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Contracts = contracts
            };

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _contractService.GetEntry(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("addEntry")]
        public IActionResult AddEntry([FromBody] Contract con)
        {
            bool status = _contractService.AddEntry(con);

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
            var status = _contractService.DeleteEntry(id);

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
        public IActionResult ChangeEntry(int id, [FromBody] Contract con)
        {
            if (ModelState.IsValid)
            {
                bool status = _contractService.ChangeEntry(con);

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
