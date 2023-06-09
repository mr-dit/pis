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
    public class VaccineController : Controller
    {
        // GET: /<controller>/
        public IActionResult OpensRegister()
        {
            var vaccination = VaccineService.GetOrganisations();
            return View(vaccination);
        }
        
        // public ActionResult AddVaccination(int registrationNumber)
        // {
        //     ViewBag.RegistrationNumber = registrationNumber;
        //     return View();
        // }
        //
        //
        // // Действие для обработки отправки формы добавления вакцинации
        // [HttpPost]
        // public ActionResult AddVaccination(int registrationNumber, Vaccine vaccine)
        // {
        //     // Vaccination vaccination = new Vaccination { Date = date, Type = type };
        //     VaccineService.AddVaccination(registrationNumber, vaccine);
        //
        //     return RedirectToAction("OpensRegister", "Vaccine");  // Перенаправление на другую страницу после добавления вакцинации
        // }

        public IActionResult AddEntry()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEntry(Vaccination vaccination)
        {
            bool status = VaccineService.FillData(vaccination);
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
                var status = VaccineService.DeleteEntry((int)id);

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
                var vaccination = VaccineService.GetEntry((int)id);

                return View(vaccination);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeEntry(Vaccination vaccination)
        {
            bool status = VaccineService.ChangeEntry(vaccination);
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