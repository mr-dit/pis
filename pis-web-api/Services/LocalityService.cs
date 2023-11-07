using pis.Repositorys;
using pis_web_api.Models.db;

namespace pis_web_api.Services
{
    public class LocalityService : Service<Locality>
    {
        private LocalityRepository _localityRepository;

        public LocalityService()
        {
            _localityRepository = new LocalityRepository();
            _repository = _localityRepository;
        }

        public (List<Locality>, int) GetLocalities(string filterValue, int pageNumber, int pageSize)
        {
            return _localityRepository.GetLocalitiesByName(filterValue, pageNumber, pageSize);
        }
    }
}
