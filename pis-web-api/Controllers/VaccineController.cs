using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pis_web_api.Models;
using pis_web_api.Repositorys;
using pis_web_api.Services;

namespace pis_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccineController : Controller
    {
        private readonly ILogger<VaccineController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;
        public VaccineController(ILogger<VaccineController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
        }

        [HttpGet("opensRegister")]
        public IActionResult OpensRegister(string filterValue, string sortBy, bool isAscending, string filterField = nameof(Vaccination.VaccineId), int pageNumber = 1, int pageSize = 10)
        {
            filterValue = filterValue?.ToLower();

            var vaccinations = VaccineService.GetVaccines(filterField, filterValue, sortBy, isAscending, pageNumber, pageSize);
            var totalItems = VaccineService.GetTotalVaccines(filterField, filterValue);
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
                Vaccination = vaccinations,
            };

            return Ok(result);
        }

        [HttpGet("GetEntry/{id}")]
        public IActionResult GetEntry(int id)
        {
            var vaccination = VaccineService.GetEntry(id);

            if (vaccination == null)
            {
                return NotFound();
            }

            return Ok(vaccination);
        }

        [HttpGet("addEntry")]
        public IActionResult AddEntry(int animalId)
        {
            var animal = AnimalService.GetEntry(animalId);

            if (animal == null)
            {
                return NotFound();
            }

            Vaccination vaccination = new Vaccination();
            vaccination.VaccineId = VaccineService.GetTotalVaccines(null, null) + 1;
            vaccination.Animal = animal;
            vaccination.VaccinationDate = DateTime.Now.Date;
            vaccination.ValidUntil = DateTime.Now.Date;
            vaccination.Organisation = OrganisationsRepository.GetOrganizations()
                .FirstOrDefault(o => o.Locality == animal.Locality);
            vaccination.Contract = ContractsRepository.GetContracts().FirstOrDefault(c =>
                c.ConclusionDate <= vaccination.VaccinationDate && c.ExpirationDate >= vaccination.VaccinationDate);

            return Ok(vaccination);
        }

        [HttpPost("addEntry")]
        public IActionResult AddEntry([FromBody] Vaccination vaccination)
        {
            bool status = VaccineService.FillData(vaccination);

            if (status)
            {
                return Ok();

            }

            return BadRequest();
        }

        [HttpPost("deleteEntry/{id}")]
        public IActionResult DeleteEntry(int id)
        {
            bool status = VaccineService.DeleteEntry(id);

            if (status)
            {
                Console.WriteLine("Vaccination entry deleted.");
                return Ok();

            }

            return NotFound();
        }

        [HttpPost("changeEntry/{id}")]
        public IActionResult ChangeEntry(int id, [FromBody] Vaccination vaccination)
        {
            bool status = VaccineService.ChangeEntry(vaccination);

            if (status)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
