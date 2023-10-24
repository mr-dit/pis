using pis.Models;
using pis.Repositorys;

namespace pis_web_api.Services
{
    public class RoleService : Service<Role>
    {
        private RoleRepository repository;
        public RoleService() 
        {
            repository = new RoleRepository();
        }    

        public (List<Role>, int) GetRoles(string filterValue, int pageNumber, int pageSize)
        {
            return repository.GetRolesByName(filterValue, pageNumber, pageSize);
        }
    }
}
