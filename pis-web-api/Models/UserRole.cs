using pis.Models;
using System.ComponentModel.DataAnnotations;

namespace pis.Models
{
    public class UserRole
    {
        [Key]
        public int UserRoleId { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public UserRole() { }
        public UserRole(User user, Role role)
        {
            UserId = user.IdUser;
            RoleId = role.IdRole;
        }
    }
}
