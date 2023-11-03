using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pis.Services;
using pis.Models;
using pis.Repositorys;
using pis_web_api.Services;
using pis_web_api.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticaController : Controller
    {
        private readonly ILogger<StatisticaController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;
        private VaccinationService _vaccinationService;
        private LocalityService _localityService;
        private ContractService _contractService;
        private VaccineService _vaccineService;

        public StatisticaController(ILogger<StatisticaController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            _vaccinationService = new VaccinationService();
            _localityService = new LocalityService();
            _contractService = new ContractService();
            _vaccineService = new VaccineService();
        }

        [HttpGet("{dateStart}/{dateEnd}")]
        public IActionResult GetStatisticaByVaccination(DateOnly dateStart, DateOnly dateEnd)
        {
            
        }
    }
}

