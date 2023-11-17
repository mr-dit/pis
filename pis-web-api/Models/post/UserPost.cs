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

        public List<int> Roles { get; set; }

        public User ConvertToUser()
        {
            var user = new User(Surname, FirstName, LastName, OrganisationId);
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
