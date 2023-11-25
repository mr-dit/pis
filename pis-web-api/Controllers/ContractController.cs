using Microsoft.AspNetCore.Mvc;
using pis.Services;
using pis_web_api.Models.db;
using pis_web_api.Models.post;
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

        [HttpGet("opensRegister")]
        public IActionResult OpensRegister(UserPost user, DateOnly startDateFilter, DateOnly endDateFilter, string filterValue = "", string filterField = "",
            string sortBy = nameof(Contract.Customer), bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            List<Contract> contracts;
            int totalItems;

            if(user.Roles.Intersect(new List<int>() {  1, 4, 6 }).Count() != 0)
            {
                (contracts, totalItems) = _contractService.GetContracts(startDateFilter, endDateFilter, filterValue, filterField,
                sortBy, isAscending, pageNumber, pageSize);
            }
            else if(user.Roles.Intersect(new List<int>() { 3, 2, 8, 7, 9, 11, 10 }).Count() != 0)
            {
                (contracts, totalItems) = _contractService.GetContractsByOrg(startDateFilter, endDateFilter, filterValue, filterField,
                sortBy, isAscending, pageNumber, pageSize, user);
            }
            else
            {
                return Forbid();
            }


            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var result = new
            {
                StartDateFilter = startDateFilter,
                EndDateFilter = endDateFilter,
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
        public IActionResult AddEntry([FromBody] ContractPost conPost)
        {
            try
            {
                var con = conPost.ConvertToContract();
                return Ok(con.IdContract);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
        public IActionResult ChangeEntry(int id, [FromBody] ContractPost conPost)
        {
            if (ModelState.IsValid)
            {
                var con = conPost.ConvertToContractWithId(id);

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
