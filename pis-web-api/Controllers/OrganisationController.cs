using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class OrganisationController : Controller
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
            if (ModelState.IsValid)
            {
                bool status = OrganisationService.ChangeEntry(organisation);

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

        //// GET: /<controller>/
        //public IActionResult OpensRegister(string filterField, string? filterValue, string sortBy, bool isAscending, int pageNumber = 1, int pageSize = 10)
        //{
        //    filterValue = filterValue?.ToLower();

        //    var organisations = OrganisationService.GetOrganisations(filterField, filterValue, sortBy, isAscending, pageNumber, pageSize);
        //    var totalItems = OrganisationService.GetTotalOrganisations(filterField, filterValue);
        //    var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        //    // if (filterValue != null) ViewBag.FilterName = filterValue;
        //    if (filterValue != null) ViewBag.FilterValue = filterValue;
        //    ViewBag.FilterField = filterField;
        //    ViewBag.SortBy = sortBy;
        //    ViewBag.IsAscending = isAscending;
        //    ViewBag.PageNumber = pageNumber;
        //    ViewBag.PageSize = pageSize;
        //    ViewBag.TotalItems = totalItems;
        //    ViewBag.TotalPages = totalPages;

        //    return View(organisations);
        //}

        //public IActionResult AddEntry()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult AddEntry(Organisation organisation)
        //{
        //    bool status = OrganisationService.FillData(organisation);
        //    if (status)
        //    {
        //        return RedirectToAction("OpensRegister", new { filterField = ViewBag.FilterField, filterValue = ViewBag.FilterValue, sortBy = ViewBag.SortBy, isAscending = ViewBag.IsAscending, pageNumber = ViewBag.PageNumber, pageSize = ViewBag.PageSize });
        //    }
        //    return Error();
        //}

        //[HttpPost]
        //public IActionResult DeleteEntry(int? id)
        //{
        //    if (id != null)
        //    {
        //        var status = OrganisationService.DeleteEntry((int)id);

        //        if (status)
        //        {
        //            Console.WriteLine("Объект Organisation удален.");
        //            return RedirectToAction("OpensRegister", new { filterField = ViewBag.FilterField, filterValue = ViewBag.FilterValue, sortBy = ViewBag.SortBy, isAscending = ViewBag.IsAscending, pageNumber = ViewBag.PageNumber, pageSize = ViewBag.PageSize });
        //        }

        //        return Error();
        //    }

        //    return NotFound();
        //}


        //public async Task<IActionResult> ChangeEntry(int? id)
        //{
        //    if (id != null)
        //    {
        //        var organisation = OrganisationService.GetEntry((int)id);

        //        return View(organisation);
        //    }

        //    return NotFound();
        //}

        //[HttpPost]
        //public async Task<IActionResult> ChangeEntry(Organisation organisation)
        //{
        //    bool status = OrganisationService.ChangeEntry(organisation);
        //    if (status)
        //    {
        //        return RedirectToAction("OpensRegister", new { filterField = ViewBag.FilterField, filterValue = ViewBag.FilterValue, sortBy = ViewBag.SortBy, isAscending = ViewBag.IsAscending, pageNumber = ViewBag.PageNumber, pageSize = ViewBag.PageSize });

        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}


        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}