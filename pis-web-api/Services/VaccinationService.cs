using System;
using System.Numerics;
using pis.Models;
using pis.Repositorys;

namespace pis.Services
{
    public class VaccinationService
    {
        private static List<Vaccination> Vaccinations { get; set; } = new List<Vaccination>(); 

        public static bool FillData(Vaccination vaccination)
        {
            bool status = VaccinationRepository.AddVacciantion(vaccination);
            return status;
        }

        public static bool DeleteEntry(int id)
        {
            bool status = VaccinationRepository.RemoveVacciantion(VaccinationRepository.GetVaccinationById(id));
            return status;
        }

        public static Vaccination? GetEntry(int id)
        {
            var vaccination = VaccinationRepository.GetVaccinationById(id);
            return vaccination;
        }

        public static List<Vaccination>? GetVaccinations(string filterField, string? filterValue, string sortBy,
            bool isAscending, int pageNumber, int pageSize)
        {
            filterValue = filterValue?.ToLower();

            // Применение фильтрации в зависимости от поля
            if (!string.IsNullOrEmpty(filterField) && !string.IsNullOrEmpty(filterValue))
            {
                switch (filterField.ToLower())
                {
                    case "animalname":
                        Vaccinations = VaccinationRepository.GetVaccinationsByAnimalName(filterValue).ToList();
                        break;
                    case "vaccinationdate":
                        Vaccinations = VaccinationRepository.GetVaccinationsByDate(DateTime.Parse(filterValue)).ToList();
                        break;
                    case "veterinarianfullname":
                        Vaccinations = VaccinationRepository.GetVaccinationsByDoctorName(filterValue).ToList();
                        break;
                    case "orgname":
                        Vaccinations = VaccinationRepository.GetVaccinationsByOrgName(filterValue).ToList();
                        break;
                    // Добавьте остальные варианты полей
                    default:
                        break;
                }
            }

            // Сортировка
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "VaccineId":
                        Vaccinations = isAscending
                            ? Vaccinations.OrderBy(v => v.IdVactination).ToList()
                            : Vaccinations.OrderByDescending(v => v.IdVactination).ToList();
                        break;
                    case "AnimalName":
                        Vaccinations = isAscending
                            ? Vaccinations.OrderBy(v => v.Animal.AnimalName).ToList()
                            : Vaccinations.OrderByDescending(v => v.Animal.AnimalName).ToList();
                        break;
                    case "VaccinationDate":
                        Vaccinations = isAscending
                            ? Vaccinations.OrderBy(v => v.VaccinationDate).ToList()
                            : Vaccinations.OrderByDescending(v => v.VaccinationDate).ToList();
                        break;
                    case "VaccineType":
                        Vaccinations = isAscending
                            ? Vaccinations.OrderBy(v => v.Vaccine.NameVaccine).ToList()
                            : Vaccinations.OrderByDescending(v => v.Vaccine.NameVaccine).ToList();
                        break;
                    //case "ValidUntil":
                    //    Vaccinations = isAscending
                    //        ? Vaccinations.OrderBy(v => v.ValidUntil).ToList()
                    //        : Vaccinations.OrderByDescending(v => v.ValidUntil).ToList();
                    //    break;
                    case "VeterinarianFullName":
                        Vaccinations = isAscending
                            ? Vaccinations.OrderBy(v => v.Doctor.LastName).ToList()
                            : Vaccinations.OrderByDescending(v => v.Doctor.LastName).ToList();
                        break;
                    //case "VeterinarianPosition":
                    //    Vaccinations = isAscending
                    //        ? Vaccinations.OrderBy(v => v. VeterinarianPosition).ToList()
                    //        : Vaccinations.OrderByDescending(v => v.VeterinarianPosition).ToList();
                    //    break;
                    case "OrgName":
                        Vaccinations = isAscending
                            ? Vaccinations.OrderBy(v => v.Contract.Customer.OrgName).ToList()
                            : Vaccinations.OrderByDescending(v => v.Contract.Customer.OrgName).ToList();
                        break;
                }
            }

            // Пагинация
            var vaccinesPag = Vaccinations.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return vaccinesPag;
        }

        public static int GetTotalVaccines(string filterField, string? filterValue)
        {
            return Vaccinations.Count();
        }


        public static bool ChangeEntry(Vaccination vaccination)
        {
            bool status = VaccinationRepository.UpdateVaccination(vaccination);
            return status;
        }

        public static List<Vaccination> GetPreviousVaccinations(int registrationNumber)
        {
            // Получение вакцинаций из базы данных по регистрационному номеру животного
            var previousVaccinations = VaccinationRepository.GetVaccinationsByAnimal(AnimalService.GetEntry(registrationNumber)).ToList();
            return previousVaccinations;
        }
    }
}