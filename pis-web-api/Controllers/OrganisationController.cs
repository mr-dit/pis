using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pis.Models;
using pis.Repositorys;
using pis.Services;


namespace pis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganisationController : ControllerBase
    {
        private readonly ILogger<OrganisationController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;
        private OrganisationService _organisationService;

        public OrganisationController(ILogger<OrganisationController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            _organisationService = new OrganisationService();
        }

        [HttpGet("opensRegister")]
        public IActionResult OpensRegister(string filterValue = "", string filterField = "", string sortBy = nameof(Organisation.OrgName), bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var (organisations, totalItems) = _organisationService.GetOrganisations(filterField, filterValue, sortBy, isAscending, pageNumber, pageSize);
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
            var organisation = _organisationService.GetEntry(id);

            if (organisation == null)
            {
                return NotFound();
            }

            return Ok(organisation);
        }

        [HttpPost("addEntry")]
        public IActionResult AddEntry([FromBody] Organisation organisation)
        {
            bool status = _organisationService.FillData(organisation);

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
            var status = _organisationService.DeleteEntry(id);

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
            var existingOrganisation = _organisationService.GetEntry(id);

            if (existingOrganisation == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool status = _organisationService.ChangeEntry(existingOrganisation);

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