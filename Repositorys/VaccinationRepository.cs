using System;
using System.Collections.Generic;
using System.Linq;
using pis.Models;

namespace pis.Repositorys
{
    public class VaccinationRepository
    {
        private static List<Vaccination> vaccinations = new List<Vaccination>
        {
            
        };

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