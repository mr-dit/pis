using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using pis.Models;
using pis.Services;
using System.Globalization;
using System.Runtime.CompilerServices;
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

        public static (List<Vaccine>, int) GetVaccinesByName(string name, int pageNumber, int pageSize, string sortBy, bool isAscending)
        {
            using (var db = new Context())
            {
                var allVaccines = db.Vaccines
                .Where(x => string.IsNullOrEmpty(name) || x.NameVaccine.Contains(name))
                .SortBy(sortBy, isAscending);                

                var vaccine = allVaccines
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

                var count = allVaccines.Count();

                return (vaccine, count);
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

    static class VaccineExtension
    {
        public static IQueryable<Vaccine> SortBy(this IQueryable<Vaccine> vaccines, string sortBy, bool isAscending)
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
