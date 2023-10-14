using Microsoft.EntityFrameworkCore;
using pis.Models;

namespace pis.Repositorys
{
    public class UserRepository
    {
        public static void AddUser(User user)
        {
            using (var db = new Context())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public static void UpdateUser(User user)
        {
            using (var db = new Context())
            {
                db.Users.Update(user);
                db.SaveChanges();
            }
        }

        public static void DeleteUser(User user)
        {
            using(var db = new Context())
            {
                db.Users.Remove(user);
                db.SaveChanges();  
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
    }
}
