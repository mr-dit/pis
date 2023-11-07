using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pis.Services;
using pis.Repositorys;
using pis_web_api.Services;
using pis_web_api.Models;
using pis_web_api.Models.db;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

[Route("api/[controller]")]
[ApiController]
public class StatisticaController : Controller
{

    private readonly ILogger<StatisticaController> _logger;
    private readonly IWebHostEnvironment _appEnvironment;
    private VaccinationService _vaccinationService;
    private ContractService _contractService;
    private LocalityService _localityService;
    private VaccineService _vaccineService;

    public StatisticaController(ILogger<StatisticaController> logger, IWebHostEnvironment appEnvironment)
    {
        _logger = logger;
        _appEnvironment = appEnvironment;
        _vaccinationService = new VaccinationService();
        _contractService = new ContractService();
        _localityService = new LocalityService();
        _vaccineService = new VaccineService();
    }

    [HttpGet("{dateStart}/{dateEnd}")]
    public IActionResult GetStatisticaByVaccination(DateOnly dateStart, DateOnly dateEnd)
    {
        //группировка по городам
        //группируется по id города, а не по самому городу
        var vaccinationsGroupedByLocality = _vaccinationService
            .GetVaccinationsByDate(dateStart, dateEnd)
            .GroupBy(x => x.Animal.LocalityId);

        //тут хранится вся статистика
        var statisticaHolders = new List<StatisticaHolder>();

        //группировка создана в виде (id города, вакцинация)
        //поочередно проходимся по каждому id городу
        foreach (var localities in vaccinationsGroupedByLocality)
        {
            //по id города находим город в БД
            var locality = _localityService.GetEntry(localities.Key);
            //тут хранятся все пары(имя вакцины-цена)
            var statisticaHolder = new StatisticaHolder(locality);
            //пробегаемся по всем вакцинациям по данному городу
            foreach (var vaccination in localities)
            {
                //в бд находим контракт, по которому была проведена вакцинация
                var contract = _contractService.GetEntry(vaccination.ContractId);
                //из контракта берем цену по населенному пункту
                var price = contract.GetPriceByLocality(locality);
                //в бд находим вакцину, которая была поставлена
                var vaccine = _vaccineService.GetEntry(vaccination.VaccineId);
                //создаем пару(вакцина-цена)
                statisticaHolder.AddVaccinePrice(vaccine, price);
            }
            //добавляем все наши пары в общую статистику
            statisticaHolders.Add(statisticaHolder);
        }
        // теперь можно обращаться к данным по типу:
        foreach (var statisticaHolder in statisticaHolders)
        {
            var localityName = statisticaHolder.LocalityName;
            foreach (var statisticaItem in statisticaHolder)
            {
                var vaccineName = statisticaItem.VaccineName;
                var vaccinePrice = statisticaItem.Price;
            }
        }
        // и с помощью этого надо сделать все в экселе, эксель не люблю
        return Ok(statisticaHolders);


        //decimal totalVaccines = statisticaHolders.Sum(holder => holder.Sum(item => item.Price));


        //using (var package = new ExcelPackage())
        //{

        //    var worksheet = package.Workbook.Worksheets.Add("VaccineStatistics");

        //    worksheet.Cells["A1"].Value = "City";
        //    worksheet.Cells["B1"].Value = "Vaccine";
        //    worksheet.Cells["C1"].Value = "Price";

        //    int row = 2;

        //    foreach (var statisticaHolder in statisticaHolders)
        //    {
        //        string localityName = statisticaHolder.LocalityName;
        //        foreach (var statisticaItem in statisticaHolder)
        //        {
        //            string vaccineName = statisticaItem.VaccineName;
        //            decimal vaccinePrice = statisticaItem.Price;

        
//            worksheet.Cells["A" + row].Value = localityName;
            //            worksheet.Cells["B" + row].Value = vaccineName;
            //            worksheet.Cells["C" + row].Value = vaccinePrice;
            //            row++;
            //        }
            //    }

            //    worksheet.Cells["A" + row].Value = "Total Vaccines";
            //    worksheet.Cells["C" + row].Value = totalVaccines;

            //    var excelBytes = package.GetAsByteArray();

            //    return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "VaccineStatistics.xlsx");
            //}


        }

}

