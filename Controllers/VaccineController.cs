using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using pis.Models;
using pis.Repositorys;
using pis.Services;


namespace pis.Controllers
{
    public class VaccineController : Controller
    {
        // GET: /<controller>/
        public IActionResult OpensRegister(string filterField, string? filterValue, string sortBy, bool isAscending,
            int pageNumber = 1, int pageSize = 10)
        {
            filterValue = filterValue?.ToLower();

            var vaccination =
                VaccineService.GetVaccines(filterField, filterValue, sortBy, isAscending, pageNumber, pageSize);
            var totalItems = VaccineService.GetTotalVaccines(filterField, filterValue);
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            if (filterValue != null) ViewBag.FilterValue = filterValue;
            ViewBag.FilterField = filterField;
            ViewBag.SortBy = sortBy;
            ViewBag.IsAscending = isAscending;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;
            ViewBag.TotalPages = totalPages;

            return View(vaccination);
        }

        //public IActionResult ExportStatisticsToExcel(string filterField, string? filterValue)
        //{
        //    // Установите контекст лицензии для EPPlus
        //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        //    // Получите данные статистики из VaccineService (вычислите количество потраченных вакцин и общую стоимость услуг с группировкой по населённым пунктам)
        //    var statistics = VaccineService.GetStatistics(filterField, filterValue);

        //    // Создайте новый пакет Excel
        //    using (var package = new ExcelPackage())
        //    {
        //        // Добавьте новый лист в пакет
        //        var worksheet = package.Workbook.Worksheets.Add("Statistics");

        //        // Запишите заголовки столбцов
        //        worksheet.Cells[1, 1].Value = "Locality";
        //        worksheet.Cells[1, 2].Value = "Total Vaccines";
        //        worksheet.Cells[1, 3].Value = "Total Cost";

        //        //Запишите данные статистики
        //        int row = 2;
        //        foreach (var item in statistics)
        //        {
        //            worksheet.Cells[row, 1].Value = item.Locality;
        //            worksheet.Cells[row, 2].Value = item.TotalVaccines;
        //            worksheet.Cells[row, 3].Value = item.TotalCost;
        //            row++;
        //        }

        //        // Автонастройка ширины столбцов
        //        worksheet.Cells.AutoFitColumns();

        //        // Сохраните пакет Excel в поток
        //        MemoryStream stream = new MemoryStream();
        //        package.SaveAs(stream);
        //        stream.Position = 0;

        //        // Определите тип контента и имя файла для загрузки
        //        string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        string fileName = "Statistics.xlsx";

        //        // Верните файл Excel для загрузки
        //        return File(stream, contentType, fileName);
        //    }
        //}

        public IActionResult AddEntry(Animal animal)
        {
            Vaccination vaccination = new Vaccination();
            vaccination.VaccineId = VaccineService.GetTotalVaccines(null, null) + 1;
            vaccination.Animal = animal;
            vaccination.VaccinationDate = DateTime.Now.Date;
            vaccination.ValidUntil = DateTime.Now.Date;
            vaccination.Organisation = OrganisationsRepository.GetOrganizations()
                .FirstOrDefault(o => o.Locality == animal.Locality);
            vaccination.Contract = ContractsRepository.GetContracts().FirstOrDefault(c =>
                c.ConclusionDate <= vaccination.VaccinationDate && c.ExpirationDate >= vaccination.VaccinationDate);

            return View(vaccination);
        }

        [HttpPost]
        public IActionResult AddEntry(Vaccination vaccination)
        {
            bool status = VaccineService.FillData(vaccination);
            if (status)
            {
                return RedirectToAction("OpensRegister",
                    new
                    {
                        filterField = ViewBag.FilterField, filterValue = ViewBag.FilterValue, sortBy = ViewBag.SortBy,
                        isAscending = ViewBag.IsAscending, pageNumber = ViewBag.PageNumber, pageSize = ViewBag.PageSize
                    });
            }

            return Error();
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
                    return RedirectToAction("OpensRegister",
                        new
                        {
                            filterField = ViewBag.FilterField, filterValue = ViewBag.FilterValue,
                            sortBy = ViewBag.SortBy, isAscending = ViewBag.IsAscending, pageNumber = ViewBag.PageNumber,
                            pageSize = ViewBag.PageSize
                        });
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
                return RedirectToAction("OpensRegister",
                    new
                    {
                        filterField = ViewBag.FilterField, filterValue = ViewBag.FilterValue, sortBy = ViewBag.SortBy,
                        isAscending = ViewBag.IsAscending, pageNumber = ViewBag.PageNumber, pageSize = ViewBag.PageSize
                    });
            }

            return NotFound();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}