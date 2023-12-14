using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using OfficeOpenXml;
using pis.Services;
using pis_web_api.Models.db;
using pis_web_api.Repositorys;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace pis_web_api.Services
{
    public class ReportService : Service<Report>
    {
        private Dictionary<string, Func<Report, string, bool>> filter = new Dictionary<string, Func<Report, string, bool>>()
        {
            [""] = (report, filter) => { return true; },
            ["Performer"] = (report, filter) => { return report.Performer.OrgName.Contains(filter); },
        };
        private Repository<Report> _repostitoryReport;
        private VaccinationService _vaccinationService;
        private ContractService _contractService;
        private LocalityService _localityService;
        private VaccineService _vaccineService;

        public ReportService() 
        {
            _repository = new Repository<Report>();
            _vaccinationService = new VaccinationService();
            _contractService = new ContractService();
            _localityService = new LocalityService();
            _vaccineService = new VaccineService();
            _repostitoryReport = new Repository<Report>();
        }

        private (List<Report>, int) GetByValue(Func<Report, bool> value, DateOnly startDate, DateOnly endDate, Func<Report, string, bool> filter, string filterValue,
            string sortBy, bool isAscending, int pageNumber, int pageSize)
        {
            var reports = _repostitoryReport.dbSet
                .Include(x => x.Performer)
                .Where(x => x.DateCreate >= startDate.ToDateTime(new TimeOnly()))
                .Where(x => x.DateCreate <= endDate.ToDateTime(new TimeOnly()))
                .Where(value)
                .Where(x => filter.Invoke(x, filterValue))
                .SortBy(sortBy, isAscending);
            
            var count = reports.Count();
            var reports_list = reports.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return (reports_list, count);
        }

        public (List<Report>, int) GetAllReports(DateOnly dateStart, DateOnly dateEnd, string filterField, string filterValue, 
            string sortBy, bool isAscending, int pageNumber, int pageSize)
        {
            var filterMethod = filter[filterField];
            var (reports, count) = GetByValue(report => { return true; }, dateStart, dateEnd, filterMethod, 
                filterValue, sortBy, isAscending, pageNumber, pageSize);
            return (reports, count);
        }

        public (List<Report>, int) GetReportsByOrg(DateOnly dateStart, DateOnly dateEnd, string filterField, string filterValue,
            string sortBy, bool isAscending, int pageNumber, int pageSize, int orgId)
        {
            var filterMethod = filter[filterField];
            var (reports, count) = GetByValue(report => { return report.PerformerId == orgId; }, dateStart, dateEnd, filterMethod,
                filterValue, sortBy, isAscending, pageNumber, pageSize);
            return (reports, count);
        }

        public List<StatisticaHolder> GetReportItems(DateOnly dateStart, DateOnly dateEnd, int orgId)
        {
            var basePath = $"./Reports";
            var baseFileName = $"{dateStart}-{dateEnd}-org_{orgId}.xlsx";
            string uniqueFileName = GetUniqueFileName(basePath, baseFileName);

            // Создание объекта FileInfo с уникальным именем
            var fileInfo = new FileInfo(Path.Combine(basePath, uniqueFileName));

            //группировка по городам
            //группируется по id города, а не по самому городу
            var vaccinationsGroupedByLocality = _vaccinationService
                .GetVaccinationsByDate(dateStart, dateEnd, orgId)
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
            var report = new Report(statisticaHolders, dateStart, dateEnd, orgId);
            return statisticaHolders;
        }

        public Report GetReport(DateOnly dateStart, DateOnly dateEnd, int orgId)
        {
            var statisticaHolders = GetReportItems(dateStart, dateEnd, orgId);
            var report = new Report(statisticaHolders, dateStart, dateEnd, orgId);
            return report;
        }

        private static string GetUniqueFileName(string basePath, string baseFileName)
        {
            string fileName = baseFileName;
            int count = 1;

            // Пока файл с таким именем уже существует, добавляем суффикс
            while (File.Exists(Path.Combine(basePath, fileName)))
            {
                fileName = $"{Path.GetFileNameWithoutExtension(baseFileName)}_{count}{Path.GetExtension(baseFileName)}";
                count++;
            }

            return fileName;
        }

        public Report GetReport(int id)
        {
            var report = _repostitoryReport.dbSet
                .Include(x => x.StatisticaHolders)
                    .ThenInclude(x => x.VaccinePrice)
                .Where(x => x.Id == id)
                .Single();
            return report;
        }

        public int GetCountDorabotka(int id)
        {
            var count = _repostitoryReport.db.Reports
                .Where(x => x.StatusName == "Доработка")
                .Count();
            return count;
        }

        public void DeleteStatistica(Report report)
        {
            _repostitoryReport.db.StatisticaHolder
                .RemoveRange(report.StatisticaHolders);
            _repostitoryReport.db.SaveChanges();
        }
    }

    static class SortingExtension
    {
        public static IEnumerable<Report> SortBy(this IEnumerable<Report> animals, string sortBy, bool isAscending)
        {
            var sortingFields = new Dictionary<string, Func<IEnumerable<Report>, bool, IOrderedEnumerable<Report>>>(StringComparer.InvariantCultureIgnoreCase)
            {
                [nameof(Report.Status)] = (animals, isAscending) =>
                    isAscending ? animals.OrderBy(a => a.Status)
                    : animals.OrderByDescending(a => a.Status),

                [nameof(Report.StatusUpdate)] = (animals, isAscending) =>
                    isAscending ? animals.OrderBy(a => a.StatusUpdate)
                    : animals.OrderByDescending(a => a.StatusUpdate),

                [nameof(Report.DateCreate)] = (animals, isAscending) =>
                    isAscending ? animals.OrderBy(a => a.DateCreate)
                    : animals.OrderByDescending(a => a.DateCreate),

                [nameof(Report.DateStart)] = (animals, isAscending) =>
                    isAscending ? animals.OrderBy(a => a.DateStart)
                    : animals.OrderByDescending(a => a.DateStart),

                [nameof(Report.DateEnd)] = (animals, isAscending) =>
                    isAscending ? animals.OrderBy(a => a.DateEnd)
                    : animals.OrderByDescending(a => a.DateEnd),

                [nameof(Report.Performer)] = (animals, isAscending) =>
                    isAscending ? animals.OrderBy(a => a.Performer.OrgName)
                    : animals.OrderByDescending(a => a.Performer.OrgName),

                [nameof(Report.Id)] = (animals, isAscending) =>
                    isAscending ? animals.OrderBy(a => a.Id)
                    : animals.OrderByDescending(a => a.Id)
            };

            var sortingMethod = sortingFields[sortBy];

            return sortingMethod(animals, isAscending);
        }
    }
}
