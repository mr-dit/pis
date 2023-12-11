using pis_web_api.Models.post;

namespace pis_web_api.Models.db.ReportStates
{
    public class AgreedFromPerformer : ReportStatusAbstract
    {
        public AgreedFromPerformer() : base() { }
        public AgreedFromPerformer(Report report) : base(report) {}

        public override List<Role> AccesRoles => _roleService.OMSU_Roles;

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
                Report.ChangeStatus(new AgreedFromOMSU(Report));
            }

            Work(user, AccesRoles, action);
        }
    }
}
