using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pis.Services;
using pis.Repositorys;
using pis_web_api.Services;
using pis_web_api.Models.db;
using OfficeOpenXml;
using Microsoft.Office.Interop.Excel;
using OfficeOpenXml.Style;
using System.Drawing;

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

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Vaccination Statistics");
                int row = 1;
                decimal globalTotal = 0;
                foreach (var statisticaHolder in statisticaHolders)
                {
                    decimal total = 0;
                    worksheet.Cells[row, 1].Value = statisticaHolder.LocalityName;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 1].Style.Font.Size += 6;
                    row++;
                    worksheet.Cells[row, 1].Value = "Вакцина";
                    worksheet.Cells[row, 1].Style.Font.Bold = true;
                    worksheet.Cells[row, 2].Value = "Цена";
                    worksheet.Cells[row, 2].Style.Font.Bold = true;
                    row++;
                    foreach (var statisticaItem in statisticaHolder)
                    {
                        worksheet.Cells[row, 1].Value = statisticaItem.VaccineName;
                        worksheet.Cells[row, 2].Value = statisticaItem.Price;
                        total += statisticaItem.Price;
                        row++;
                    }
                    globalTotal += total;
                    worksheet.Cells[row, 1].Value = "Итого:";
                    worksheet.Cells[row, 1].Style.Font.Color.SetColor(Color.Red);
                    worksheet.Cells[row, 2].Value = total;
                    worksheet.Cells[row, 2].Style.Font.Color.SetColor(Color.Red);
                    row += 2;
                }
                worksheet.Cells[row+1, 1].Value = "Итого за все города:";
                worksheet.Cells[row + 1, 2].Value = globalTotal;
                worksheet.Columns[1].AutoFit();

                using (var memoryStream = new MemoryStream())
                {
                    package.SaveAs(memoryStream);
                    memoryStream.Position = 0;

                    return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "VaccinationData.xlsx");
                }
            }
        }

    }


}

