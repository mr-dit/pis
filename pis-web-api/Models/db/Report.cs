using NUnit.Framework;
using pis_web_api.Models.db.ReportStates;
using pis_web_api.References;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pis_web_api.Models.db
{
    public class Report
    {
        [Key]
        public int Id { get; set; }
        public string StatusName { get; private set; }

        private ReportStatusAbstract? status;
        [NotMapped]
        public ReportStatusAbstract Status { 
            get
            {
                status ??= StatusReferences.GetStatusByName(StatusName, this);
                return status;
            }
            set
            {
                status = value;
                StatusName = status.Name;
            }
        }

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
            Status = new DraftStatus(this);
            StatusUpdate = DateTime.Now;
            DateCreate = DateTime.Now;
        }

        public void ChangeStatus(ReportStatusAbstract status)
        {
            Status = status;
            StatusUpdate = DateTime.Now;
        }

        public void Update(List<StatisticaHolder> statisticaHolders)
        {
            StatisticaHolders = statisticaHolders;
        }
    }
}
