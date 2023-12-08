using NUnit.Framework;
using System.ComponentModel.DataAnnotations;

namespace pis_web_api.Models.db
{
    public class Report
    {
        [Key]
        public int Id { get; set; }

        public ReportStatus Status { get; set; }
        public DateTime StatusUpdate { get; set; }
        public DateTime DateCreate { get; set; }

        public DateOnly DateStart { get; set; }
        public DateOnly DateEnd { get; set; }
        public int PerformerId { get; set; }
        public Organisation Performer { get; set; }

        public List<StatisticaHolder> StatisticaHolders { get; set; }

        public Report() { }
        public Report(List<StatisticaHolder> statisticaHolders, DateOnly dateStart, DateOnly dateEnd, int orgId)
        {
            StatisticaHolders = statisticaHolders;
            DateStart = dateStart;
            DateEnd = dateEnd;
            PerformerId = orgId;
            Status = ReportStatus.Черновик;
            StatusUpdate = DateTime.Now;
            DateCreate = DateTime.Now;
        }

        public void ChangeStatusToSoglasovanieUPodpisanta()
        {
            Status = ReportStatus.Согласование_у_исполнителя;
            StatusUpdate = DateTime.Now;
        }

        public void ChangeStatusToSoglasovanUPodpisanta()
        {
            Status = ReportStatus.Согласован_у_исполнителя;
            StatusUpdate = DateTime.Now;
        }

        public void ChangeStatusToSoglasovanUOMSU()
        {
            Status = ReportStatus.Согласован_в_ОМСУ;
            StatusUpdate = DateTime.Now;
        }

        public void ChangeStatusToDorabotka()
        {
            Status = ReportStatus.Доработка;
            StatusUpdate = DateTime.Now;
        }

        public void Update(List<StatisticaHolder> statisticaHolders)
        {
            StatisticaHolders = statisticaHolders;

        }
    }
}
