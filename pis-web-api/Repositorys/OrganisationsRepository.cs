using System;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Constraints;
using pis.Models;
using pis_web_api.Repositorys;

namespace pis.Repositorys
{
    public class OrganisationsRepository : Repository<Organisation>
    {
        public OrganisationsRepository() : base()
        {
        }

        private delegate void UserAction(Context db, Organisation user);

        private (List<Organisation>, int) GetOrganisationsByValue(Func<Organisation, bool> value, int pageNumber, int pageSize, string sortBy, bool isAscending)
        {
            using (Context db = new Context())
            {
                var allUser = db.Organisations
                    .Include(x => x.Locality)
                    .Include(x => x.OrgType)
                    .Where(value)
                    .SortBy(sortBy, isAscending);
                var users = allUser.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                return (users, allUser.Count());
            }
        }

        public (List<Organisation>, int) GetOrganisationsByDefault(
           string value, int pageNumber,
           int pageSize, string sortBy, bool isAscending) =>
           GetOrganisationsByValue(org => { return true; },
               pageNumber, pageSize, sortBy, isAscending);

        public (List<Organisation>, int) GetOrganisationsByOrgName(
           string name, int pageNumber,
           int pageSize, string sortBy, bool isAscending) =>
           GetOrganisationsByValue(org => org.OrgName.Contains(name, StringComparison.InvariantCultureIgnoreCase),
               pageNumber, pageSize, sortBy, isAscending);

        public (List<Organisation>, int) GetOrganisationsByINN(
           string inn, int pageNumber,
           int pageSize, string sortBy, bool isAscending) =>
           GetOrganisationsByValue(org => org.INN.Contains(inn, StringComparison.InvariantCultureIgnoreCase),
               pageNumber, pageSize, sortBy, isAscending);

        public (List<Organisation>, int) GetOrganisationsByKPP(
           string kpp, int pageNumber,
           int pageSize, string sortBy, bool isAscending) =>
           GetOrganisationsByValue(org => org.KPP.Contains(kpp, StringComparison.InvariantCultureIgnoreCase),
               pageNumber, pageSize, sortBy, isAscending);

        public (List<Organisation>, int) GetOrganisationsByLocality(
           string localityName, int pageNumber,
           int pageSize, string sortBy, bool isAscending) =>
           GetOrganisationsByValue(org => org.Locality.NameLocality.Contains(localityName, StringComparison.InvariantCultureIgnoreCase),
               pageNumber, pageSize, sortBy, isAscending);

        public (List<Organisation>, int) GetOrganisationsByAdressReg(
           string adressReg, int pageNumber,
           int pageSize, string sortBy, bool isAscending) =>
           GetOrganisationsByValue(org => org.AdressReg.Contains(adressReg, StringComparison.InvariantCultureIgnoreCase),
               pageNumber, pageSize, sortBy, isAscending);

        //public static List<Organisation> GetOrganisations(int limit)
        //{
        //    using (var db = new Context())
        //    {
        //        var organisations = db.Organisations
        //            .Take(limit)
        //            .Include(x => x.OrgType)
        //            .Include(x => x.Locality)
        //            .ToList();
        //        return organisations;
        //    }
        //}

        //public static Organisation GetOrganisationByName(string name)
        //{
        //    using (var db = new Context())
        //    {
        //        var organisation = db.Organisations
        //            .Where(organisation => organisation.OrgName == name)
        //            .Include(x => x.OrgType)
        //            .Include(x => x.Locality)
        //            .Single();
        //        if (organisation == null)
        //            throw new ArgumentException($"Нет организации с названием \"{name}\"");
        //        return organisation;
        //    }
        //}

        //public static List<Organisation> GetOrganisationsByName(string name)
        //{
        //    using (var db = new Context())
        //    {
        //        var organisations = db.Organisations
        //            .Where(organisation => organisation.OrgName.Contains(name))
        //            .Include(x => x.OrgType)
        //            .Include(x => x.Locality);
        //        //if (organisations.Count() == 0)
        //        //    throw new ArgumentException($"Нет организаций с названием \"{name}\"");
        //        return organisations.ToList();
        //    }
        //}

        //public static List<Organisation> GetOrganisationsByINN(string inn)
        //{
        //    using (var db = new Context())
        //    {
        //        var organisations = db.Organisations
        //            .Where(organisation => organisation.INN.Contains(inn))
        //            .Include(x => x.OrgType)
        //            .Include(x => x.Locality);
        //        //if (organisations.Count() == 0)
        //        //    throw new ArgumentException($"Нет организаций с названием \"{name}\"");
        //        return organisations.ToList();
        //    }
        //}

        //public static List<Organisation> GetOrganisationsByKPP(string kpp)
        //{
        //    using (var db = new Context())
        //    {
        //        var organisations = db.Organisations
        //            .Where(organisation => organisation.KPP.Contains(kpp))
        //            .Include(x => x.OrgType)
        //            .Include(x => x.Locality);
        //        //if (organisations.Count() == 0)
        //        //    throw new ArgumentException($"Нет организаций с названием \"{name}\"");
        //        return organisations.ToList();
        //    }
        //}

    }

    static class OrganisationExtension
    {
        public static IEnumerable<Organisation> SortBy(this IEnumerable<Organisation> orgs, string sortBy, bool isAscending)
        {
            var sortingFields = new Dictionary<string, Func<IEnumerable<Organisation>, bool, IOrderedEnumerable<Organisation>>>(StringComparer.InvariantCultureIgnoreCase)
            {
                [nameof(Organisation.OrgName)] = (orgs, isAscending) =>
                    isAscending ? orgs.OrderBy(a => a.OrgName)
                    : orgs.OrderByDescending(a => a.OrgName),

                [nameof(Organisation.AdressReg)] = (orgs, isAscending) =>
                    isAscending ? orgs.OrderBy(a => a.AdressReg)
                    : orgs.OrderByDescending(a => a.AdressReg),

                [nameof(Organisation.INN)] = (orgs, isAscending) =>
                    isAscending ? orgs.OrderBy(a => a.INN)
                    : orgs.OrderByDescending(a => a.INN),

                [nameof(Organisation.KPP)] = (orgs, isAscending) =>
                    isAscending ? orgs.OrderBy(a => a.KPP)
                    : orgs.OrderByDescending(a => a.KPP),

                [nameof(Organisation.Locality)] = (orgs, isAscending) =>
                    isAscending ? orgs.OrderBy(a => a.Locality.NameLocality)
                    : orgs.OrderByDescending(a => a.Locality.NameLocality),

                [nameof(Organisation.OrgType)] = (orgs, isAscending) =>
                    isAscending ? orgs.OrderBy(a => a.OrgType.NameOrgType)
                    : orgs.OrderByDescending(a => a.OrgType.NameOrgType)
            };

            var sortingMethod = sortingFields[sortBy];

            return sortingMethod(orgs, isAscending);
        }
    }
    
}