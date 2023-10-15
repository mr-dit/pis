using NUnit.Framework;
using pis.Models;

namespace pis.Repositorys
{
    public class OrgTypeRepository
    {
        //private static List<OrgType> orgTypes = new List<OrgType>
        //{
        //    new OrgType(1, "Исполнительный орган государственной власти"),
        //    new OrgType(2, "Орган местного самоуправления"),
        //    new OrgType(3, "Приют"),
        //    new OrgType(4, "Организация по отлову"),
        //    new OrgType(5, "Организация по транспортировке"),
        //    new OrgType(6, "Ветеринарная клиника: государственная"),
        //    new OrgType(7, "Ветеринарная клиника: муниципальная"),
        //    new OrgType(8, "Ветеринарная клиника: частная"),
        //    new OrgType(9, "Благотворительный фонд"),
        //    new OrgType(10, "Организации по продаже товаров и предоставлению услуг для животных"),
        //    new OrgType(11, "Организация по отлову и приют"),
        //    new OrgType(12, "Заявитель")
        //};

        public static void AddOrgType(OrgType orgType)
        {
            using (var db = new Context())
            {
                db.OrgTypes.Add(orgType);
                db.SaveChanges();
            }
        }

        public static void DeleteOrgType(OrgType orgType)
        {
            using (var db = new Context())
            {
                db.OrgTypes.Remove(orgType); 
                db.SaveChanges();
            }
        }

        public static void UpdateOrgType(OrgType orgType)
        {
            using (var db = new Context()) 
            { 
                db.OrgTypes.Update(orgType); 
                db.SaveChanges();
            }
        }

        public static OrgType GetOrgTypeByName(string name)
        {
            using (var db = new Context())
            {
                var orgType = db.OrgTypes.Where(orgType => orgType.NameOrgType == name).Single();
                if (orgType == null)
                    throw new ArgumentException($"Нет типа организации с названием \"{name}\"");
                return orgType;
            }
        }

        public static OrgType STATEPOWER => GetOrgTypeByName("Исполнительный орган государственной власти");
        public static OrgType OMSU => GetOrgTypeByName("Орган местного самоуправления");
        public static OrgType SHELTER => GetOrgTypeByName("Приют");
        public static OrgType TRAPPING => GetOrgTypeByName("Организация по отлову");
        public static OrgType TRANSPORTATION => GetOrgTypeByName("Организация по транспортировке");
        public static OrgType GOV_VETCLINIC => GetOrgTypeByName("Ветеринарная клиника: государственная");
        public static OrgType MUN_VETCLINIC => GetOrgTypeByName("Ветеринарная клиника: муниципальная");
        public static OrgType PRIVATE_VETCLINIC => GetOrgTypeByName("Ветеринарная клиника: частная");
        public static OrgType FOUNDATION => GetOrgTypeByName("Благотворительный фонд");
        public static OrgType SELL_AND_SERVICE => GetOrgTypeByName("Организация по продаже товаров и предоставлению услуг для животных");
        public static OrgType TRAPPING_AND_SHELTER => GetOrgTypeByName("Организация по отлову и приют");
        public static OrgType APPLICANT => GetOrgTypeByName("Заявитель");
    }
}
