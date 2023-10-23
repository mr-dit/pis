using pis.Models;
using pis.Repositorys;

namespace pis_web_api.Services
{
    public class LocalityService
    {
        private LocalityRepository _localityRepository;

        public LocalityService()
        {
            _localityRepository = new LocalityRepository();
        }

        public bool FillData(Locality locality)
        {
            bool status = _localityRepository.Add(locality);
            return status;
        }

        public bool DeleteEntry(int id)
        {
            bool status = _localityRepository.Remove(_localityRepository.GetById(id));
            return status;
        }

        public Locality GetEntry(int id)
        {
            var entry = _localityRepository.GetById(id);
            return entry;
        }

        public (List<Locality>, int) GetLocalities(string filterValue, int pageNumber, int pageSize)
        {
            return _localityRepository.GetLocalitiesByName(filterValue, pageNumber, pageSize);
        }


        public bool ChangeEntry(Locality locality)
        {
            bool status = _localityRepository.Update(locality);
            return status;
        }
    }
}
