using pis.Repositorys;
using pis_web_api.Models.db;

namespace pis_web_api.Models.post
{
    public class UserPost
    {
        public string Surname { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int OrganisationId { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public List<int> Roles { get; set; }

        public UserPost() { }

        public UserPost(string surname, string firstName, string lastName,
            int orgId, string login, string password, List<int> roles)
        {
            Surname = surname;
            FirstName = firstName;
            LastName = lastName;
            OrganisationId = orgId;
            Login = login;
            Password = password;
            Roles = new List<int>();
            Roles.AddRange(roles);
        }

        public User ConvertToUser()
        {
            var user = new User(Surname, FirstName, LastName, OrganisationId, Login, Password);
            user.AddRoles(Roles.ToArray());
            return user;
        }

        public User ConvertToUserWithId(int id)
        {
            var user = new UserRepository().GetById(id);
            user.Update(this);
            return user;
        }
    }
}
