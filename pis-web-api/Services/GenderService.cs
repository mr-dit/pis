using pis.Repositorys;
using pis_web_api.Models.db;

namespace pis_web_api.Services
{
    public class GenderService : Service<Gender>
    {
        private GenderRepository _genderRepository;

        public GenderService()
        {
            _genderRepository = new GenderRepository();
            _repository = _genderRepository;
        }

        public List<Gender> GetGenders()
        {
            return _genderRepository.GetGenders();
        }
    }
}
