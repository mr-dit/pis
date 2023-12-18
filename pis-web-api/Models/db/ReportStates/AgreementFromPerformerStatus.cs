using pis_web_api.Models.post;
using pis_web_api.References;
using pis_web_api.Services;

namespace pis_web_api.Models.db.ReportStates
{
    public class AgreementFromPerformerStatus : ReportStatusAbstract
    {
        public AgreementFromPerformerStatus() : base() { }
        public AgreementFromPerformerStatus(Report report) : base(report) {}

        public override List<Role> AccesRoles => new List<Role>()
        {
            RolesReferences.KURATOR_VETSERVICE,
            RolesReferences.ADMIN
        };
        public override string Name { get { return "Согласование у исполнителя"; } set { } }

        public override void Cancel(UserPost user)
        {
            void action()
            {
                Report.ChangeStatus(new ReworkStatus(Report));
            }

            Work(user, AccesRoles, action);
        }

        public override void Confirm(UserPost user)
        {
            void action()
            {
                Report.ChangeStatus(new AgreedFromPerformer(Report));
            }

            Work(user, AccesRoles, action);
        }
    }
}
