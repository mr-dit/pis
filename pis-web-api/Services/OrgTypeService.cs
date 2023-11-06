using pis.Repositorys;
using pis_web_api.Models.db;

namespace pis_web_api.Services
{
    public class OrgTypeService : Service<OrgType>
    {
        private OrgTypeRepository _orgTypeRepository;
        public OrgTypeService()
        {
            _orgTypeRepository = new OrgTypeRepository();
            _repository = _orgTypeRepository;
        }

        public (List<OrgType>, int) GetOrgTypes(string filterValue, int pageNumber, int pageSize)
        {
            return _orgTypeRepository.GetOrgTypesByName(filterValue, pageNumber, pageSize);
        }
    }
}
