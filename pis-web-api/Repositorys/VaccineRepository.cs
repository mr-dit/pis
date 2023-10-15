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

        public static bool AddVaccine(Vaccine vaccine)
        {
            using (var db = new Context())
            {
                try
                {
                    db.Vaccines.Add(vaccine);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }

        public static bool DeleteVaccine(Vaccine vaccine)
        {
            using (var db = new Context())
            {
                try
                {
                    db.Vaccines.Remove(vaccine);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }

        public static bool UpdateVaccine(Vaccine vaccine)
        {
            using (var db = new Context())
            {
                try
                {
                    db.Vaccines.Update(vaccine);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
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

        //public static Vaccine GetVaccineByName(string name)
        //{
        //    using (var db = new Context())
        //    {
        //        var vaccine = db.Vaccines.Where(x => x.NameVaccine == name).Single();
        //        if (vaccine == null)
        //            throw new ArgumentNullException($"Нет вакцины с названием \"{name}\"");
        //        return vaccine;
        //    }
        //}

        public static List<Vaccine> GetVaccinesByName(string name, int pageNumber, int pageSize)
        {
            using (var db = new Context())
            {
                var vaccine = db.Vaccines
                .Where(x => string.IsNullOrEmpty(name) || x.NameVaccine.Contains(name))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
                return vaccine;
            }
        }

        public static List<Vaccine> GetFirstVaccines(int limit)
        {
            using (var db = new Context())
            {
                var vaccine = db.Vaccines.Take(limit).ToList();
                return vaccine;
            }
        }
    }
}
