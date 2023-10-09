using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using pis.Models;
using pis.Services;
using System.Xml.Linq;

namespace pis.Repositorys
{
    public class VaccineRepository
    {
        //private static List<Vaccine> vaccines = new List<Vaccine>
        //{
        //    new Vaccine (1, "Бешенный", 20),
        //    new Vaccine (2, "Блошинка", 90)
        //};

        public static void AddVaccine(Vaccine vaccine)
        {
            using (var db = new Context())
            {
                db.Vaccines.Add(vaccine);
                db.SaveChangesAsync();
            }
        }

        public static void DeleteVaccine(Vaccine vaccine)
        {
            using (var db = new Context())
            {
                db.Vaccines.Remove(vaccine);
                db.SaveChangesAsync();
            }
        }

        public static void UpdateVaccine(Vaccine vaccine)
        {
            using (var db = new Context())
            {
                db.Vaccines.Update(vaccine);
                db.SaveChangesAsync();
            }
        }

        public static Vaccine GetVaccineById(int id)
        {
            using (var db = new Context())
            {
                var vaccine = db.Vaccines.Where(x => x.IdVaccine == id).Single();
                if (vaccine == null)
                    throw new ArgumentNullException($"Нет вакцины с id \"{id}\"");
                return vaccine;
            }
        }

        public static Vaccine GetVaccineByName(string name)
        {
            using (var db = new Context())
            {
                var vaccine = db.Vaccines.Where(x => x.NameVaccine == name).Single();
                if (vaccine == null)
                    throw new ArgumentNullException($"Нет вакцины с названием \"{name}\"");
                return vaccine;
            }
        }
    }
}
