using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using pis_web_api.Models.db;
using pis_web_api.Repositorys;
using System.Globalization;
using System.Xml.Linq;

namespace pis.Repositorys
{
    public class LocalityRepository : Repository<Locality>
    {
        public Locality GetLocalityByName(string name)
        {
            using (var db = new Context())
            {
                var locality = db.Localitis.Where(x => x.NameLocality == name).Single();
                if (locality == null)
                    throw new ArgumentException($"Нет населенного пункта с названием \"{name}\"");
                return locality;
            }
        }

        public (List<Locality>, int) GetLocalitiesByName(string name, int pageNumber, int pageSize)
        {
            using (Context db = new Context())
            {
                var allLocalities = db.Localitis
                    .AsEnumerable()
                    .Where(x => x.NameLocality.Contains(name, StringComparison.InvariantCultureIgnoreCase))
                    .OrderBy(x => x.NameLocality);
                var localiies = allLocalities.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                return (localiies, allLocalities.Count());
            }
        }
    }
}
