using pis.Repositorys;
using pis_web_api.Models.db;

namespace pis_web_api.Models.get
{
    public class UserGet
    {
        public int IdUser { get; set; }

        public string Surname { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int OrganisationId { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public List<int> Roles { get; set; }

        public UserGet() { }

        public UserGet(int userId, string surname, string firstName, string lastName,
            int orgId, string login, string password, List<int> roles)
        {
            IdUser = userId;
            Surname = surname;
            FirstName = firstName;
            LastName = lastName;
            OrganisationId = orgId;
            Login = login;
            Password = password;
            Roles = new List<int>();
            Roles.AddRange(roles);
        }
    }
}
