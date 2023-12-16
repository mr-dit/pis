using pis_web_api.Models.post;
using pis_web_api.References;

namespace pis_web_api.Models.db.ReportStates
{
    public class ConfirmedFromPerformer : ReportStatusAbstract
    {
        public ConfirmedFromPerformer() : base() { }
        public ConfirmedFromPerformer(Report report): base(report) { }

        public override string Name { get { return "Удтвержден у исполнителя"; } set { } }

        public override List<Role> AccesRoles => new List<Role>() 
        {
            RolesReferences.KURATOR_OMSU,
            RolesReferences.ADMIN
        };

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
                Report.ChangeStatus(new AgreedFromOMSU(Report));
            }

            Work(user, AccesRoles, action);
        }
    }
}
