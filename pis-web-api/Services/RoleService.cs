using pis.Models;
using pis.Repositorys;

namespace pis_web_api.Services
{
    public class RoleService : Service<Role>
    {
        private RoleRepository _repositoryRole;
        public RoleService() 
        {
            _repositoryRole = new RoleRepository();
            _repository = _repositoryRole;
        }    

        public (List<Role>, int) GetRoles(string filterValue, int pageNumber, int pageSize)
        {
            return _repositoryRole.GetRolesByName(filterValue, pageNumber, pageSize);
        }
    }
}
