using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using Microsoft.EntityFrameworkCore;
using pis.Models;

namespace pis.Repositorys
{
    public class VaccinationRepository
    {
        public static bool AddVacciantion(Vaccination vac)
        {
            using (var db = new Context())
            {
                try
                {
                    db.Vaccinations.Add(vac);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }

        public static bool RemoveVacciantion(Vaccination vac)
        {
            using (var db = new Context())
            {
                try
                {
                    db.Vaccinations.Remove(vac);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }

        public static bool UpdateVaccination(Vaccination vac)
        {
            using (var db = new Context())
            {
                try
                {
                    db.Vaccinations.Update(vac);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }

        public static Vaccination GetVaccinationById(int id)
        {
            using (var db = new Context())
            {
                var vac = db.Vaccinations
                    .Include(x => x.Animal)
                    .Include(x => x.Contract)
                        .ThenInclude(x => x.Customer)
                    .Include(x => x.Doctor)
                        .ThenInclude(x => x.Organisation)
                    .Include(x => x.Vaccine)
                    .Single();
                if (vac == null)
                    throw new ArgumentNullException($"Не существует вакцинации с id {id}");
                return vac;
            }
        }

        public static IQueryable<Vaccination> GetVaccinationsByAnimal(Animal animal)
        {
            using (var db = new Context())
            {
                var vac = db.Vaccinations
                    .Include(x => x.Animal)
                    .Include(x => x.Contract)
                        .ThenInclude(x => x.Customer)
                    .Include(x => x.Doctor)
                        .ThenInclude(x => x.Organisation)
                    .Include(x => x.Vaccine)
                    .Where(x => x.Animal.AnimalName.ToLower().Contains(animal.AnimalName));
                //if (vac == null)
                //    throw new ArgumentNullException($"Не существует вакцинации с животными {id}");
                return vac;
            }
        }

        public static IQueryable<Vaccination> GetVaccinationsByAnimalName(string name)
        {
            using (var db = new Context())
            {
                var vac = db.Vaccinations
                    .Include(x => x.Animal)
                    .Include(x => x.Contract)
                        .ThenInclude(x => x.Customer)
                    .Include(x => x.Doctor)
                        .ThenInclude(x => x.Organisation)
                    .Include(x => x.Vaccine)
                    .Where(x => x.Animal.AnimalName.ToLower().Contains(name));
                //if (vac == null)
                //    throw new ArgumentNullException($"Не существует вакцинации с животными {id}");
                return vac;
            }
        }

        public static IQueryable<Vaccination> GetVaccinationsByDate(DateOnly date)
        {
            using (var db = new Context())
            {
                var vac = db.Vaccinations
                    .Include(x => x.Animal)
                    .Include(x => x.Contract)
                        .ThenInclude(x => x.Customer)
                    .Include(x => x.Doctor)
                        .ThenInclude(x => x.Organisation)
                    .Include(x => x.Vaccine)
                    .Where(x => x.VaccinationDate.Equals(date));
                //if (vac == null)
                //    throw new ArgumentNullException($"Не существует вакцинации с животными {id}");
                return vac;
            }
        }

        public static IQueryable<Vaccination> GetVaccinationsByDoctorName(string name)
        {
            using (var db = new Context())
            {
                var vac = db.Vaccinations
                    .Include(x => x.Animal)
                    .Include(x => x.Contract)
                        .ThenInclude(x => x.Customer)
                    .Include(x => x.Doctor)
                        .ThenInclude(x => x.Organisation)
                    .Include(x => x.Vaccine)
                    .Where(x => x.Doctor.LastName.Contains(name));
                //if (vac == null)
                //    throw new ArgumentNullException($"Не существует вакцинации с животными {id}");
                return vac;
            }
        }

        public static IQueryable<Vaccination> GetVaccinationsByOrgName(string name)
        {
            using (var db = new Context())
            {
                var vac = db.Vaccinations
                    .Include(x => x.Animal)
                    .Include(x => x.Contract)
                        .ThenInclude(x => x.Customer)
                    .Include(x => x.Doctor)
                        .ThenInclude(x => x.Organisation)
                    .Include(x => x.Vaccine)
                    .Where(x => x.Contract.Customer.OrgName.ToLower().Contains(name.ToLower()) ||
                                    x.Doctor.Organisation.OrgName.ToLower().Contains(name.ToLower()));
                //if (vac == null)
                //    throw new ArgumentNullException($"Не существует вакцинации с животными {id}");
                return vac;
            }
        }
    }
}