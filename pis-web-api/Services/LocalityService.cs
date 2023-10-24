using pis.Models;
using pis.Repositorys;

namespace pis_web_api.Services
{
    public class LocalityService : Service<Locality>
    {
        private LocalityRepository _localityRepository;

        public LocalityService()
        {
            _localityRepository = new LocalityRepository();
            _repository = new LocalityRepository();
        }

        public (List<Locality>, int) GetLocalities(string filterValue, int pageNumber, int pageSize)
        {
            return _localityRepository.GetLocalitiesByName(filterValue, pageNumber, pageSize);
        }
    }
}
