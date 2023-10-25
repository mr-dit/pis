﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pis.Models;
using pis.Services;
using pis.Repositorys;
using pis;
using Microsoft.EntityFrameworkCore;
using pis_web_api.Repositorys;

namespace pis.Repositorys
{
    public class ContractRepository : Repository<Contract>
    {
        public ContractRepository() : base()
        {
        }

        private delegate void ContractAction(Context db, Contract con);

        private (List<Contract>, int) GetContractsByValue(Func<Contract, bool> value, int pageNumber, int pageSize, string sortBy, bool isAscending)
        {
            using (Context db = new Context())
            {
                var allCons = db.Contracts
                    .Include(x => x.Customer)
                    .Include(x => x.Performer)
                    .Where(value)
                    .SortBy(sortBy, isAscending);
                var contracts = allCons.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                return (contracts, allCons.Count());
            }
        }

        public (List<Contract>, int) GetContractsByCustomerName(string name, int pageNumber, int pageSize, string sortBy, bool isAscending) =>
            GetContractsByValue(con => con.Customer.OrgName.Contains(name, StringComparison.InvariantCultureIgnoreCase),
                pageNumber, pageSize, sortBy, isAscending);

        public (List<Contract>, int) GetContractsByPerformerName(string name, int pageNumber, int pageSize, string sortBy, bool isAscending) =>
            GetContractsByValue(con => con.Performer.OrgName.Contains(name, StringComparison.InvariantCultureIgnoreCase),
                pageNumber, pageSize, sortBy, isAscending);

        public (List<Contract>, int) GetContractsByConclusionDate(DateOnly? dateStart, DateOnly? dateEnd, int pageNumber, int pageSize, string sortBy, bool isAscending) =>
            GetContractsByValue(con => con.ConclusionDate > dateStart && con.ConclusionDate < dateEnd,
                pageNumber, pageSize, sortBy, isAscending);

        public (List<Contract>, int) GetContractsByExpirationDate(DateOnly? dateStart, DateOnly? dateEnd, int pageNumber, int pageSize, string sortBy, bool isAscending) =>
            GetContractsByValue(con => con.ExpirationDate > dateStart && con.ExpirationDate < dateEnd,
                pageNumber, pageSize, sortBy, isAscending);

        public (List<Contract>, int) GetContractsByDefault(string useless, int pageNumber, int pageSize, string sortBy, bool isAscending) =>
            GetContractsByValue(con => { return true; }, pageNumber, pageSize, sortBy, isAscending);

        public (List<Contract>, int) GetContractsByDefault(DateOnly? dateStart, DateOnly? dateEnd, int pageNumber, int pageSize, string sortBy, bool isAscending) =>
            GetContractsByValue(con => { return true; }, pageNumber, pageSize, sortBy, isAscending);
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