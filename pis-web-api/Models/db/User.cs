using pis.Repositorys;
using pis_web_api.Models;
using pis_web_api.Services;
using System.ComponentModel.DataAnnotations;

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

        public Organisation? Organisation { get; set; }

        public List<UserRole>? Roles { get; set; }

        public User() { }

        public User(string surName, string firstName, string lastName, int organizationId)
        {
            Surname = surName;
            FirstName = firstName;
            LastName = lastName;
            OrganisationId = organizationId;
        }

        public bool AddRoles(params Role[] roles)
        {
            var unique = roles.GroupBy(x => x.IdRole).Select(x => x.Key);

            if (unique.Count() != roles.Count())
                return false;

            foreach (var role in roles)
            {
                var userRole = new UserRole(this, role);
                UserRoleRepository.Create(userRole);
            }
            new UserRepository().Update(this);
            //Roles ??= new List<Role>();
            //Roles.AddRange(roles);

            return true;
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
        //public User(int idUser, string firstName, string lastName, string surname, Post post, Organisation organisation, List<Vaccination> vaccinations)
        //{
        //    IdUser = idUser;
        //    FirstName = firstName;
        //    LastName = lastName;
        //    Surname = surname;
        //    Post = post;
        //    Organisation = organisation;
        //    Vaccinations = vaccinations;
        //}
    }
}
