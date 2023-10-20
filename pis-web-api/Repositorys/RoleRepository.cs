using Npgsql.PostgresTypes;
using pis.Models;

namespace pis.Repositorys
{
    public class RoleRepository
    {
        public static Role GetRoleByName(string name)
        {
            using (var db = new Context())
            {
                var post = db.Roles.Where(x => x.NameRole == name).Single();
                if (post == null)
                    throw new ArgumentException($"Не существует должности с названием {name}");
                return post;
            }
        }

        public static void AddRole(Role role)
        {
            using (var db = new Context())
            {
                db.Roles.Add(role);
                db.SaveChanges();
            }
        }

        public static void DeleteRole(Role role)
        {
            using (var db = new Context())
            {
                db.Roles.Remove(role);
                db.SaveChanges();
            }
        }

        public static void UpdateRole(Role role)
        {
            using (var db = new Context())
            {
                db.Roles.Update(role);
                db.SaveChanges();
            }
        }
    }
}
