using System.ComponentModel.DataAnnotations;

namespace pis.Models
{
    public class OrgType
    {
        [Key]
        public int IdOrgType { get; set; }
        public string NameOrgType { get; set; }

        //public OrgType(int idOrgType, string nameOrgType)
        //{
        //    IdOrgType = idOrgType;
        //    NameOrgType = nameOrgType;
        //}
    }
}
