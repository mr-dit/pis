using System;
using pis.Repositorys;
using pis_web_api.Models.db;
using pis_web_api.Services;

namespace pis.Services
{
    public class ContractService : Service<Contract>
    {
        private ContractRepository _contractRepository;

        public ContractService() 
        {
            _contractRepository = new ContractRepository();
            _repository = _contractRepository;
        }

        public (List<Contract>, int) GetContractsByFilter(string filterField, string filterValue, string sortBy,
            bool isAscending, int pageNumber, int pageSize)
        {
            var filterFields = new Dictionary<string, Func<string, int, int, string, bool, (List<Contract>, int)>>(StringComparer.InvariantCultureIgnoreCase)
            {
                [nameof(Contract.Customer)] = _contractRepository.GetContractsByCustomerName,

                [nameof(Contract.Performer)] = _contractRepository.GetContractsByPerformerName,

                [""] = _contractRepository.GetContractsByDefault
            };
            return filterFields[filterField](filterValue, pageNumber, pageSize, sortBy, isAscending);
        }

        public (List<Contract>, int) GetContractsByDate(string filterField, DateOnly? startDateFilter, DateOnly? endDateFilter, string sortBy,
            bool isAscending, int pageNumber, int pageSize)
        {
            var filterFields = new Dictionary<string, Func<DateOnly?, DateOnly?, int, int, string, bool, (List<Contract>, int)>>(StringComparer.InvariantCultureIgnoreCase)
            {
                [nameof(Contract.ConclusionDate)] = _contractRepository.GetContractsByConclusionDate,

                [nameof(Contract.ExpirationDate)] = _contractRepository.GetContractsByExpirationDate,

                [""] = _contractRepository.GetContractsByDefault
            };

            startDateFilter ??= DateOnly.MinValue; 
            endDateFilter ??= DateOnly.MaxValue;

            return filterFields[filterField](startDateFilter, endDateFilter, pageNumber, pageSize, sortBy, isAscending);
        }
    }
}