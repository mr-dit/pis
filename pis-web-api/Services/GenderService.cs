using pis.Models;
using pis.Repositorys;

namespace pis_web_api.Services
{
    public class GenderService
    {
        private GenderRepository _genderRepository;

        public GenderService()
        {
            _genderRepository = new GenderRepository();
        }

        public bool FillData(Gender gender)
        {
            bool status = _genderRepository.Add(gender);
            return status;
        }

        public Gender GetEntry(int id)
        {
            var entry = _genderRepository.GetById(id);
            return entry;
        }

        public List<Gender> GetGenders()
        {
            return _genderRepository.GetGenders();
        }
    }
}
