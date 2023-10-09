using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using AspNetCore;
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
                    db.SaveChangesAsync();
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
                    db.SaveChangesAsync();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }

        public static void UpdateVaccination(Vaccination vac)
        {
            using (var db = new Context())
            {
                db.Vaccinations.Update(vac);
                db.SaveChangesAsync();
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
                    .Include(x => x.VaccinePriceListByLocality)
                        .ThenInclude(x => x.Vaccine)
                    .Single();
                if (vac == null)
                    throw new ArgumentNullException($"Не существует вакцинации с id {id}");
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
                    .Include(x => x.VaccinePriceListByLocality)
                        .ThenInclude(x => x.Vaccine)
                    .Where(x => x.Animal.AnimalName.ToLower().Contains(name));
                //if (vac == null)
                //    throw new ArgumentNullException($"Не существует вакцинации с животными {id}");
                return vac;
            }
        }

        public static IQueryable<Vaccination> GetVaccinationsByDate(DateTime date)
        {
            using (var db = new Context())
            {
                var vac = db.Vaccinations
                    .Include(x => x.Animal)
                    .Include(x => x.Contract)
                        .ThenInclude(x => x.Customer)
                    .Include(x => x.Doctor)
                        .ThenInclude(x => x.Organisation)
                    .Include(x => x.VaccinePriceListByLocality)
                        .ThenInclude(x => x.Vaccine)
                    .Where(x => x.VaccinationDate.Date.Equals(date.Date));
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
                    .Include(x => x.VaccinePriceListByLocality)
                        .ThenInclude(x => x.Vaccine)
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
                    .Include(x => x.VaccinePriceListByLocality)
                        .ThenInclude(x => x.Vaccine)
                    .Where(x => x.Contract.Customer.OrgName.ToLower().Contains(name.ToLower()) ||
                                    x.Doctor.Organisation.OrgName.ToLower().Contains(name.ToLower()));
                //if (vac == null)
                //    throw new ArgumentNullException($"Не существует вакцинации с животными {id}");
                return vac;
            }
        }
        //public static bool NewEntry(Vaccination vaccine)
        //{
        //    int maxRegistrationNumber = vaccines.Max(a => a.VaccineId);
        //    int nextRegistrationNumber = maxRegistrationNumber + 1;

        //    vaccine.VaccineId = nextRegistrationNumber;
        //    vaccines.Add(vaccine);
        //    return true;
        //}

        //public static bool DeleteEntry(int vaccineId)
        //{
        //    var foundVaccine = vaccines.FirstOrDefault(a => a.VaccineId == vaccineId);
        //    if (foundVaccine != null)
        //    {
        //        vaccines.Remove(foundVaccine);
        //        Console.WriteLine("Vaccinated animal entry deleted.");
        //        return true;
        //    }

        //    Console.WriteLine("Vaccinated animal entry not found.");
        //    return false;
        //}

        //public static Vaccination? GetEntry(int vaccineId)
        //{
        //    var foundVaccine = vaccines.FirstOrDefault(a => a.VaccineId == vaccineId);
        //    return foundVaccine;
        //}

        //public static List<Vaccination> GetVaccines()
        //{
        //    return vaccines;
        //}

        //public static bool ChangeEntry(Vaccination vaccine)
        //{
        //    var foundVaccine =
        //        vaccines.FirstOrDefault(a => a.VaccineId == vaccine.VaccineId);
        //    if (foundVaccine != null)
        //    {
        //        foundVaccine.Animal = vaccine.Animal;
        //        foundVaccine.VaccinationDate = vaccine.VaccinationDate;
        //        foundVaccine.VaccineType = vaccine.VaccineType;
        //        foundVaccine.BatchNumber = vaccine.BatchNumber;
        //        foundVaccine.ValidUntil = vaccine.ValidUntil;
        //        foundVaccine.VeterinarianFullName = vaccine.VeterinarianFullName;
        //        foundVaccine.VeterinarianPosition = vaccine.VeterinarianPosition;
        //        foundVaccine.Organisation = vaccine.Organisation;

        //        return true;
        //    }

        //    return false;
        //}

        //public VaccinationRepository()
        //{
        //}
    }
}