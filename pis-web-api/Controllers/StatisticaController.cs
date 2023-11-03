using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pis.Services;
using pis.Models;
using pis.Repositorys;
using pis_web_api.Services;

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

        public StatisticaController(ILogger<StatisticaController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            _vaccinationService = new VaccinationService();
        }

        [HttpGet("{dateStart}/{dateEnd}")]
        public IActionResult GetStatisticaByVaccination(DateOnly dateStart, DateOnly dateEnd)
        {
            var vaccination = _vaccinationService.GetVaccinationsByDate(dateStart, dateEnd);
            var a = vaccination.GroupBy(x => x.Animal.LocalityId);

            if (vaccination == null)
            {
                return NotFound();
            }



            return Ok(vaccination);

        }


    }


}

