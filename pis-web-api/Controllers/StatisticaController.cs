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
            _localityService = new LocalityService();
            _contractService = new ContractService();
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
            //return Ok(statisticaHolders);


            //decimal totalVaccines = statisticaHolders.Sum(holder => holder.Sum(item => item.Price));


            using (var package = new ExcelPackage())
            {


                var worksheet = package.Workbook.Worksheets.Add("Vaccination Statistics");

                // Add headers to the Excel sheet
                worksheet.Cells["A1"].Value = "Населенный пункт";
                worksheet.Cells["B1"].Value = "Вакцина";
                worksheet.Cells["C1"].Value = "Цена";
                worksheet.Cells["D1"].Value = "Сумма";

                int row = 2;

                var firstLocality = statisticaHolders.FirstOrDefault();

                if (firstLocality != null)
                {
                    var localityName = firstLocality.LocalityName;
                    foreach (var statisticaItem in firstLocality)
                    {
                        var vaccineName = statisticaItem.VaccineName;
                        var vaccinePrice = statisticaItem.Price;
                        //var totalAmount = statisticaItem.Quantity; // Use the Quantity property for total amount

                        worksheet.Cells[row, 1].Value = localityName;
                        worksheet.Cells[row, 2].Value = vaccineName;
                        worksheet.Cells[row, 3].Value = vaccinePrice;
                        //worksheet.Cells[row, 4].Value = totalAmount;

                        row++;
                    }
                }

                // Calculate the total quantity of vaccines for the first city
                var totalVaccines = firstLocality?.Sum(item => item.Price) ?? 0;
                worksheet.Cells[row, 1].Value = "Total Quantity for the First City";
                worksheet.Cells[row, 4].Value = totalVaccines;

                // Save the Excel package to a stream
                using (var memoryStream = new MemoryStream())
                {
                    package.SaveAs(memoryStream);
                    memoryStream.Position = 0;

                    // Return the Excel file to the client
                    return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "VaccinationData.xlsx");
                }

                //foreach (var statisticaHolder in statisticaHolders)
                //{
                //    var localityName = statisticaHolder.LocalityName;

                //    foreach (var statisticaItem in statisticaHolder)
                //    {
                //        var vaccineName = statisticaItem.VaccineName;
                //        var vaccinePrice = statisticaItem.Price;

                //        worksheet.Cells[$"A{row}"].Value = localityName;
                //        worksheet.Cells[$"B{row}"].Value = vaccineName;
                //        worksheet.Cells[$"C{row}"].Value = vaccinePrice;

                //        row++;
                //    }

                //    // Calculate the total amount of vaccines for the current city
                //    var totalVaccines = statisticaHolder.Sum(item => item.Price);
                //    worksheet.Cells[$"D{row}"].Value = totalVaccines;
                //    row++;
                //}

                //// Auto-fit columns for better readability
                //worksheet.Cells.AutoFitColumns();

                //// Generate a unique file name for the Excel file
                //var fileName = $"VaccinationStatistics_{dateStart:yyyyMMdd}_{dateEnd:yyyyMMdd}.xlsx";

                //// Return the Excel file as a downloadable file
                //var excelBytes = package.GetAsByteArray();
                //return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }


        }

    }


}

