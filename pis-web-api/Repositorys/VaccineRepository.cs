using System;
using System.Collections.Generic;
using System.Linq;
using pis_web_api.Models;

namespace pis_web_api.Repositorys
{
    public class VaccineRepository
    {
        private static List<Vaccination> vaccines = new List<Vaccination>
        {
            new Vaccination
            {
                VaccineId = 1,
                Animal = AnimalRepository.GetEntry(1),
                VaccinationDate = DateTime.Now,
                VaccineType = "Название вакцины 1",
                BatchNumber = "123456",
                ValidUntil = DateTime.Now.AddDays(365),
                VeterinarianFullName = "Иванов Иван Иванович",
                VeterinarianPosition = "Ветеринарный врач",
                Organisation = OrganisationsRepository.GetEntry(1),
                Contract = ContractsRepository.GetEntry(1)
            },
            new Vaccination
            {
                VaccineId = 2,
                Animal = AnimalRepository.GetEntry(2),
                VaccinationDate = DateTime.Now,
                VaccineType = "Название вакцины 2",
                BatchNumber = "654321",
                ValidUntil = DateTime.Now.AddDays(365),
                VeterinarianFullName = "Петров Петр Петрович",
                VeterinarianPosition = "Ветеринарный специалист",
                Organisation = OrganisationsRepository.GetEntry(2),
                Contract = ContractsRepository.GetEntry(2)
            }
        };

        public static bool NewEntry(Vaccination vaccine)
        {
            int maxRegistrationNumber = vaccines.Max(a => a.VaccineId);
            int nextRegistrationNumber = maxRegistrationNumber + 1;

            vaccine.VaccineId = nextRegistrationNumber;
            vaccines.Add(vaccine);
            return true;
        }

        public static bool DeleteEntry(int vaccineId)
        {
            var foundVaccine = vaccines.FirstOrDefault(a => a.VaccineId == vaccineId);
            if (foundVaccine != null)
            {
                vaccines.Remove(foundVaccine);
                Console.WriteLine("Vaccinated animal entry deleted.");
                return true;
            }

            Console.WriteLine("Vaccinated animal entry not found.");
            return false;
        }

        public static Vaccination? GetEntry(int vaccineId)
        {
            var foundVaccine = vaccines.FirstOrDefault(a => a.VaccineId == vaccineId);
            return foundVaccine;
        }

        public static List<Vaccination> GetVaccines()
        {
            return vaccines;
        }

        public static bool ChangeEntry(Vaccination vaccine)
        {
            var foundVaccine =
                vaccines.FirstOrDefault(a => a.VaccineId == vaccine.VaccineId);
            if (foundVaccine != null)
            {
                foundVaccine.Animal = vaccine.Animal;
                foundVaccine.VaccinationDate = vaccine.VaccinationDate;
                foundVaccine.VaccineType = vaccine.VaccineType;
                foundVaccine.BatchNumber = vaccine.BatchNumber;
                foundVaccine.ValidUntil = vaccine.ValidUntil;
                foundVaccine.VeterinarianFullName = vaccine.VeterinarianFullName;
                foundVaccine.VeterinarianPosition = vaccine.VeterinarianPosition;
                foundVaccine.Organisation = vaccine.Organisation;

                return true;
            }

            return false;
        }

        public VaccineRepository()
        {
        }
    }
}