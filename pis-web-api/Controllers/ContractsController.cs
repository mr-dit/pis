using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pis.Models;
using pis.Services;
using pis.Repositorys;


namespace pis.Controllers
{
    public class ContractsController : Controller
    {
        private readonly ILogger<ContractsController> _logger;

        public ContractsController(ILogger<ContractsController> logger)
        {
            _logger = logger;
        }

        public IActionResult OpensRegister(string filterField, string? filterValue, string sortBy, bool isAscending,
            int pageNumber = 1, int pageSize = 10)
        {
            filterValue = filterValue?.ToLower();

            var contracts =
                ContractsService.GetContracts(filterField, filterValue, sortBy, isAscending, pageNumber, pageSize);
            var totalItems = ContractsService.GetTotalContracts(filterField, filterValue);
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            if (filterValue != null) ViewBag.FilterValue = filterValue;
            ViewBag.FilterField = filterField;
            ViewBag.SortBy = sortBy;
            ViewBag.IsAscending = isAscending;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;
            ViewBag.TotalPages = totalPages;

            // var contracts = ContractsService.GetContracts();
            return View(contracts);
        }

        public IActionResult FillData()
        {
            var organizations = OrganisationsRepository.GetOrganisations(3).ToList();
            ViewBag.Organizations = organizations;

            return View();
        }

        [HttpPost]
        public IActionResult FillData(Contract contracts)
        {
            bool status = ContractsService.CreateContract(contracts);
            if (status)
            {
                return RedirectToAction("OpensRegister",
                    new
                    {
                        filterField = ViewBag.FilterField, filterValue = ViewBag.FilterValue, sortBy = ViewBag.SortBy,
                        isAscending = ViewBag.IsAscending, pageNumber = ViewBag.PageNumber, pageSize = ViewBag.PageSize
                    });
            }

            return Error();
        }


        [HttpPost]
        public IActionResult DeleteEntry(int? id)
        {
            if (id != null)
            {
                var status = ContractsService.DeleteEntry((int)id);

                if (status)
                {
                    Console.WriteLine("Объект Contracts удален.");
                    return RedirectToAction("OpensRegister",
                        new
                        {
                            filterField = ViewBag.FilterField, filterValue = ViewBag.FilterValue,
                            sortBy = ViewBag.SortBy, isAscending = ViewBag.IsAscending, pageNumber = ViewBag.PageNumber,
                            pageSize = ViewBag.PageSize
                        });
                }

                return Error();
            }

            return NotFound();
        }

        public async Task<IActionResult> ChangeEntry(int? id)
        {
            if (id != null)
            {
                var organizations = OrganisationsRepository.GetOrganisations(3);
                ViewBag.Organizations = organizations;
                var newcontracts = ContractsService.GetEntry((int)id);

                return View(newcontracts);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeEntry(Contract contracts)
        {
            bool status = ContractsService.ChangeEntry(contracts);
            if (status)
            {
                return RedirectToAction("OpensRegister",
                    new
                    {
                        filterField = ViewBag.FilterField, filterValue = ViewBag.FilterValue, sortBy = ViewBag.SortBy,
                        isAscending = ViewBag.IsAscending, pageNumber = ViewBag.PageNumber, pageSize = ViewBag.PageSize
                    });
            }

            return NotFound();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}