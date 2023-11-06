using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Npgsql.PostgresTypes;
using pis_web_api.Models.db;
using pis_web_api.Repositorys;
using System.Globalization;

namespace pis.Repositorys
{
    public class RoleRepository : Repository<Role>
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

        public (List<Role>, int) GetRolesByName(string filterValue, int pageNumber, int pageSize)
        {
            using (Context db = new Context())
            {
                var allUser = db.Roles
                    .AsEnumerable()
                    .Where(x => x.NameRole.Contains(filterValue, StringComparison.InvariantCultureIgnoreCase))
                    .OrderBy(x => x.NameRole);
                var users = allUser.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                return (users, allUser.Count());
            }
        }
    }
}
