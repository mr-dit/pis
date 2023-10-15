using Microsoft.EntityFrameworkCore;
using pis.Models;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace pis.Repositorys
{
    public class UserRepository
    {
        public static bool AddUser(User user)
        {
            using (var db = new Context())
            {
                try
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
                
            }
        }

        public static bool UpdateUser(User user)
        {
            using (var db = new Context())
            {
                try
                {
                    db.Users.Update(user);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }

        public static bool DeleteUser(User user)
        {
            using(var db = new Context())
            {
                try
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return false;
            }
        }

        public static User GetUserById(int id)
        {
            using (var db = new Context())
            {
                var user = db.Users
                    .Where(x => x.IdUser == id)
                    .Include(x => x.Organisation)
                    .Include(x => x.Post)
                    .Single();
                if (user == null)
                    throw new ArgumentNullException($"Не существует пользователя с id {id}");
                return user;
            }
        }

        public static (List<User>, int) GetUsersByFunc(Expression<Func<User, bool>> filterFunc, string name, int pageNumber, int pageSize, string sortBy, bool isAscending)
        {
            using (var db = new Context())
            {
                var allUsers = db.Users
                .Where(filterFunc)
                .SortBy(sortBy, isAscending);

                var users = allUsers
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

                var count = allUsers.Count();

                return (users, count);
            }
        }

        public static (List<User>, int) GetUsersByFirstName(string name, int pageNumber, int pageSize, string sortBy, bool isAscending) =>
            GetUsersByFunc((x => string.IsNullOrEmpty(name) || x.FirstName.Contains(name)), name, pageNumber, pageSize, sortBy, isAscending);

        public static (List<User>, int) GetUsersBySurname(string name, int pageNumber, int pageSize, string sortBy, bool isAscending) =>
            GetUsersByFunc((x => string.IsNullOrEmpty(name) || x.Surname.Contains(name)), name, pageNumber, pageSize, sortBy, isAscending);

        public static (List<User>, int) GetUsersByLastName(string name, int pageNumber, int pageSize, string sortBy, bool isAscending) =>
            GetUsersByFunc((x => string.IsNullOrEmpty(name) || x.LastName.Contains(name)), name, pageNumber, pageSize, sortBy, isAscending);

        public static (List<User>, int) GetUsersByPost(string name, int pageNumber, int pageSize, string sortBy, bool isAscending) =>
            GetUsersByFunc((x => string.IsNullOrEmpty(name) || x.Post.NamePost.Contains(name)), name, pageNumber, pageSize, sortBy, isAscending);

        public static (List<User>, int) GetUsersByOrganisation(string name, int pageNumber, int pageSize, string sortBy, bool isAscending) =>
            GetUsersByFunc((x => string.IsNullOrEmpty(name) || x.Post.NamePost.Contains(name)), name, pageNumber, pageSize, sortBy, isAscending);
    }

    static class UserExtension
    {
        public static IQueryable<User> SortBy(this IQueryable<User> users, string sortBy, bool isAscending)
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case nameof(User.Surname):
                        users = isAscending ? users.OrderBy(a => a.Surname) : users.OrderByDescending(a => a.Surname);
                        break;
                    case nameof(User.FirstName):
                        users = isAscending ? users.OrderBy(a => a.FirstName) : users.OrderByDescending(a => a.FirstName);
                        break;
                    case nameof(User.LastName):
                        users = isAscending ? users.OrderBy(a => a.LastName) : users.OrderByDescending(a => a.LastName);
                        break;
                    case nameof(User.Organisation):
                        users = isAscending ? users.OrderBy(a => a.Organisation.OrgName) : users.OrderByDescending(a => a.Organisation.OrgName);
                        break;
                    case nameof(User.Post):
                        users = isAscending ? users.OrderBy(a => a.Post.NamePost) : users.OrderByDescending(a => a.Post.NamePost);
                        break;
                }
            }
            return users;
        }
    }
}
