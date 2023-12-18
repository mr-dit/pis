using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using pis_web_api.Models.db;
using pis_web_api.Repositorys;
using System.Globalization;

namespace pis.Repositorys
{
    public class OrgTypeRepository : Repository<OrgType>
    {
        private Lazy<OrgType> statepower = new Lazy<OrgType>(() 
            => GetOrgTypeByName("Исполнительный орган государственной власти"));
        private Lazy<OrgType> omsu = new Lazy<OrgType>(() 
            => GetOrgTypeByName("Орган местного самоуправления"));
        private Lazy<OrgType> shelter = new Lazy<OrgType>(() 
            => GetOrgTypeByName("Приют"));
        private Lazy<OrgType> trapping = new Lazy<OrgType>(() 
            => GetOrgTypeByName("Организация по отлову"));
        private Lazy<OrgType> transportation = new Lazy<OrgType>(() 
            => GetOrgTypeByName("Организация по транспортировке"));
        private Lazy<OrgType> gov_vetclinic = new Lazy<OrgType>(() 
            => GetOrgTypeByName("Ветеринарная клиника: государственная"));
        private Lazy<OrgType> mun_vatclinic = new Lazy<OrgType>(() 
            => GetOrgTypeByName("Ветеринарная клиника: муниципальная"));
        private Lazy<OrgType> private_vetclinic = new Lazy<OrgType>(() 
            => GetOrgTypeByName("Ветеринарная клиника: частная"));
        private Lazy<OrgType> foundation = new Lazy<OrgType>(() 
            => GetOrgTypeByName("Благотворительный фонд"));
        private Lazy<OrgType> sell_and_service = new Lazy<OrgType>(() 
            => GetOrgTypeByName("Организация по продаже товаров и предоставлению услуг для животных"));
        private Lazy<OrgType> trapping_and_shelter = new Lazy<OrgType>(() 
            => GetOrgTypeByName("Организация по отлову и приют"));
        private Lazy<OrgType> applicant = new Lazy<OrgType>(() 
            => GetOrgTypeByName("Заявитель"));

        public OrgType STATEPOWER => statepower.Value;
        public OrgType OMSU => omsu.Value;
        public OrgType SHELTER => shelter.Value;
        public OrgType TRAPPING => trapping.Value;
        public OrgType TRANSPORTATION => transportation.Value;
        public OrgType GOV_VETCLINIC => gov_vetclinic.Value;
        public OrgType MUN_VETCLINIC => mun_vatclinic.Value;
        public OrgType PRIVATE_VETCLINIC => private_vetclinic.Value;
        public OrgType FOUNDATION => foundation.Value;
        public OrgType SELL_AND_SERVICE => sell_and_service.Value;
        public OrgType TRAPPING_AND_SHELTER => trapping_and_shelter.Value;
        public OrgType APPLICANT => applicant.Value;

        private static OrgType GetOrgTypeByName(string name)
        {
            using (var db = new Context())
            {
                var orgType = db.OrgTypes.Where(orgType => orgType.NameOrgType == name).Single();
                if (orgType == null)
                    throw new ArgumentException($"Нет типа организации с названием \"{name}\"");
                return orgType;
            }
        }

        public (List<OrgType>, int) GetOrgTypesByName(string name, int pageNumber, int pageSize)
        {
            using (Context db = new Context())
            {
                var allTypes = db.OrgTypes
                    .AsEnumerable()
                    .Where(x => x.NameOrgType.Contains(name, StringComparison.InvariantCultureIgnoreCase))
                    .OrderBy(x => x.NameOrgType);
                var types = allTypes.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                return (types, allTypes.Count());
            }
        }
    }
}
