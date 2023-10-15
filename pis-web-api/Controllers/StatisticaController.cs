using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pis.Services;
using pis.Models;
using pis.Repositorys;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pis.Controllers
{
    public class StatisticaController : Controller
    {
        //// GET: /<controller>/
        //public IActionResult Index()
        //{
        //    return View();
        //}
        //public IActionResult ExportStatisticsToExcel(/*StatisticsItem model*/)
        //{

        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        //    //var statistics = StatisticaService.GetStatistics(model.StartDate, model.EndDate);

        //    var vaccinations = VaccinationRepository.GetVaccinationsByDate(DateTime.Today);

        //    using (var package = new ExcelPackage())
        //    {

        //        var worksheet = package.Workbook.Worksheets.Add("Statistics");

        //        worksheet.Cells[1, 1].Value = "Locality";
        //        worksheet.Cells[1, 2].Value = "Total Vaccines";
        //        worksheet.Cells[1, 3].Value = "Total Cost";


        //        int row = 2;
        //        foreach (var item in vaccinations)
        //        {
        //            worksheet.Cells[row, 1].Value = "Locality";
        //            worksheet.Cells[row, 2].Value = 4;
        //            worksheet.Cells[row, 3].Value = 300;
        //            row++;
        //        }

        //        //Ширина столбцов
        //        worksheet.Cells.AutoFitColumns();

        //        // Сохранить пакет Excel в поток
        //        MemoryStream stream = new MemoryStream();
        //        package.SaveAs(stream);
        //        stream.Position = 0;

        //        string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        string fileName = "Statistics.xlsx";

        //        return File(stream, contentType, fileName);
        //}
        //}
        //public IActionResult Statistica()
        //{
        //    return View();
        //}
    }
}

