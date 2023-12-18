using Microsoft.EntityFrameworkCore;
using pis_web_api.Models.db;
using pis_web_api.Repositorys;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace pis.Repositorys
{
    public class UserRepository : Repository<User>
    {
        public UserRepository() : base()
        {
        }

        private delegate void UserAction(Context db, User user);

        private (List<User>, int) GetUsersByValue(Func<User, bool> value, int pageNumber, int pageSize, string sortBy, bool isAscending)
        {
            using (Context db = new Context())
            {
                var allUser = db.Users
                    .Include(x => x.Organisation)
                    .Where(value)
                    .SortBy(sortBy, isAscending);
                var users = allUser.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                return (users, allUser.Count());
            }
        }

        public (List<User>, int) GetUsersBySurname(
            string surname, int pageNumber,
            int pageSize, string sortBy, bool isAscending) =>
            GetUsersByValue(user => user.Surname.Contains(surname, StringComparison.InvariantCultureIgnoreCase),
                pageNumber, pageSize, sortBy, isAscending);

        public (List<User>, int) GetUsersByFirstName(
            string surname, int pageNumber,
            int pageSize, string sortBy, bool isAscending) =>
            GetUsersByValue(user => user.FirstName.Contains(surname, StringComparison.InvariantCultureIgnoreCase),
                pageNumber, pageSize, sortBy, isAscending);

        public (List<User>, int) GetUsersByLastName(
            string surname, int pageNumber,
            int pageSize, string sortBy, bool isAscending) =>
            GetUsersByValue(user => user.LastName.Contains(surname, StringComparison.InvariantCultureIgnoreCase),
                pageNumber, pageSize, sortBy, isAscending);

        public (List<User>, int) GetUsersByOrganisationName(
            string orgName, int pageNumber,
            int pageSize, string sortBy, bool isAscending) =>
            GetUsersByValue(user => user.Organisation.OrgName.Contains(orgName, StringComparison.InvariantCultureIgnoreCase),
                pageNumber, pageSize, sortBy, isAscending);

        public (List<User>, int) GetUsersByDefault(
           string surname, int pageNumber,
           int pageSize, string sortBy, bool isAscending) =>
           GetUsersByValue(user => { return true; },
               pageNumber, pageSize, sortBy, isAscending);

        public List<User> GetUsersByOrganisation(int orgId)
        {
            using (Context db = new Context())
            {
                var users = db.Users
                    .Include(x => x.Organisation)
                    .Where(x => x.OrganisationId == orgId);
                return users.ToList();
            }
        }

        public override User GetById(int id)
        {
            using (Context db = new Context())
            {
                var user = db.Users
                    .Where(x => x.IdUser == id)
                    .Include(x => x.Organisation)
                    .Include(x => x.Roles)
                    .Single();
                return user;
            }
        }

        public User GetUserByLogin(string login, string password)
        {
            using (Context db = new Context())
            {
                var user = db.Users
                    .Where(x => x.Login == login)
                    .Where(x => x.Password == password)
                    .FirstOrDefault();

                if(user is null)
                {
                    throw new Exception("Неверный логин или пароль");
                }

                return user;
            }
        }
    }

    static class UserExtension
    {
        public static IEnumerable<User> SortBy(this IEnumerable<User> users, string sortBy, bool isAscending)
        {
            var sortingFields = new Dictionary<string, Func<IEnumerable<User>, bool, IOrderedEnumerable<User>>>(StringComparer.InvariantCultureIgnoreCase)
            {
                [nameof(User.Surname)] = (users, isAscending) =>
                    isAscending ? users.OrderBy(a => a.Surname)
                    : users.OrderByDescending(a => a.Surname),

                [nameof(User.FirstName)] = (users, isAscending) =>
                    isAscending ? users.OrderBy(a => a.FirstName)
                    : users.OrderByDescending(a => a.FirstName),

                [nameof(User.LastName)] = (users, isAscending) =>
                    isAscending ? users.OrderBy(a => a.LastName)
                    : users.OrderByDescending(a => a.LastName),

                [nameof(User.Organisation)] = (users, isAscending) =>
                    isAscending ? users.OrderBy(a => a.Organisation.OrgName)
                    : users.OrderByDescending(a => a.Organisation.OrgName)
            };

            var sortingMethod = sortingFields[sortBy];

            return sortingMethod(users, isAscending);
        }
    }
}
