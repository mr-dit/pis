using pis;
using NUnit.Framework.Constraints;
using pis_web_api.Models.db;
using pis_web_api.Repositorys;

namespace pis.Repositorys
{
    public class UserRoleRepository : Repository<UserRole>
    {
        public static bool Create(UserRole userRole)
        {
            using (Context db = new Context())
            {
                try
                {
                    db.UserRole.Add(userRole);
                }
                catch (Exception)
                {
                    throw new Exception();
                    //return false;
                }
                db.SaveChanges();
                return true;
            }
        }
    }
}
