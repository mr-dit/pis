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
            Status = ConvertStatusToString(report.Status);
            StatusUpdate = report.StatusUpdate.ToLongDateString();
            DateCreate = report.DateCreate.ToShortDateString();
            RolesAccess = FillRolesByStatus(report.Status);
        }

        private List<int> FillRolesByStatus(ReportStatus status)
        {
            if(status == ReportStatus.Черновик || status == ReportStatus.Доработка || status == ReportStatus.Согласован_у_исполнителя)
            {
                return new List<int>() { 9, 10, 11, 15 };
            }
            else if(status == ReportStatus.Согласование_у_исполнителя)
            {
                return new List<int>() { 1, 4, 6, 13, 14, 15 };
            }
            else 
            {
                return new List<int>(); 
            }
        }

        private string ConvertStatusToString(ReportStatus status)
        {
            if (status == ReportStatus.Черновик)
                return "Черновик";
            else if (status == ReportStatus.Доработка)
                return "Доработка";
            else if (status == ReportStatus.Согласован_в_ОМСУ)
                return "Согласован в ОМСУ";
            else if (status == ReportStatus.Согласован_у_исполнителя)
                return "Согласован у исполнителя";
            else if (status == ReportStatus.Согласование_у_исполнителя)
                return "Согласование у исполнителя";
            else throw new Exception("Некорректный статус");
        }
    }
}
