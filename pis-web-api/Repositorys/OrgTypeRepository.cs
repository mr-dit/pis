using NUnit.Framework;
using pis.Models;

namespace pis.Repositorys
{
    public class OrgTypeRepository
    {
        private static Lazy<OrgType> statepower = new Lazy<OrgType>(() 
            => GetOrgTypeByName("Исполнительный орган государственной власти"));
        private static Lazy<OrgType> omsu = new Lazy<OrgType>(() 
            => GetOrgTypeByName("Орган местного самоуправления"));
        private static Lazy<OrgType> shelter = new Lazy<OrgType>(() 
            => GetOrgTypeByName("Приют"));
        private static Lazy<OrgType> trapping = new Lazy<OrgType>(() 
            => GetOrgTypeByName("Организация по отлову"));
        private static Lazy<OrgType> transportation = new Lazy<OrgType>(() 
            => GetOrgTypeByName("Организация по транспортировке"));
        private static Lazy<OrgType> gov_vetclinic = new Lazy<OrgType>(() 
            => GetOrgTypeByName("Ветеринарная клиника: государственная"));
        private static Lazy<OrgType> mun_vatclinic = new Lazy<OrgType>(() 
            => GetOrgTypeByName("Ветеринарная клиника: муниципальная"));
        private static Lazy<OrgType> private_vetclinic = new Lazy<OrgType>(() 
            => GetOrgTypeByName("Ветеринарная клиника: частная"));
        private static Lazy<OrgType> foundation = new Lazy<OrgType>(() 
            => GetOrgTypeByName("Благотворительный фонд"));
        private static Lazy<OrgType> sell_and_service = new Lazy<OrgType>(() 
            => GetOrgTypeByName("Организация по продаже товаров и предоставлению услуг для животных"));
        private static Lazy<OrgType> trapping_and_shelter = new Lazy<OrgType>(() 
            => GetOrgTypeByName("Организация по отлову и приют"));
        private static Lazy<OrgType> applicant = new Lazy<OrgType>(() 
            => GetOrgTypeByName("Заявитель"));

        public static OrgType STATEPOWER => statepower.Value;
        public static OrgType OMSU => omsu.Value;
        public static OrgType SHELTER => shelter.Value;
        public static OrgType TRAPPING => trapping.Value;
        public static OrgType TRANSPORTATION => transportation.Value;
        public static OrgType GOV_VETCLINIC => gov_vetclinic.Value;
        public static OrgType MUN_VETCLINIC => mun_vatclinic.Value;
        public static OrgType PRIVATE_VETCLINIC => private_vetclinic.Value;
        public static OrgType FOUNDATION => foundation.Value;
        public static OrgType SELL_AND_SERVICE => sell_and_service.Value;
        public static OrgType TRAPPING_AND_SHELTER => trapping_and_shelter.Value;
        public static OrgType APPLICANT => applicant.Value;

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
    }
}
