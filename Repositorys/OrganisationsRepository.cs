using System;
using NUnit.Framework.Constraints;
using pis.Models;

namespace pis.Repositorys
{
    public class OrganisationsRepository
    {
        private static List<Organisation> organisations = new List<Organisation>
        {
            new Organisation(1, "Дружба", "89898989", "5656565656", "50 лет ВЛКСМ",
                OrgTypeRepository.GetOrgTypeByName("Ветеринарная клиника: государственная"), 
                LocalityRepository.GetLocalityByName("Тюмень"), new List<User>(), new List<Contract>()),

            new Organisation(2, "Администрация г. Тюмень", "51515151", "9090909090", "60 лет Октября",
                OrgTypeRepository.GetOrgTypeByName("Орган местного самоуправления"),
                LocalityRepository.GetLocalityByName("Тюмень"), new List<User>(), new List<Contract>()),

            new Organisation(3, "Калинка", "51156767", "9009098787", "30 лет Ленину",
                OrgTypeRepository.GetOrgTypeByName("Ветеринарная клиника: частная"),
                LocalityRepository.GetLocalityByName("Зубарева"), new List<User>(), new List<Contract>())
        };

        public static Organisation GetOrganisationByName(string name)
        {
            var organisation = organisations.Where(organisation => organisation.OrgName == name).FirstOrDefault();
            if (organisation == null)
                throw new ArgumentException($"Нет организации с названием \"{name}\"");
            return organisation;
        }
        //public static bool NewEntry(Organisation organisation)
        //{
        //    try
        //    {
        //        int maxRegistrationNumber = org.Max(a => a.OrgId);
        //        int nextRegistrationNumber = maxRegistrationNumber + 1;

        //        organisation.OrgId = nextRegistrationNumber;
        //        org.Add(organisation);
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        //public static bool DeleteEntry(int id)
        //{
        //    var foundOrg = org.FirstOrDefault(a => a.OrgId == id);
        //    if (foundOrg != null)
        //    {
        //        org.Remove(foundOrg);
        //        Console.WriteLine("Объект Organisation удален.");
        //        return true;
        //    }

        //    Console.WriteLine("Объект Organisation не найден.");
        //    return false;
        //}

        //public static Organisation? GetEntry(int id)
        //{
        //    var foundOrg = org.FirstOrDefault(a => a.OrgId == id);
        //    return foundOrg;
        //}

        //public static List<Organisation> GetOrganizations()
        //{
        //    var foundOrg = org;
        //    return foundOrg;
        //}

        //public static bool ChangeEntry(Organisation organisation)
        //{
        //    var foundOrg = org.FirstOrDefault(a => a.OrgId == organisation.OrgId);
        //    if (foundOrg != null)
        //    {
        //        foundOrg.OrgName = organisation.OrgName;
        //        foundOrg.INN = organisation.INN;
        //        foundOrg.KPP = organisation.KPP;
        //        foundOrg.AdressReg = organisation.AdressReg;
        //        foundOrg.TypeOrg = organisation.TypeOrg;
        //        foundOrg.OrgAttribute = organisation.OrgAttribute;
        //        foundOrg.Locality = organisation.Locality;
        //        return true;
        //    }

        //    return false;
        //}

    }
}