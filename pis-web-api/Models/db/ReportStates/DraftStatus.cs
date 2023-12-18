using pis.Repositorys;
using pis_web_api.Models.post;
using pis_web_api.References;
using pis_web_api.Services;

namespace pis_web_api.Models.db.ReportStates
{
    public class DraftStatus : ReportStatusAbstract
    {
        public DraftStatus() : base() { }
        public DraftStatus(Report report) : base(report) {}

        public override List<Role> AccesRoles => new List<Role>() 
        {
            RolesReferences.OPERATOR_OMSU,
            RolesReferences.ADMIN
        };
        public override string Name { get { return "Черновик"; } set { } } 

        public override void Cancel(UserPost user)
        {
            void action()
            {
                var reportService = new ReportService();
                reportService.DeleteEntry(Report.Id);
            }

            Work(user, AccesRoles, action);
        }

        public override void Confirm(UserPost user)
        {
            void action ()
            {
                Report.ChangeStatus(new AgreementFromPerformerStatus(Report));
            };

            Work(user, AccesRoles, action);
        }
    }
}
