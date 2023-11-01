using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using pis.Models;
using pis.Repositorys;
using pis.Services;
using pis_web_api.Services;


namespace pis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationController : Controller
    {
        private readonly ILogger<VaccinationController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;
        private VaccinationService _vaccinationService;
        private AnimalService _animalService;
        private VaccineService _vaccineService;
        private ContractService _contractService;
        private UserService _userService;
        private LocalityService _localityService;
        private OrganisationService _organisationService;

        public VaccinationController(ILogger<VaccinationController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            _vaccinationService = new VaccinationService();
            _animalService = new AnimalService();
            _vaccineService = new VaccineService();
            _contractService = new ContractService();
            _userService = new UserService();
            _localityService = new LocalityService();
            _organisationService = new OrganisationService();
        }

        [HttpGet("getVaccinationsByAnimal/{idAnimal}")]
        public IActionResult GetVaccinationsByAnimal(int idAnimal, int pageNumber = 1, int pageSize = 15)
        {
            var (vaccinations, totalItems) = _vaccinationService.GetVaccinationsByAnimal(idAnimal, pageNumber, pageSize);
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var result = new
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Vaccinations = vaccinations
            };

            return Ok(result);
        }

        [HttpPost("add/{animalId}/{vaccineId}/{contractId}/{doctorId}/{vaccineSeries}")]
        public IActionResult AddEntry(int animalId, int vaccineId, int contractId, int doctorId, string vaccineSeries)
        {
            var animal = _animalService.GetEntry(animalId);
            var vaccine = _vaccineService.GetEntry(vaccineId);
            var contract = _contractService.GetEntry(contractId);
            var doctor = _userService.GetEntry(doctorId);

            if(!contract.HasLocality(animal.LocalityId))
                return BadRequest($"Контракт с id[{contractId}] не имеет города с id[{animal.LocalityId}]");
            if(!contract.Performer.HasUser(doctorId))
                return BadRequest($"У исполнителя контракта нет пользователя с id[{doctorId}]");
            if(!doctor.IsDoctor())
                return BadRequest($"Пользователь [{doctorId}] не имеет роли доктора ");

            var vaccination = new Vaccination(vaccineSeries, animal, vaccine, doctor, contract);
            _vaccinationService.AddEntry(vaccination);

            return Ok();
        }
    }

}