using pis_web_api.Models.db;

namespace pis_web_api.Models.post
{
    public class OrgTypePost
    {
        public string NameOrgType { get; set; }

        public OrgType ConvertToOrgType()
        {
            return new OrgType(NameOrgType);
        }
    }
}
