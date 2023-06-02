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
            if (status)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool DeleteEntry(int id)
        {
            bool status = ContractsRepository.DeleteEntry(id);
            if (status)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static List<Contracts>? GetContracts()
        {
            var entry = ContractsRepository.GetContracts();
            return entry;

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

