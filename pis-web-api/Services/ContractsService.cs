using System;
using pis.Models;
using pis.Repositorys;

namespace pis.Services
{
    public class ContractsService
    {
        private static List<Contract> Contracts { get; set; } = new List<Contract>();

        public static bool CreateContract(Contract contract)
        {
            bool status = ContractsRepository.CreateContract(contract);
            return status;
        }

        public static bool DeleteEntry(int id)
        {
            bool status = ContractsRepository.DeleteContract(ContractsRepository.GetContractById(id));
            return status;
        }

        public static List<Contract>? GetContracts(string filterField, string? filterValue, string sortBy,
            bool isAscending, int pageNumber, int pageSize)
        {
            filterValue = filterValue?.ToLower();

            // Применение фильтрации в зависимости от поля
            if (!string.IsNullOrEmpty(filterField) && !string.IsNullOrEmpty(filterValue))
            {
                switch (filterField.ToLower())
                {
                    case "customer":
                        Contracts = ContractsRepository.GetContractsByOrganisationName(filterValue).ToList();
                        break;
                    case "performer":
                        Contracts = ContractsRepository.GetContractsByOrganisationName(filterValue).ToList();
                        break;
                    case "conclusiondate":
                        Contracts = ContractsRepository.GetContractsByDate(DateOnly.Parse(filterValue)).ToList();
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
                        Contracts = isAscending
                            ? Contracts.OrderBy(c => c.IdContract).ToList()
                            : Contracts.OrderByDescending(c => c.IdContract).ToList();
                        break;
                    case "ConclusionDate":
                        Contracts = isAscending
                            ? Contracts.OrderBy(c => c.ConclusionDate).ToList()
                            : Contracts.OrderByDescending(c => c.ConclusionDate).ToList();
                        break;
                    case "ExpirationDate":
                        Contracts = isAscending
                            ? Contracts.OrderBy(c => c.ExpirationDate).ToList()
                            : Contracts.OrderByDescending(c => c.ExpirationDate).ToList();
                        break;
                    case "Performer":
                        Contracts = isAscending
                            ? Contracts.OrderBy(c => c.Performer.OrgName).ToList()
                            : Contracts.OrderByDescending(c => c.Performer.OrgName).ToList();
                        break;
                    case "Customer":
                        Contracts = isAscending
                            ? Contracts.OrderBy(c => c.Customer).ToList()
                            : Contracts.OrderByDescending(c => c.Customer).ToList();
                        break;
                }
            }

            // Пагинация
            var contractsPag = Contracts.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return contractsPag;
        }

        public static int GetTotalContracts(string filterField, string? filterValue)
        {
            return Contracts.Count();
        }

        public static Contract? GetEntry(int id)
        {
            var entry = ContractsRepository.GetContractById(id);
            return entry;
        }

        public static bool ChangeEntry(Contract contracts)
        {
            bool status = ContractsRepository.UpdateContract(contracts);
            return status;
        }

        public ContractsService()
        {
        }
    }
}