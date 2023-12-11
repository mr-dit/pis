using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using pis_web_api.Models.get;
using pis_web_api.Models.post;
using pis_web_api.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pis_web_api.Models.db.ReportStates
{
    public abstract class ReportStatusAbstract
    {
        abstract public string Name { get; set; }

        protected Report Report;
        protected RoleService _roleService;
        abstract public List<Role> AccesRoles { get; }

        public ReportStatusAbstract() 
        {
            _roleService = new RoleService();
        }

        public ReportStatusAbstract(Report report)
        {
            Report = report;
            _roleService = new RoleService();
        }

        protected void Work(UserPost user, List<Role> rolesAcces, Action work)
        {
            if (_roleService.IsUserHasRole(user, rolesAcces))
            {
                work();
            }
            else
            {
                throw new UnauthorizedAccessException("Недостаточно прав!");
            }
        }

        abstract public void Confirm(UserPost user);

        abstract public void Cancel(UserPost user);
    }
}
