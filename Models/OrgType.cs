using System.ComponentModel.DataAnnotations;

namespace pis.Models
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
    }
}
