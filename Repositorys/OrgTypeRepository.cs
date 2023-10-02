using NUnit.Framework;
using pis.Models;

namespace pis.Repositorys
{
    public class OrgTypeRepository
    {
        private static List<OrgType> orgTypes = new List<OrgType>
        {
            new OrgType(1, "Исполнительный орган государственной власти"),
            new OrgType(2, "Орган местного самоуправления"),
            new OrgType(3, "Приют"),
            new OrgType(4, "Организация по отлову"),
            new OrgType(5, "Организация по транспортировке"),
            new OrgType(6, "Ветеринарная клиника: государственная"),
            new OrgType(7, "Ветеринарная клиника: муниципальная"),
            new OrgType(8, "Ветеринарная клиника: частная"),
            new OrgType(9, "Благотворительный фонд"),
            new OrgType(10, "Организации по продаже товаров и предоставлению услуг для животных"),
            new OrgType(11, "Организация по отлову и приют"),
            new OrgType(12, "Заявитель")
        };

        public static OrgType GetOrgTypeByName(string name)
        {
            var orgType = orgTypes.Where(orgType => orgType.NameOrgType == name).FirstOrDefault();
            if (orgType == null)
                throw new ArgumentException($"Нет типа организации с названием \"{name}\"");
            return orgType;
        }
    }
}
