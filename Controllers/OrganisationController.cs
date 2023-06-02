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
        public IActionResult OpensRegister()
        {
            var organisations = OrganizationService.GetOrganisations();
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
                return RedirectToAction("OpensRegister");
            }
            else
            {
                return Error();
            }
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
                    return RedirectToAction("OpensRegister");
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
                return RedirectToAction("OpensRegister");
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