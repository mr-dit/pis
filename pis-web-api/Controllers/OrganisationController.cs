using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using pis_web_api.Models;
using pis_web_api.Services;
using System.Diagnostics;

namespace pis_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganisationController : ControllerBase
    {
        private readonly ILogger<OrganisationController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;

        public OrganisationController(ILogger<OrganisationController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
        }

        [HttpGet("opensRegister")]
        public IActionResult OpensRegister(string? filterValue, string? sortBy, bool isAscending, string filterField = nameof(Organisation.OrgId), int pageNumber = 1, int pageSize = 10)
        {
            filterValue = filterValue?.ToLower();

            var organisations = OrganisationService.GetOrganisations(filterField, filterValue, sortBy, isAscending, pageNumber, pageSize);
            var totalItems = OrganisationService.GetTotalOrganisations(filterField, filterValue);
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
                Organisations = organisations
            };

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrganisation(int id)
        {
            var organisation = OrganisationService.GetEntry(id);

            if (organisation == null)
            {
                return NotFound();
            }

            return Ok(organisation);
        }

        [HttpPost("addEntry")]
        public IActionResult AddEntry([FromBody] Organisation organisation)
        {
            bool status = OrganisationService.FillData(organisation);

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
            var status = OrganisationService.DeleteEntry(id);

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
        public IActionResult ChangeEntry(int id, [FromBody] Organisation organisation)
        {
            var existingOrganisation = OrganisationService.GetEntry(id);

            if (existingOrganisation == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool status = OrganisationService.ChangeEntry(existingOrganisation);

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
