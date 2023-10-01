namespace pis.Models
{
    public class OrgType
    {
        public int IdOrgType { get; set; }
        public string NameOrgType { get; set; }

        public OrgType(int idOrgType, string nameOrgType)
        {
            IdOrgType = idOrgType;
            NameOrgType = nameOrgType;
        }
    }
}
