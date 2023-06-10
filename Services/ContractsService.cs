using System;
using pis.Models;
using pis.Repositorys;

namespace pis.Services
{
    public class ContractsService
    {
        public static bool CreateContract(Contracts contracts)
        {
            bool status = ContractsRepository.CreateContracts(contracts);
            return status;
        }

        public static bool DeleteEntry(int id)
        {
            bool status = ContractsRepository.DeleteEntry(id);
            return status;
        }

        public static List<Contracts>? GetContracts(string filterField, string? filterValue, string sortBy,
            bool isAscending, int pageNumber, int pageSize)
        {
            filterValue = filterValue?.ToLower();

            var contracts = ContractsRepository.GetContracts();

            // Применение фильтрации в зависимости от поля
            if (!string.IsNullOrEmpty(filterField) && !string.IsNullOrEmpty(filterValue))
            {
                switch (filterField.ToLower())
                {
                    case "customer":
                        contracts = contracts.Where(c => c.Customer.ToLower().Contains(filterValue)).ToList();
                        break;
                    case "performer":
                        contracts = contracts.Where(c => c.Performer.OrgName.ToLower().Contains(filterValue)).ToList();
                        break;
                    case "conclusiondate":
                        contracts = contracts
                            .Where(c => c.ConclusionDate.ToShortDateString().ToLower().Contains(filterValue)).ToList();
                        break;
                    // Добавьте остальные варианты полей
                    default:
                        break;
                }
            }

            // Сортировка
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "ContractsId":
                        contracts = isAscending
                            ? contracts.OrderBy(c => c.ContractsId).ToList()
                            : contracts.OrderByDescending(c => c.ContractsId).ToList();
                        break;
                    case "ConclusionDate":
                        contracts = isAscending
                            ? contracts.OrderBy(c => c.ConclusionDate).ToList()
                            : contracts.OrderByDescending(c => c.ConclusionDate).ToList();
                        break;
                    case "ExpirationDate":
                        contracts = isAscending
                            ? contracts.OrderBy(c => c.ExpirationDate).ToList()
                            : contracts.OrderByDescending(c => c.ExpirationDate).ToList();
                        break;
                    case "Performer":
                        contracts = isAscending
                            ? contracts.OrderBy(c => c.Performer.OrgName).ToList()
                            : contracts.OrderByDescending(c => c.Performer.OrgName).ToList();
                        break;
                    case "Customer":
                        contracts = isAscending
                            ? contracts.OrderBy(c => c.Customer).ToList()
                            : contracts.OrderByDescending(c => c.Customer).ToList();
                        break;
                }
            }

            // Пагинация
            contracts = contracts.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return contracts;
        }

        public static int GetTotalContracts(string filterField, string? filterValue)
        {
            filterValue = filterValue?.ToLower();

            var contracts = ContractsRepository.GetContracts();

            // Применение фильтрации в зависимости от поля
            if (!string.IsNullOrEmpty(filterField) && !string.IsNullOrEmpty(filterValue))
            {
                switch (filterField.ToLower())
                {
                    case "customer":
                        contracts = contracts.Where(c => c.Customer.ToLower().Contains(filterValue)).ToList();
                        break;
                    case "performer":
                        contracts = contracts.Where(c => c.Performer.OrgName.ToLower().Contains(filterValue)).ToList();
                        break;
                    case "conclusiondate":
                        contracts = contracts
                            .Where(c => c.ConclusionDate.ToShortDateString().ToLower().Contains(filterValue)).ToList();
                        break;
                    // Добавьте остальные варианты полей
                    default:
                        break;
                }
            }

            return contracts.Count;
        }

        public static Contracts? GetEntry(int id)
        {
            var entry = ContractsRepository.GetEntry(id);
            return entry;
        }

        public static bool ChangeEntry(Contracts contracts)
        {
            bool status = ContractsRepository.ChangeEntry(contracts);
            return status;
        }

        public ContractsService()
        {
        }
    }
}