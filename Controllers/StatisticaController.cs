using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using pis.Services;
using pis.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pis.Controllers
{
    public class StatisticaController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ExportStatisticsToExcel(StatisticsItem model)
        {
            // Установите контекст лицензии для EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // Получите данные статистики из VaccineService (вычислите количество потраченных вакцин и общую стоимость услуг с группировкой по населённым пунктам)
            var statistics = StatisticaService.GetStatistics(model.StartDate, model.EndDate);

            // Создайте новый пакет Excel
            using (var package = new ExcelPackage())
            {
                // Добавьте новый лист в пакет
                var worksheet = package.Workbook.Worksheets.Add("Statistics");

                // Запишите заголовки столбцов
                worksheet.Cells[1, 1].Value = "Locality";
                worksheet.Cells[1, 2].Value = "Total Vaccines";
                worksheet.Cells[1, 3].Value = "Total Cost";

                //Запишите данные статистики
                int row = 2;
                foreach (var item in statistics)
                {
                    worksheet.Cells[row, 1].Value = item.Locality;
                    worksheet.Cells[row, 2].Value = item.TotalVaccines;
                    worksheet.Cells[row, 3].Value = item.TotalCost;
                    row++;
                }

                // Автонастройка ширины столбцов
                worksheet.Cells.AutoFitColumns();

                // Сохраните пакет Excel в поток
                MemoryStream stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                // Определите тип контента и имя файла для загрузки
                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string fileName = "Statistics.xlsx";

                // Верните файл Excel для загрузки
                return File(stream, contentType, fileName);
            }
        }
        public IActionResult Statistica()
        {
            return View();
        }
    }
}

