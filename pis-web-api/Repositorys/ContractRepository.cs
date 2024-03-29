﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pis.Services;
using pis.Repositorys;
using pis;
using Microsoft.EntityFrameworkCore;
using pis_web_api.Repositorys;
using pis_web_api.Models.db;
using pis_web_api.Models.post;

namespace pis.Repositorys
{
    public class ContractRepository : Repository<Contract>
    {
        public ContractRepository() : base()
        {
        }

        private delegate void ContractAction(Context db, Contract con);

        private (List<Contract>, int) GetContractsByValue(Func<Contract, bool> value, DateOnly dateStart, DateOnly dateEnd, int pageNumber, int pageSize, string sortBy, bool isAscending)
        {
            var allCons = db.Contracts
                .Include(x => x.Customer)
                .Include(x => x.Performer)
                .Where(value)
                .Where(x => x.ConclusionDate >= dateStart && x.ConclusionDate <= dateEnd)
                .SortBy(sortBy, isAscending);
            var contracts = allCons.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return (contracts, allCons.Count());
            
        }

        private (List<Contract>, int) GetContractsByValueForOrg(Func<Contract, bool> value, DateOnly dateStart, DateOnly dateEnd, 
            int pageNumber, int pageSize, string sortBy, bool isAscending, UserPost user)
        {
            var contractsId = db.Contracts
                .Where(x => x.CustomerId == user.OrganisationId || x.PerformerId == user.OrganisationId)
                .Select(x => x.IdContract)
                .ToList();

            var allCons = db.Contracts
                .Where(x => contractsId.Contains(x.IdContract))
                .Include(x => x.Customer)
                .Include(x => x.Performer)
                .Where(value)
                .Where(x => x.ExpirationDate >= dateStart && x.ExpirationDate <= dateEnd)
                .SortBy(sortBy, isAscending)
                .ToList();
            var contracts = allCons.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return (contracts, allCons.Count());
        }


        public (List<Contract>, int) GetContractsByCustomerName(DateOnly dateStart, DateOnly dateEnd, string name, int pageNumber, int pageSize, string sortBy, bool isAscending) =>
            GetContractsByValue(con => con.Customer.OrgName.Contains(name, StringComparison.InvariantCultureIgnoreCase),
                dateStart, dateEnd, pageNumber, pageSize, sortBy, isAscending);

        public (List<Contract>, int) GetContractsByCustomerNameByOrg(DateOnly dateStart, DateOnly dateEnd, string name, int pageNumber, 
            int pageSize, string sortBy, bool isAscending, UserPost user) =>
            GetContractsByValueForOrg(con => con.Customer.OrgName.Contains(name, StringComparison.InvariantCultureIgnoreCase),
                dateStart, dateEnd, pageNumber, pageSize, sortBy, isAscending, user);



        public (List<Contract>, int) GetContractsByPerformerName(DateOnly dateStart, DateOnly dateEnd, string name, int pageNumber, int pageSize, string sortBy, bool isAscending) =>
            GetContractsByValue(con => con.Performer.OrgName.Contains(name, StringComparison.InvariantCultureIgnoreCase),
                dateStart, dateEnd, pageNumber, pageSize, sortBy, isAscending);

        public (List<Contract>, int) GetContractsByPerformerNameByOrg(DateOnly dateStart, DateOnly dateEnd, string name, int pageNumber, 
            int pageSize, string sortBy, bool isAscending, UserPost user) =>
            GetContractsByValueForOrg(con => con.Performer.OrgName.Contains(name, StringComparison.InvariantCultureIgnoreCase),
                dateStart, dateEnd, pageNumber, pageSize, sortBy, isAscending, user);



        public (List<Contract>, int) GetContractsByDefault(DateOnly dateStart, DateOnly dateEnd, string useless, int pageNumber, int pageSize, string sortBy, bool isAscending) =>
            GetContractsByValue(con => { return true; }, dateStart, dateEnd, pageNumber, pageSize, sortBy, isAscending);

        public (List<Contract>, int) GetContractsByDefaultByOrg(DateOnly dateStart, DateOnly dateEnd, string useless, int pageNumber
            , int pageSize, string sortBy, bool isAscending, UserPost user) =>
            GetContractsByValueForOrg(con => { return true; }, dateStart, dateEnd, pageNumber, pageSize, sortBy, isAscending, user);

        public override Contract GetById(int id)
        {
            using (var db = new Context())
            {
                var contract = db.Contracts
                    .Where(x => x.IdContract == id)
                    .Include(x => x.Performer)
                        .ThenInclude(x => x.Users)
                    .Include(x => x.Customer)
                        .ThenInclude(x => x.Users)
                    .Include(x => x.Localities)
                    .Include(x => x.Vaccinations)
                    .Single();
                return contract;
            }
        }
    }

    static class ContractExtension
    {
        public static IEnumerable<Contract> SortBy(this IEnumerable<Contract> con, string sortBy, bool isAscending)
        {
            var sortingFields = new Dictionary<string, Func<IEnumerable<Contract>, bool, IOrderedEnumerable<Contract>>>(StringComparer.InvariantCultureIgnoreCase)
            {
                [nameof(Contract.ConclusionDate)] = (con, isAscending) =>
                    isAscending ? con.OrderBy(a => a.ConclusionDate)
                    : con.OrderByDescending(a => a.ConclusionDate),

                [nameof(Contract.ExpirationDate)] = (con, isAscending) =>
                    isAscending ? con.OrderBy(a => a.ExpirationDate)
                    : con.OrderByDescending(a => a.ExpirationDate),

                [nameof(Contract.Performer)] = (con, isAscending) =>
                    isAscending ? con.OrderBy(a => a.Performer.OrgName)
                    : con.OrderByDescending(a => a.Performer.OrgName),

                [nameof(Contract.Customer)] = (con, isAscending) =>
                    isAscending ? con.OrderBy(a => a.Customer.OrgName)
                    : con.OrderByDescending(a => a.Customer.OrgName)
            };

            var sortingMethod = sortingFields[sortBy];

            return sortingMethod(con, isAscending);
        }
    }
}