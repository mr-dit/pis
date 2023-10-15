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
    public class OrganisationController : Controller
    {
        // GET: /<controller>/
        public IActionResult OpensRegister(string filterField, string? filterValue, string sortBy, bool isAscending, int pageNumber = 1, int pageSize = 10)
        {
            filterValue = filterValue?.ToLower();
            
            var organisations = OrganizationService.GetOrganisations(filterField, filterValue, sortBy, isAscending, pageNumber, pageSize);
            var totalItems = OrganizationService.GetTotalOrganisations(filterField, filterValue);
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // if (filterValue != null) ViewBag.FilterName = filterValue;
            if (filterValue != null) ViewBag.FilterValue = filterValue;
            ViewBag.FilterField = filterField;
            ViewBag.SortBy = sortBy;
            ViewBag.IsAscending = isAscending;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;
            ViewBag.TotalPages = totalPages;

            return View(organisations);
        }

        public IActionResult AddEntry()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEntry(Organisation organisation)
        {
            bool status = OrganizationService.FillData(organisation);
            if (status)
            {
                return RedirectToAction("OpensRegister", new { filterField = ViewBag.FilterField, filterValue = ViewBag.FilterValue, sortBy = ViewBag.SortBy, isAscending = ViewBag.IsAscending, pageNumber = ViewBag.PageNumber, pageSize = ViewBag.PageSize });
            }
            return Error();
        }

        [HttpPost]
        public IActionResult DeleteEntry(int? id)
        {
            if (id != null)
            {
                var status = OrganizationService.DeleteEntry((int)id);

                if (status)
                {
                    Console.WriteLine("Объект Organisation удален.");
                    return RedirectToAction("OpensRegister", new { filterField = ViewBag.FilterField, filterValue = ViewBag.FilterValue, sortBy = ViewBag.SortBy, isAscending = ViewBag.IsAscending, pageNumber = ViewBag.PageNumber, pageSize = ViewBag.PageSize });
                }

                return Error();
            }

            return NotFound();
        }


        public async Task<IActionResult> ChangeEntry(int? id)
        {
            if (id != null)
            {
                var organisation = OrganizationService.GetEntry((int)id);

                return View(organisation);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeEntry(Organisation organisation)
        {
            bool status = OrganizationService.ChangeEntry(organisation);
            if (status)
            {
                return RedirectToAction("OpensRegister", new { filterField = ViewBag.FilterField, filterValue = ViewBag.FilterValue, sortBy = ViewBag.SortBy, isAscending = ViewBag.IsAscending, pageNumber = ViewBag.PageNumber, pageSize = ViewBag.PageSize });

            }
            else
            {
                return NotFound();
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}