using NUnit.Framework;
using pis.Models;
using pis.Repositorys;

namespace pis_web_api.Services
{
    public class VaccineService : Service<Vaccine>
    {
        private VaccineRepository _vaccineRepository;
        public VaccineService() 
        {
            _vaccineRepository = new VaccineRepository();
            _repository = _vaccineRepository;
        }

        public (List<Vaccine>, int) GetVaccines(string filterField, string filterValue, string sortBy, bool isAscending, int pageNumber, int pageSize)
        {
            var filterFields = new Dictionary<string, Func<string, int, int, string, bool, (List<Vaccine>, int)>>(StringComparer.InvariantCultureIgnoreCase)
            {
                [nameof(Vaccine.NameVaccine)] = _vaccineRepository.GetVaccinesByName,

                [""] = _vaccineRepository.GetVaccinesByDefault
            };

            return filterFields[filterField](filterValue, pageNumber, pageSize, sortBy, isAscending);
        }
    }
}
