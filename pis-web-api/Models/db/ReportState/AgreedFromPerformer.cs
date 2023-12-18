using pis.Repositorys;
using pis_web_api.Models.post;
using pis_web_api.References;

namespace pis_web_api.Models.db.ReportStates
{
    public class AgreedFromPerformer : ReportStatusAbstract
    {
        public AgreedFromPerformer() : base() { }
        public AgreedFromPerformer(Report report) : base(report) {}

        public override List<Role> AccesRoles => new List<Role>()
        {
            RolesReferences.SIGNER_VETSERVICE, 
            RolesReferences.ADMIN 
        };

        public override string Name { get { return "Согласован у исполнителя"; } set { } }

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
                Report.ChangeStatus(new ConfirmedFromPerformer(Report));
            }

            Work(user, AccesRoles, action);
        }
    }
}
