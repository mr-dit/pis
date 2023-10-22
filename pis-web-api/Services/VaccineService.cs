using NUnit.Framework;
using pis.Models;
using pis.Repositorys;

namespace pis_web_api.Services
{
    public class VaccineService
    {
        private VaccineRepository _vaccineRepository;
        public VaccineService() 
        {
            _vaccineRepository = new VaccineRepository();
        }

        public bool FillData(Vaccine vaccine)
        {
            bool status = _vaccineRepository.Add(vaccine);
            return status;
        }

        public bool DeleteEntry(int id)
        {
            bool status = _vaccineRepository.Remove(_vaccineRepository.GetById(id));
            return status;
        }

        public Vaccine? GetEntry(int id)
        {
            var entry = _vaccineRepository.GetById(id);
            return entry;
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


        public bool ChangeEntry(Vaccine animal)
        {
            bool status = _vaccineRepository.Update(animal);
            return status;
        }
    }
}
