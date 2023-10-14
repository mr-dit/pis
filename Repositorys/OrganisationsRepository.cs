using System;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Constraints;
using pis.Models;

namespace pis.Repositorys
{
    public class OrganisationsRepository
    {
        //private static List<Organisation> organisations = new List<Organisation>
        //{
        //    new Organisation(1, "Дружба", "89898989", "5656565656", "50 лет ВЛКСМ",
        //        OrgTypeRepository.GetOrgTypeByName("Ветеринарная клиника: государственная"), 
        //        LocalityRepository.GetLocalityByName("Тюмень"), new List<User>(), new List<Contract>()),

        //    new Organisation(2, "Администрация г. Тюмень", "51515151", "9090909090", "60 лет Октября",
        //        OrgTypeRepository.GetOrgTypeByName("Орган местного самоуправления"),
        //        LocalityRepository.GetLocalityByName("Тюмень"), new List<User>(), new List<Contract>()),

        //    new Organisation(3, "Калинка", "51156767", "9009098787", "30 лет Ленину",
        //        OrgTypeRepository.GetOrgTypeByName("Ветеринарная клиника: частная"),
        //        LocalityRepository.GetLocalityByName("Зубарева"), new List<User>(), new List<Contract>())
        //};

        public static IQueryable<Organisation> GetOrganisations()
        {
            using (var db = new Context())
            {
                var organisations = db.Organisations.Take(3);
                return organisations;
            }
        }

        public static Organisation GetOrganisationByName(string name)
        {
            using (var db = new Context())
            {
                var organisation = db.Organisations
                    .Where(organisation => organisation.OrgName == name)
                    .Include(x => x.OrgType)
                    .Single();
                if (organisation == null)
                    throw new ArgumentException($"Нет организации с названием \"{name}\"");
                return organisation;
            }
        }

        public static IQueryable<Organisation> GetOrganisationsByName(string name)
        {
            using (var db = new Context())
            {
                var organisations = db.Organisations
                    .Where(organisation => organisation.OrgName.Contains(name))
                    .Include(x => x.OrgType);
                //if (organisations.Count() == 0)
                //    throw new ArgumentException($"Нет организаций с названием \"{name}\"");
                return organisations;
            }
        }

        public static IQueryable<Organisation> GetOrganisationsByINN(string inn)
        {
            using (var db = new Context())
            {
                var organisations = db.Organisations
                    .Where(organisation => organisation.INN.Contains(inn))
                    .Include(x => x.OrgType);
                //if (organisations.Count() == 0)
                //    throw new ArgumentException($"Нет организаций с названием \"{name}\"");
                return organisations;
            }
        }

        public static IQueryable<Organisation> GetOrganisationsByKPP(string kpp)
        {
            using (var db = new Context())
            {
                var organisations = db.Organisations
                    .Where(organisation => organisation.KPP.Contains(kpp))
                    .Include(x => x.OrgType);
                //if (organisations.Count() == 0)
                //    throw new ArgumentException($"Нет организаций с названием \"{name}\"");
                return organisations;
            }
        }

        public static Organisation GetOrganisationById(int id)
        {
            using (var db = new Context())
            {
                var organisation = db.Organisations
                    .Where(organisation => organisation.OrgId == id)
                    .Include(x => x.OrgType)
                    .Single();
                if (organisation == null)
                    throw new ArgumentException($"Нет организации с id \"{id}\"");
                return organisation;
            }
        }

        public static bool AddOrganisation(Organisation org)
        {
            using(var db = new Context())
            {
                try
                {
                    db.Organisations.Add(org);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }

        public static bool DeleteOrganisation(Organisation org)
        {
            using (var db = new Context())
            {
                try
                {
                    db.Organisations.Remove(org);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }

        public static bool UpdateOrganisaton(Organisation org)
        {
            using (var db = new Context())
            {
                try
                {
                    db.Organisations.Update(org);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }

    }
}