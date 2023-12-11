using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pis.Repositorys;
using pis.Services;
using pis_web_api.Models.db;
using pis_web_api.Models.post;
using pis_web_api.Services;

namespace pis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganisationController : ControllerBase
    {
        private readonly ILogger<OrganisationController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;
        private OrganisationService _organisationService;
        private readonly JournalService _journalService;

        public OrganisationController(ILogger<OrganisationController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            _organisationService = new OrganisationService();
            _journalService = new JournalService();
        }

        [HttpPost("opensRegister")]
        public IActionResult OpensRegister([FromBody]UserPost user, string filterValue = "", string filterField = "", string sortBy = nameof(Organisation.OrgName), bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            List<Organisation> organisations;
            int totalItems;

            if (user.Roles.Intersect(new List<int>() { 1, 2, 3, 6, 7, 8, 9, 10, 11, 15 }).Count() != 0)
            {
                (organisations, totalItems) = _organisationService.GetOrganisations(filterField, filterValue, sortBy, isAscending, pageNumber, pageSize);
            }
            else if (user.Roles.Intersect(new List<int>() { 9, 11, 13 }).Count() != 0)
            {
                (organisations, totalItems) = _organisationService.GetOrganisationsByOrg(filterField, filterValue, sortBy, isAscending, pageNumber, pageSize, user);
            }
            else if(user.Roles.Intersect(new List<int>() { 4 }).Count() != 0)
            {
                (organisations, totalItems) = _organisationService.GetOrganisationsForOperatorVetService(filterField, filterValue, sortBy, isAscending, pageNumber, pageSize);
            }
            else if (user.Roles.Intersect(new List<int>() { 10 }).Count() != 0)
            {
                (organisations, totalItems) = _organisationService.GetOrganisationsForOperatorOMSU(filterField, filterValue, sortBy, isAscending, pageNumber, pageSize);
            }
            else
            {
                return Forbid();
            }

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
        public IActionResult AddEntry([FromBody] OrganisationPost organisationPost, int userId)
        {
            if (ModelState.IsValid)
            {
                var organisation = organisationPost.ConvertToOrganisation();
                bool status = _organisationService.AddEntry(organisation);

                if (status)
                {
                    _journalService.JournalAddOrganisation(userId, organisation.OrgId);
                    return Ok(organisation.OrgId);
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
        public IActionResult DeleteEntry(int id, int userId)
        {
            _journalService.JournalDeleteOrganisation(userId, id);
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
        public IActionResult ChangeEntry(int id, [FromBody] OrganisationPost organisationPost, int userId)
        {
            var existingOrganisation = _organisationService.GetEntry(id);

            if (existingOrganisation == null)
            {
                return NotFound();
            }

            existingOrganisation.Update(organisationPost);

            if (ModelState.IsValid)
            {
                bool status = _organisationService.ChangeEntry(existingOrganisation);

                if (status)
                {
                    _journalService.JournalEditOrganisation(userId, id);
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