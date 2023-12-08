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
using pis_web_api.Models.post;
using System.IO;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;
using pis_web_api.Models.get;

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
        private ReportService _reportService;

        public StatisticaController(ILogger<StatisticaController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            _vaccinationService = new VaccinationService();
            _contractService = new ContractService();
            _localityService = new LocalityService();
            _vaccineService = new VaccineService();
            _reportService = new ReportService();
        }

        [HttpPost("openRegister")]
        public IActionResult OpenRegister([FromBody] UserPost user, DateOnly startDateFilter, DateOnly endDateFilter, string filterField = "", string filterValue = "", string sortBy = nameof(Report.Id), bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            if (startDateFilter == DateOnly.MinValue && endDateFilter == DateOnly.MinValue)
                endDateFilter = DateOnly.MaxValue;

            List<Report> reports;
            int totalItems;

            if (user.Roles.Intersect(new List<int>() { 9, 10, 11, 15 }).Count() != 0)
            {
                (reports, totalItems) = _reportService.GetAllReports(startDateFilter, endDateFilter, filterField,
                    filterValue, sortBy, isAscending, pageNumber, pageSize);
            }
            else if (user.Roles.Intersect(new List<int>() { 1, 4, 6, 13, 14, 15 }).Count() != 0)
            {
                (reports, totalItems) = _reportService.GetReportsByOrg(startDateFilter, endDateFilter, filterField,
                    filterValue, sortBy, isAscending, pageNumber, pageSize, user.OrganisationId);
            }
            else
            {
                return Forbid();
            }

            var reportsGet = new List<ReportGet>();
            foreach (var report in reports)
            {
                reportsGet.Add(new ReportGet(report));
            }
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var result = new
            {
                FilterValue = filterValue,
                FilterField = filterField,
                SortBy = sortBy,
                IsAscending = isAscending,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Reports = reportsGet
            };
            return Ok(result);
        }

        [HttpPost("createReport/{dateStart}/{dateEnd}/{orgId}")]
        public IActionResult CreateReport([FromBody] UserPost user, DateOnly dateStart, DateOnly dateEnd, int orgId)
        {
            if (user.Roles.Intersect(new List<int>() { 9, 10, 11, 15 }).Count() != 0)
            {
                var statisticaHolders = _reportService.GetReportItems(dateStart, dateEnd, orgId);
                var report = new Report(statisticaHolders, dateStart, dateEnd, orgId);
                _reportService.AddEntry(report);
                return Ok();
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPost("getReportAsFile/{id}")]
        public IActionResult GetReportAsFile([FromBody] UserPost user, int id)
        {
            var report = _reportService.GetReport(id);
            var convertor = new ReportConverterToExcel();
            var file = convertor.ConvertToExcel(report);
            return File(file, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"{id}-{report.DateStart}-{report.DateEnd}-org{report.PerformerId}.xlsx");
        }

        [HttpPost("confirm/{id}")]
        public IActionResult Confirm([FromBody] UserPost user, int id)
        {
            var report = _reportService.GetReport(id);
            if (report.Status == ReportStatus.Черновик)
            {
                if (user.Roles.Intersect(new List<int>() { 9, 10, 11, 15 }).Count() != 0)
                {
                    report.ChangeStatusToSoglasovanieUPodpisanta();
                    _reportService.ChangeEntry(report);
                    return Ok();
                }
                else
                {
                    return Forbid();
                }
            }
            else if (report.Status == ReportStatus.Согласование_у_исполнителя)
            {
                if (user.Roles.Intersect(new List<int>() { 1, 4, 6, 13, 14, 15 }).Count() != 0)
                {
                    report.ChangeStatusToSoglasovanUPodpisanta();
                    _reportService.ChangeEntry(report);
                    return Ok();
                }
                else
                {
                    return Forbid();
                }
            }
            else if (report.Status == ReportStatus.Согласован_у_исполнителя)
            {
                if (user.Roles.Intersect(new List<int>() { 9, 10, 11, 15 }).Count() != 0)
                {
                    report.ChangeStatusToSoglasovanUOMSU();
                    _reportService.ChangeEntry(report);
                    return Ok();
                }
                else
                {
                    return Forbid();
                }
            }
            else if (report.Status == ReportStatus.Согласован_в_ОМСУ)
            {
                return BadRequest("Невозможно подтвердить со статусом <Согласован в ОМСУ>");
            }
            return BadRequest("Некорректный статус");
        }

        [HttpPost("cancel/{id}")]
        public IActionResult Cancel([FromBody] UserPost user, int id)
        {
            var report = _reportService.GetReport(id);
            if (report.Status == ReportStatus.Согласован_в_ОМСУ)
            {
                return BadRequest("Невозможно отменить со статусом <Согласован в ОМСУ>");
            }
            if (report.Status == ReportStatus.Черновик)
            {
                if (user.Roles.Intersect(new List<int>() { 9, 10, 11, 15 }).Count() != 0)
                {
                    _reportService.DeleteEntry(id);
                    return Ok();
                }
                else
                {
                    return Forbid();
                }
            }
            else if (report.Status == ReportStatus.Согласование_у_исполнителя)
            {
                if (user.Roles.Intersect(new List<int>() { 1, 4, 6, 13, 14, 15 }).Count() != 0)
                {
                    report.ChangeStatusToDorabotka();
                    _reportService.ChangeEntry(report);
                    return Ok();
                }
                else
                {
                    return Forbid();
                }
            }
            else if (report.Status == ReportStatus.Согласован_у_исполнителя)
            {
                if (user.Roles.Intersect(new List<int>() { 9, 10, 11, 15 }).Count() != 0)
                {
                    report.ChangeStatusToDorabotka();
                    _reportService.ChangeEntry(report);
                    return Ok();
                }
                else
                {
                    return Forbid();
                }
            }
            return BadRequest("Некорректный статус");
        }

        [HttpPost("getCountDorabotka")]
        public IActionResult GetCountDorabotka([FromBody] UserPost user)
        {
            return Ok(_reportService.GetCountDorabotka(user.OrganisationId));
        }

        [HttpPost("recalculateReport/{id}")]
        public IActionResult RecalculateReport([FromBody] UserPost user, int id)
        {
            var report = _reportService.GetReport(id);
            if (user.Roles.Intersect(new List<int>() { 9, 10, 11, 15 }).Count() != 0)
            {
                _reportService.DeleteStatistica(report);
                var statisticaHolders = _reportService.GetReportItems(report.DateStart, report.DateEnd, report.PerformerId);
                report.Update(statisticaHolders);
                _reportService.ChangeEntry(report);
                return Ok();
            }
            else
            {
                return Forbid();
            }
        }
    }
}

