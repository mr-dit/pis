using pis.Repositorys;
using pis_web_api.Models.db;
using pis_web_api.Models.get;
using pis_web_api.Models.post;
using pis_web_api.References;

namespace pis_web_api.Services
{
    public class RoleService : Service<Role>
    {
        private RoleRepository _repositoryRole;

        public List<Role> OMSU_Roles 
        { 
            get
            {
                return new List<Role>() { 
                    RolesReferences.SIGNER_OMSU, 
                    RolesReferences.OPERATOR_OMSU, 
                    RolesReferences.KURATOR_OMSU, 
                    RolesReferences.ADMIN };
            }
        }

        public List<Role> Vetclinic_Roles
        {
            get
            {
                return new List<Role>() { 
                    RolesReferences.SIGNER_VETSERVICE, 
                    RolesReferences.KURATOR_VETSERVICE,
                    RolesReferences.OPERATOR_VETSERVICE, 
                    RolesReferences.DOCTOR,
                    RolesReferences.DOCTOR_SHELTER,
                    RolesReferences.ADMIN };
            }
        }

        public RoleService() 
        {
            _repositoryRole = new RoleRepository();
            _repository = _repositoryRole;
        }    

        public (List<Role>, int) GetRoles(string filterValue, int pageNumber, int pageSize) =>
            _repositoryRole.GetRolesByName(filterValue, pageNumber, pageSize);

        public bool UserIsOmsu(UserPost user)
        {
            return user.Roles.Intersect(OMSU_Roles.Select(x => x.IdRole)).Count() != 0;
        }

        public bool UserIsVet(UserPost user)
        {
            return user.Roles.Intersect(Vetclinic_Roles.Select(x => x.IdRole)).Count() != 0;
        }

        public bool IsUserHasRole(UserPost user, List<Role> roles)
        {
            return user.Roles.Intersect(roles.Select(x => x.IdRole)).Count() != 0;
        }

        public bool IsUserHasRole(UserGet user, List<Role> roles)
        {
            return user.Roles.Intersect(roles.Select(x => x.IdRole)).Count() != 0;
        }
    }
}
