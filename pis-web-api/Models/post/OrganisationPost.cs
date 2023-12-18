using pis_web_api.Models.db;

namespace pis_web_api.Models.post
{
    public class OrganisationPost
    {
        public string OrgName { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }
        public string AdressReg { get; set; }
        public int OrgTypeId { get; set; }
        public int LocalityId { get; set; }

        public Organisation ConvertToOrganisation()
        {
            var con = new Organisation(OrgName, INN, KPP, AdressReg, OrgTypeId, LocalityId);
            return con;
        }
    }
}
