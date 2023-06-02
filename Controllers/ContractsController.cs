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

        public IActionResult OpensRegister()
        {
            var contracts = ContractsService.GetContracts();
            return View(contracts);
        }

        public IActionResult FillData()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FillData(Contracts contracts)
        {
            bool status = ContractsService.CreateContract(contracts);
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
                var status = ContractsService.DeleteEntry((int)id);

                if (status)
                {
                    Console.WriteLine("Объект Contracts удален.");
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
                var newcontracts = ContractsService.GetEntry((int)id);

                return View(newcontracts);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeEntry(Contracts contracts)
        {
            bool status = ContractsService.ChangeEntry(contracts);
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

