using System;
using System.Diagnostics.Contracts;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Constraints;
using pis_web_api.Models.db;
using pis_web_api.Models.post;
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
            var allUser = db.Organisations
                .Include(x => x.Locality)
                .Include(x => x.OrgType)
                .Where(value)
                .SortBy(sortBy, isAscending);
            var users = allUser.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return (users, allUser.Count());
        }

        private (List<Organisation>, int) GetOrganisationsByValue(Func<Organisation, bool> value, int pageNumber, int pageSize,
            string sortBy, bool isAscending, UserPost user)
        {
            var orgsIds = db.Contracts
                .Where(x => x.CustomerId == user.OrganisationId || x.PerformerId == user.OrganisationId)
                .Select(x => x.CustomerId == user.OrganisationId ? x.PerformerId : x.CustomerId)
                .Distinct();

            var allUser = db.Organisations
                .Where(x => orgsIds.Contains(x.OrgId))
                .Include(x => x.Locality)
                .Include(x => x.OrgType)
                .Where(value)
                .SortBy(sortBy, isAscending);
            var users = allUser.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return (users, allUser.Count());
        }

        private (List<Organisation>, int) GetOrganisationsByValueForOperatorVetService(Func<Organisation, bool> value, int pageNumber, int pageSize, string sortBy, bool isAscending)
        {
            var orgsTypesIds = new List<int>() { 1, 2, 6 };
            var allUser = db.Organisations
                .Where(x => orgsTypesIds.Contains(x.OrgTypeId))
                .Include(x => x.Locality)
                .Include(x => x.OrgType)
                .Where(value)
                .SortBy(sortBy, isAscending);
            var users = allUser.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return (users, allUser.Count());
        }

        private (List<Organisation>, int) GetOrganisationsByValueForOperatorOMSU(Func<Organisation, bool> value, int pageNumber, int pageSize, string sortBy, bool isAscending)
        {
            var orgsTypesIds = new List<int>() { 3, 4, 11, 5, 6, 8, 9, 10 };
            var allUser = db.Organisations
                .Where(x => orgsTypesIds.Contains(x.OrgTypeId))
                .Include(x => x.Locality)
                .Include(x => x.OrgType)
                .Where(value)
                .SortBy(sortBy, isAscending);
            var users = allUser.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return (users, allUser.Count());
        }

        public (List<Organisation>, int) GetOrganisationsByDefault(
           string value, int pageNumber,
           int pageSize, string sortBy, bool isAscending) =>
           GetOrganisationsByValue(org => { return true; },
               pageNumber, pageSize, sortBy, isAscending);

        public (List<Organisation>, int) GetOrganisationsByDefault(
           string value, int pageNumber,
           int pageSize, string sortBy, bool isAscending, UserPost user) =>
           GetOrganisationsByValue(org => { return true; },
               pageNumber, pageSize, sortBy, isAscending, user);

        public (List<Organisation>, int) GetOrganisationsByDefaultForOperatorVetService(
           string value, int pageNumber,
           int pageSize, string sortBy, bool isAscending) =>
           GetOrganisationsByValueForOperatorVetService(org => { return true; },
               pageNumber, pageSize, sortBy, isAscending);

        public (List<Organisation>, int) GetOrganisationsByDefaultForOperatorOMSU(
           string value, int pageNumber,
           int pageSize, string sortBy, bool isAscending) =>
           GetOrganisationsByValueForOperatorOMSU(org => { return true; },
               pageNumber, pageSize, sortBy, isAscending);



        public (List<Organisation>, int) GetOrganisationsByOrgName(
           string name, int pageNumber,
           int pageSize, string sortBy, bool isAscending) =>
           GetOrganisationsByValue(org => org.OrgName.Contains(name, StringComparison.InvariantCultureIgnoreCase),
               pageNumber, pageSize, sortBy, isAscending);

        public (List<Organisation>, int) GetOrganisationsByOrgName(
           string name, int pageNumber,
           int pageSize, string sortBy, bool isAscending, UserPost user) =>
           GetOrganisationsByValue(org => org.OrgName.Contains(name, StringComparison.InvariantCultureIgnoreCase),
               pageNumber, pageSize, sortBy, isAscending, user);

        public (List<Organisation>, int) GetOrganisationsByOrgNameForOperatorVetService(
           string name, int pageNumber,
           int pageSize, string sortBy, bool isAscending) =>
           GetOrganisationsByValueForOperatorVetService(org => org.OrgName.Contains(name, StringComparison.InvariantCultureIgnoreCase),
               pageNumber, pageSize, sortBy, isAscending);

        public (List<Organisation>, int) GetOrganisationsByOrgNameForOperatorOMSU(
           string name, int pageNumber,
           int pageSize, string sortBy, bool isAscending) =>
           GetOrganisationsByValueForOperatorOMSU(org => org.OrgName.Contains(name, StringComparison.InvariantCultureIgnoreCase),
               pageNumber, pageSize, sortBy, isAscending);



        public (List<Organisation>, int) GetOrganisationsByINN(
           string inn, int pageNumber,
           int pageSize, string sortBy, bool isAscending) =>
           GetOrganisationsByValue(org => org.INN.Contains(inn, StringComparison.InvariantCultureIgnoreCase),
               pageNumber, pageSize, sortBy, isAscending);

        public (List<Organisation>, int) GetOrganisationsByINN(
           string inn, int pageNumber,
           int pageSize, string sortBy, bool isAscending, UserPost user) =>
           GetOrganisationsByValue(org => org.INN.Contains(inn, StringComparison.InvariantCultureIgnoreCase),
               pageNumber, pageSize, sortBy, isAscending, user);

        public (List<Organisation>, int) GetOrganisationsByINNForOperatorVetService(
           string inn, int pageNumber,
           int pageSize, string sortBy, bool isAscending) =>
           GetOrganisationsByValueForOperatorVetService(org => org.INN.Contains(inn, StringComparison.InvariantCultureIgnoreCase),
               pageNumber, pageSize, sortBy, isAscending);

        public (List<Organisation>, int) GetOrganisationsByINNForOperatorOMSU(
           string inn, int pageNumber,
           int pageSize, string sortBy, bool isAscending) =>
           GetOrganisationsByValueForOperatorOMSU(org => org.INN.Contains(inn, StringComparison.InvariantCultureIgnoreCase),
               pageNumber, pageSize, sortBy, isAscending);



        public (List<Organisation>, int) GetOrganisationsByKPP(
           string kpp, int pageNumber,
           int pageSize, string sortBy, bool isAscending) =>
           GetOrganisationsByValue(org => org.KPP.Contains(kpp, StringComparison.InvariantCultureIgnoreCase),
               pageNumber, pageSize, sortBy, isAscending);

        public (List<Organisation>, int) GetOrganisationsByKPP(
           string kpp, int pageNumber,
           int pageSize, string sortBy, bool isAscending, UserPost user) =>
           GetOrganisationsByValue(org => org.KPP.Contains(kpp, StringComparison.InvariantCultureIgnoreCase),
               pageNumber, pageSize, sortBy, isAscending, user);

        public (List<Organisation>, int) GetOrganisationsByKPPForOperatorVetService(
           string kpp, int pageNumber,
           int pageSize, string sortBy, bool isAscending) =>
           GetOrganisationsByValueForOperatorVetService(org => org.KPP.Contains(kpp, StringComparison.InvariantCultureIgnoreCase),
               pageNumber, pageSize, sortBy, isAscending);

        public (List<Organisation>, int) GetOrganisationsByKPPForOperatorOMSU(
           string kpp, int pageNumber,
           int pageSize, string sortBy, bool isAscending) =>
           GetOrganisationsByValueForOperatorOMSU(org => org.KPP.Contains(kpp, StringComparison.InvariantCultureIgnoreCase),
               pageNumber, pageSize, sortBy, isAscending);



        public (List<Organisation>, int) GetOrganisationsByLocality(
           string localityName, int pageNumber,
           int pageSize, string sortBy, bool isAscending) =>
           GetOrganisationsByValue(org => org.Locality.NameLocality.Contains(localityName, StringComparison.InvariantCultureIgnoreCase),
               pageNumber, pageSize, sortBy, isAscending);

        public (List<Organisation>, int) GetOrganisationsByLocality(
           string localityName, int pageNumber,
           int pageSize, string sortBy, bool isAscending, UserPost user) =>
           GetOrganisationsByValue(org => org.Locality.NameLocality.Contains(localityName, StringComparison.InvariantCultureIgnoreCase),
               pageNumber, pageSize, sortBy, isAscending, user);

        public (List<Organisation>, int) GetOrganisationsByLocalityForOperatorVetService(
           string localityName, int pageNumber,
           int pageSize, string sortBy, bool isAscending) =>
           GetOrganisationsByValueForOperatorVetService(org => org.Locality.NameLocality.Contains(localityName, StringComparison.InvariantCultureIgnoreCase),
               pageNumber, pageSize, sortBy, isAscending);

        public (List<Organisation>, int) GetOrganisationsByLocalityForOperatorOMSU(
           string localityName, int pageNumber,
           int pageSize, string sortBy, bool isAscending) =>
           GetOrganisationsByValueForOperatorOMSU(org => org.Locality.NameLocality.Contains(localityName, StringComparison.InvariantCultureIgnoreCase),
               pageNumber, pageSize, sortBy, isAscending);




        public (List<Organisation>, int) GetOrganisationsByAdressReg(
           string adressReg, int pageNumber,
           int pageSize, string sortBy, bool isAscending) =>
           GetOrganisationsByValue(org => org.AdressReg.Contains(adressReg, StringComparison.InvariantCultureIgnoreCase),
               pageNumber, pageSize, sortBy, isAscending);

        public (List<Organisation>, int) GetOrganisationsByAdressReg(
           string adressReg, int pageNumber,
           int pageSize, string sortBy, bool isAscending, UserPost user) =>
           GetOrganisationsByValue(org => org.AdressReg.Contains(adressReg, StringComparison.InvariantCultureIgnoreCase),
               pageNumber, pageSize, sortBy, isAscending, user);

        public (List<Organisation>, int) GetOrganisationsByAdressRegForOperatorVetService(
           string adressReg, int pageNumber,
           int pageSize, string sortBy, bool isAscending) =>
           GetOrganisationsByValueForOperatorVetService(org => org.AdressReg.Contains(adressReg, StringComparison.InvariantCultureIgnoreCase),
               pageNumber, pageSize, sortBy, isAscending);

        public (List<Organisation>, int) GetOrganisationsByAdressRegForOperatorOMSU(
           string adressReg, int pageNumber,
           int pageSize, string sortBy, bool isAscending) =>
           GetOrganisationsByValueForOperatorOMSU(org => org.AdressReg.Contains(adressReg, StringComparison.InvariantCultureIgnoreCase),
               pageNumber, pageSize, sortBy, isAscending);
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