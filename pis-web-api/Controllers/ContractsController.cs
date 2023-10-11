using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pis_web_api.Models;
using pis_web_api.Services;
using pis_web_api.Repositorys;

namespace pis_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly ILogger<ContractsController> _logger;

        public ContractsController(ILogger<ContractsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("OpensRegister")]
        public IActionResult OpensRegister(string? filterValue, string? sortBy, bool isAscending, string filterField = nameof(Contracts.ContractsId), int pageNumber = 1, int pageSize = 10)
        {
            filterValue = filterValue?.ToLower();

            var contracts = ContractsService.GetContracts(filterField, filterValue, sortBy, isAscending, pageNumber, pageSize);
            var totalItems = ContractsService.GetTotalContracts(filterField, filterValue);
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var result = new
            {
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
        public IActionResult GetContract(int id)
        {
            var organization = OrganisationsRepository.GetEntry(id);
            return Ok(organization);
        }

        [HttpPost("AddEntry")]
        public IActionResult AddEntry([FromBody] Contracts contracts)
        {
            bool status = ContractsService.CreateContract(contracts);
            if (status)
            {
                return Ok("Contract created successfully");
            }

            return BadRequest("Failed to create contract");
        }

        [HttpPost("DeleteEntry/{id}")]
        public IActionResult DeleteEntry(int? id)
        {
            if (id != null)
            {
                var status = ContractsService.DeleteEntry((int)id);

                if (status)
                {
                    return Ok("Contract deleted successfully");
                }

                return BadRequest("Failed to delete contract");
            }

            return NotFound();
        }

        [HttpPost("ChangeEntry/{id}")]
        public IActionResult ChangeEntry([FromBody] Contracts contracts)
        {
            bool status = ContractsService.ChangeEntry(contracts);
            if (status)
            {
                return Ok("Contract updated successfully");
            }

            return NotFound("Contract not found or update failed");
        }
    }
}
