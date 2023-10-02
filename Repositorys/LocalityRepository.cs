using NUnit.Framework;
using pis.Models;

namespace pis.Repositorys
{
    public class LocalityRepository
    {
        private static List<Locality> localitys = new List<Locality> 
        {
            new Locality (1, "Тюмень"),
            new Locality (2, "Зубарева"),
            new Locality (3, "Патрушева"),
            new Locality (4, "Боровский"),
            new Locality (5, "Луговое"),
        };

        public static Locality GetLocalityByName(string name)
        {
            var locality = localitys.Where(locality => locality.NameLocality == name).FirstOrDefault();
            if (locality == null)
                throw new ArgumentException($"Нет населенного пункта с названием \"{name}\"");
            return locality;
        }
    }
}
