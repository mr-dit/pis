using pis.Models;
using pis;
using NUnit.Framework.Constraints;

namespace pis.Repositorys
{
    public class UserRoleRepository
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
