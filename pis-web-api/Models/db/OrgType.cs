using pis_web_api.Models.post;
using System.ComponentModel.DataAnnotations;

namespace pis_web_api.Models.db
{
    public class OrgType
    {
        [Key]
        public int IdOrgType { get; set; }
        public string NameOrgType { get; set; }

        public OrgType(string nameOrgType)
        {
            NameOrgType = nameOrgType;
        }

        public OrgType() { }

        public void Update(OrgTypePost orgTypePost)
        {
            NameOrgType = orgTypePost.NameOrgType;
        }
    }
}
