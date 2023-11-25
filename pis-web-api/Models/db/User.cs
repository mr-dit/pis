using NUnit.Framework;
using pis.Repositorys;
using pis_web_api.Models;
using pis_web_api.Models.post;
using pis_web_api.Services;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace pis_web_api.Models.db
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }

        public string Surname { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int OrganisationId { get; set; }

        public string Login { get; set; }
        
        public string Password { get; set; }

        public Organisation? Organisation { get; set; }

        public List<UserRole>? Roles { get; set; }

        public User() { }

        public User(string surName, string firstName, string lastName, int organizationId, string login, string password)
        {
            Surname = surName;
            FirstName = firstName;
            LastName = lastName;
            OrganisationId = organizationId;
            Login = login;
            Password = password;
        }

        public bool AddRoles(params Role[] roles)
        {
            var unique = roles.GroupBy(x => x.IdRole).Select(x => x.Key);

            if (unique.Count() != roles.Count())
                return false;
            if (IdUser == 0)
                new UserRepository().Add(this);

            foreach (var role in roles)
            {
                Roles ??= new List<UserRole>();
                if (Roles.Select(x => x.RoleId).Contains(role.IdRole))
                    continue;
                var userRole = new UserRole(this, role);
                UserRoleRepository.Create(userRole);
            }
            new UserRepository().Update(this);
            return true;
        }

        public bool AddRoles(params int[] rolesIds)
        {
            var repos = new RoleRepository();
            var roles = new List<Role>();
            foreach (var roleId in rolesIds)
            {
                roles.Add(repos.GetById(roleId));
            }
            return AddRoles(roles.ToArray());
        }

        public bool IsDoctor()
        {
            if (Roles is null)
                throw new Exception("Нет ролей у пользователя");
            var roles = Roles.Select(x => x.RoleId);
            return roles.Any(x => x == 13 || x == 14);
        }

        public override bool Equals(object? obj)
        {
            if (!(obj is User user) || obj == null) return false;
            var compUser = (User)obj;

            return IdUser == compUser.IdUser;
        }

        public void Update(UserPost userPost)
        {
            var repos = new UserRoleRepository();
            Surname = userPost.Surname;
            FirstName = userPost.FirstName;
            LastName = userPost.LastName;
            OrganisationId = userPost.OrganisationId;

            var rolesNotInUser = userPost.Roles.Where(x => !Roles.Select(x => x.RoleId).Contains(x));
            var rolesNotInPostUser = new List<UserRole>();

            foreach (var role in Roles)
            {
                var roleId = role.RoleId;
                if (!userPost.Roles.Contains(roleId))
                    rolesNotInPostUser.Add(role);
            }

            foreach (var role in rolesNotInPostUser)
            {
                Roles.Remove(role);
                repos.Remove(role);
            }

            this.AddRoles(rolesNotInUser.ToArray());
        }

        public UserPost ConvertToUserPost()
        {
            UserRoleRepository roleRepository = new UserRoleRepository();
            var roles = roleRepository.db.UserRole
                .Where(x => x.UserId == this.IdUser)
                .Select(x => x.RoleId)
                .ToList();
            var userPost = new UserPost(Surname, FirstName, LastName, OrganisationId, Login, Password, roles);
            return userPost;
        }
    }
}
