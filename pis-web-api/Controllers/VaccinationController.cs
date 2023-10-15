using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pis.Models;
using pis.Repositorys;
using pis.Services;


namespace pis.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class VaccinationController : Controller
    //{
    //    private readonly ILogger<VaccinationController> _logger;
    //    private readonly IWebHostEnvironment _appEnvironment;
    //    public VaccinationController(ILogger<VaccinationController> logger, IWebHostEnvironment appEnvironment)
    //    {
    //        _logger = logger;
    //        _appEnvironment = appEnvironment;
    //    }

    //    [HttpGet("opensRegister")]
    //    public IActionResult OpensRegister(string filterValue, string sortBy, bool isAscending, string filterField = nameof(Vaccination.IdVactination), int pageNumber = 1, int pageSize = 10)        
    //    {
    //        filterValue = filterValue?.ToLower();

    //        var vaccinations = VaccinationService.GetVaccinations(filterField, filterValue, sortBy, isAscending, pageNumber, pageSize);
    //        var totalItems = VaccinationService.GetTotalVaccines(filterField, filterValue);
    //        var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

    //        var result = new
    //        {
    //            FilterValue = filterValue,
    //            FilterField = filterField,
    //            SortBy = sortBy,
    //            IsAscending = isAscending,
    //            PageNumber = pageNumber,
    //            PageSize = pageSize,
    //            TotalItems = totalItems,
    //            TotalPages = totalPages,
    //            Vaccination = vaccinations,
    //        };

    //        return Ok(result);
    //    }

    //    [HttpGet("GetEntry/{id}")]
    //    public IActionResult GetEntry(int id)
    //    {
    //        var vaccination = VaccinationService.GetEntry(id);

    //        if (vaccination == null)
    //        {
    //            return NotFound();
    //        }

    //        return Ok(vaccination);
    //    }

    //    [HttpGet("addEntry")]
    //    public IActionResult AddEntry(int animalId)
    //    {
    //        var animal = AnimalService.GetEntry(animalId);

    //        if (animal == null)
    //        {
    //            return NotFound();
    //        }

    //        Vaccination vaccination = new Vaccination();
    //        vaccination.VaccineId = VaccineService.GetTotalVaccines(null, null) + 1;
    //        vaccination.Animal = animal;
    //        vaccination.VaccinationDate = DateTime.Now.Date;
    //        vaccination.ValidUntil = DateTime.Now.Date;
    //        vaccination.Organisation = OrganisationsRepository.GetOrganizations()
    //            .FirstOrDefault(o => o.Locality == animal.Locality);
    //        vaccination.Contract = ContractsRepository.GetContracts().FirstOrDefault(c =>
    //            c.ConclusionDate <= vaccination.VaccinationDate && c.ExpirationDate >= vaccination.VaccinationDate);

    //        return Ok(vaccination);
    //    }

    //    [HttpPost("addEntry")]
    //    public IActionResult AddEntry([FromBody] Vaccination vaccination)
    //    {
    //        bool status = VaccineService.FillData(vaccination);

    //        if (status)
    //        {
    //            return Ok();

    //        }

    //        return BadRequest();
    //    }

    //    [HttpPost("deleteEntry/{id}")]
    //    public IActionResult DeleteEntry(int id)
    //    {
    //        bool status = VaccineService.DeleteEntry(id);

    //        if (status)
    //        {
    //            Console.WriteLine("Vaccination entry deleted.");
    //            return Ok();

    //        }

    //        return NotFound();
    //    }

    //    [HttpPost("changeEntry/{id}")]
    //    public IActionResult ChangeEntry(int id, [FromBody] Vaccination vaccination)
    //    {
    //        bool status = VaccineService.ChangeEntry(vaccination);

    //        if (status)
    //        {
    //            return Ok();
    //        }

    //        return NotFound();
    //    }
        //// GET: /<controller>/
        //public IActionResult OpensRegister(string filterField, string? filterValue, string sortBy, bool isAscending,
        //    int pageNumber = 1, int pageSize = 10)
        //{
        //    filterValue = filterValue?.ToLower();

        //    var vaccination =
        //        VaccinationService.GetVaccinations(filterField, filterValue, sortBy, isAscending, pageNumber, pageSize);
        //    var totalItems = VaccinationService.GetTotalVaccines(filterField, filterValue);
        //    var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        //    if (filterValue != null) ViewBag.FilterValue = filterValue;
        //    ViewBag.FilterField = filterField;
        //    ViewBag.SortBy = sortBy;
        //    ViewBag.IsAscending = isAscending;
        //    ViewBag.PageNumber = pageNumber;
        //    ViewBag.PageSize = pageSize;
        //    ViewBag.TotalItems = totalItems;
        //    ViewBag.TotalPages = totalPages;

        //    return View(vaccination);
        //}

        ////public IActionResult ExportStatisticsToExcel(string filterField, string? filterValue)
        ////{
        ////    // Установите контекст лицензии для EPPlus
        ////    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        ////    // Получите данные статистики из VaccineService (вычислите количество потраченных вакцин и общую стоимость услуг с группировкой по населённым пунктам)
        ////    var statistics = VaccineService.GetStatistics(filterField, filterValue);

        ////    // Создайте новый пакет Excel
        ////    using (var package = new ExcelPackage())
        ////    {
        ////        // Добавьте новый лист в пакет
        ////        var worksheet = package.Workbook.Worksheets.Add("Statistics");

        ////        // Запишите заголовки столбцов
        ////        worksheet.Cells[1, 1].Value = "Locality";
        ////        worksheet.Cells[1, 2].Value = "Total Vaccines";
        ////        worksheet.Cells[1, 3].Value = "Total Cost";

        ////        //Запишите данные статистики
        ////        int row = 2;
        ////        foreach (var item in statistics)
        ////        {
        ////            worksheet.Cells[row, 1].Value = item.Locality;
        ////            worksheet.Cells[row, 2].Value = item.TotalVaccines;
        ////            worksheet.Cells[row, 3].Value = item.TotalCost;
        ////            row++;
        ////        }

        ////        // Автонастройка ширины столбцов
        ////        worksheet.Cells.AutoFitColumns();

        ////        // Сохраните пакет Excel в поток
        ////        MemoryStream stream = new MemoryStream();
        ////        package.SaveAs(stream);
        ////        stream.Position = 0;

        ////        // Определите тип контента и имя файла для загрузки
        ////        string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        ////        string fileName = "Statistics.xlsx";

        ////        // Верните файл Excel для загрузки
        ////        return File(stream, contentType, fileName);
        ////    }
        ////}

        //public IActionResult AddEntry(Animal animal)
        //{
        //    Vaccination vaccination = new Vaccination();
        //    vaccination.IdVactination = VaccinationService.GetTotalVaccines(null, null) + 1;
        //    vaccination.Animal = animal;
        //    vaccination.VaccinationDate = DateTime.Now.Date;
        //    vaccination.Doctor = new User();
        //    vaccination.Contract = ContractsRepository.Get GetContracts().FirstOrDefault(c =>
        //        c.ConclusionDate <= vaccination.VaccinationDate && c.ExpirationDate >= vaccination.VaccinationDate);

        //    return View(vaccination);
        //}

        //[HttpPost]
        //public IActionResult AddEntry(Vaccination vaccination)
        //{
        //    bool status = VaccineService.FillData(vaccination);
        //    if (status)
        //    {
        //        return RedirectToAction("OpensRegister",
        //            new
        //            {
        //                filterField = ViewBag.FilterField, filterValue = ViewBag.FilterValue, sortBy = ViewBag.SortBy,
        //                isAscending = ViewBag.IsAscending, pageNumber = ViewBag.PageNumber, pageSize = ViewBag.PageSize
        //            });
        //    }

        //    return Error();
        //}

        //[HttpPost]
        //public IActionResult DeleteEntry(int? id)
        //{
        //    if (id != null)
        //    {
        //        var status = VaccineService.DeleteEntry((int)id);
        //        if (status)
        //        {
        //            Console.WriteLine("Объект Organisation удален.");
        //            return RedirectToAction("OpensRegister",
        //                new
        //                {
        //                    filterField = ViewBag.FilterField, filterValue = ViewBag.FilterValue,
        //                    sortBy = ViewBag.SortBy, isAscending = ViewBag.IsAscending, pageNumber = ViewBag.PageNumber,
        //                    pageSize = ViewBag.PageSize
        //                });
        //        }

        //        return Error();
        //    }

        //    return NotFound();
        //}


        //public async Task<IActionResult> ChangeEntry(int? id)
        //{
        //    if (id != null)
        //    {
        //        var vaccination = VaccineService.GetEntry((int)id);

        //        return View(vaccination);
        //    }

        //    return NotFound();
        //}

        //[HttpPost]
        //public async Task<IActionResult> ChangeEntry(Vaccination vaccination)
        //{
        //    bool status = VaccineService.ChangeEntry(vaccination);
        //    if (status)
        //    {
        //        return RedirectToAction("OpensRegister",
        //            new
        //            {
        //                filterField = ViewBag.FilterField, filterValue = ViewBag.FilterValue, sortBy = ViewBag.SortBy,
        //                isAscending = ViewBag.IsAscending, pageNumber = ViewBag.PageNumber, pageSize = ViewBag.PageSize
        //            });
        //    }

        //    return NotFound();
        //}


        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    
}