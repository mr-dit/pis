using pis_web_api.Models.db;
using System.ComponentModel.DataAnnotations;

namespace pis_web_api.Models.get
{
    public class ReportGet
    {
        public int Id { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public string Performer { get; set; }

        public string Status { get; set; }
        public string StatusUpdate { get; set; }
        public string DateCreate { get; set; }

        public List<int> RolesAccess { get; set; }

        public ReportGet(Report report) 
        {
            Id = report.Id;
            DateStart = report.DateStart.ToShortDateString();
            DateEnd = report.DateEnd.ToShortDateString();
            Performer = report.Performer.OrgName;
            Status = report.Status.Name;
            StatusUpdate = report.StatusUpdate.ToLongDateString();
            DateCreate = report.DateCreate.ToShortDateString();
            RolesAccess = report.Status.AccesRoles.Select(x => x.IdRole).ToList();
        }
    }
}
