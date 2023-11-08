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

        public (List<Contract>, int) GetContracts(DateOnly startDateFilter, DateOnly endDateFilter, string filterValue, string filterField,
            string sortBy, bool isAscending, int pageNumber, int pageSize)
        {
            var filterFields = new Dictionary<string, Func<DateOnly, DateOnly, string, int, int, string, bool, (List<Contract>, int)>>(StringComparer.InvariantCultureIgnoreCase)
            {
                [nameof(Contract.Customer)] = _contractRepository.GetContractsByCustomerName,

                [nameof(Contract.Performer)] = _contractRepository.GetContractsByPerformerName,

                [""] = _contractRepository.GetContractsByDefault
            };
            return filterFields[filterField](startDateFilter, endDateFilter, filterValue, pageNumber, pageSize, sortBy, isAscending);
        }
    }
}