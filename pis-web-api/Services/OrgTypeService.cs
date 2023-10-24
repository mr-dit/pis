using pis.Models;
using pis.Repositorys;

namespace pis_web_api.Services
{
    public class OrgTypeService
    {
        private OrgTypeRepository _orgTypeRepository;
        public OrgTypeService()
        {
            _orgTypeRepository = new OrgTypeRepository();
        }

        public bool FillData(OrgType orgType)
        {
            bool status = _orgTypeRepository.Add(orgType);
            return status;
        }

        public bool DeleteEntry(int id)
        {
            bool status = _orgTypeRepository.Remove(_orgTypeRepository.GetById(id));
            return status;
        }

        public OrgType GetEntry(int id)
        {
            var entry = _orgTypeRepository.GetById(id);
            return entry;
        }

        public (List<OrgType>, int) GetOrgTypes(string filterValue, int pageNumber, int pageSize)
        {
            return _orgTypeRepository.GetOrgTypesByName(filterValue, pageNumber, pageSize);
        }


        public bool ChangeEntry(OrgType orgType)
        {
            bool status = _orgTypeRepository.Update(orgType);
            return status;
        }
    }
}
