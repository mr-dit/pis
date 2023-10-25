using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using pis.Models;
using pis.Services;
using pis_web_api.Repositorys;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace pis.Repositorys
{
    public class VaccineRepository : Repository<Vaccine>
    {
        public VaccineRepository() : base()
        {
        }

        private delegate void VaccineAction(Context db, Vaccine user);

        private (List<Vaccine>, int) GetVaccinesByValue(Func<Vaccine, bool> value, int pageNumber, int pageSize, string sortBy, bool isAscending)
        {
            using (Context db = new Context())
            {
                var allUser = db.Vaccines
                    .Where(value)
                    .SortBy(sortBy, isAscending);
                var users = allUser.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                return (users, allUser.Count());
            }
        }

        public (List<Vaccine>, int) GetVaccinesByDefault(string useless, int pageNumber,
            int pageSize, string sortBy, bool isAscending) =>
            GetVaccinesByValue(vaccine => { return true; }, pageNumber, pageSize, sortBy, isAscending);

        public (List<Vaccine>, int) GetVaccinesByName(string name, int pageNumber,
            int pageSize, string sortBy, bool isAscending) =>
            GetVaccinesByValue(vaccine => vaccine.NameVaccine.Contains(name, StringComparison.InvariantCultureIgnoreCase),
                pageNumber, pageSize, sortBy, isAscending);
    }

    static class VaccineExtension
    {
        public static IEnumerable<Vaccine> SortBy(this IEnumerable<Vaccine> vaccines, string sortBy, bool isAscending)
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case nameof(Vaccine.NameVaccine):
                        vaccines = isAscending ? vaccines.OrderBy(a => a.NameVaccine) : vaccines.OrderByDescending(a => a.NameVaccine);
                        break;
                    case nameof(Vaccine.IdVaccine):
                        vaccines = isAscending ? vaccines.OrderBy(a => a.IdVaccine) : vaccines.OrderByDescending(a => a.IdVaccine);
                        break;
                }
            }
            return vaccines;
        }
    }
}
