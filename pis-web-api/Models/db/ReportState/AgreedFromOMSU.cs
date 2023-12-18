using pis_web_api.Models.post;
using System.ComponentModel.DataAnnotations.Schema;

namespace pis_web_api.Models.db.ReportStates
{
    public class AgreedFromOMSU : ReportStatusAbstract
    {
        public AgreedFromOMSU() : base() { }
        public AgreedFromOMSU(Report report) : base(report) { }

        public override List<Role> AccesRoles => new List<Role>();
        public override string Name { get { return "Согласован в ОМСУ"; } set { } }

        public override void Cancel(UserPost user)
        {
            void action()
            {
                throw new Exception("Невозможно!");
            }

            Work(user, AccesRoles, action);
        }

        public override void Confirm(UserPost user)
        {
            void action()
            {
                throw new Exception("Невозможно!");
            }

            Work(user, AccesRoles, action);
        }
    }
}
