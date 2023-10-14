using NUnit.Framework;
using pis.Models;
using System.Xml.Linq;

namespace pis.Repositorys
{
    public class LocalityRepository
    {
        public static void AddLocality(Locality locality)
        {
            using (var db = new Context())
            {
                db.Add(locality);
                db.SaveChanges();
            }
        }

        public static Locality GetLocalityById(int id)
        {
            using (var db = new Context())
            {
                var locality = db.Localitis.Where(x => x.IdLocality == id).Single();
                if (locality == null)
                    throw new ArgumentException($"Нет населенного пункта с id \"{id}\"");
                return locality;
            }
        }

        public static Locality GetLocalityByName(string name)
        {
            using (var db = new Context())
            {
                var locality = db.Localitis.Where(x => x.NameLocality == name).Single();
                if (locality == null)
                    throw new ArgumentException($"Нет населенного пункта с названием \"{name}\"");
                return locality;
            }
        }

        public static void UpdateLocality(Locality locality)
        {
            using (var db = new Context())
            {
                db.Localitis.Update(locality);
                db.SaveChanges();
            }
        }

        public static void DeleteLocality(Locality locality) 
        {
            using (var db = new Context())
            {
                db.Localitis.Remove(locality);
                db.SaveChanges();
            }
        }
    }
}
