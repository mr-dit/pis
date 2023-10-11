using System;
using pis_web_api.Models;
using pis_web_api.Repositorys;

namespace pis_web_api.Services
{
    public class VaccineService
    {

        private static bool FilterVaccination(Vaccination vaccination, string filterField, string? filterValue)
        {
            if (filterField == "Locality")
            {
                return vaccination.Animal?.Locality == filterValue;
            }

            // Другие возможные параметры фильтрации

            return true;
        }
        public static bool FillData(Vaccination vaccination)
        {
            bool status = VaccineRepository.NewEntry(vaccination);
            if (status)
            {
                return true;
            }

            return false;
        }

        public static bool DeleteEntry(int id)
        {
            bool status = VaccineRepository.DeleteEntry(id);
            if (status)
            {
                return true;
            }

            return false;
        }

        public static Vaccination? GetEntry(int id)
        {
            var entry = VaccineRepository.GetEntry(id);
            return entry;
        }

        public static List<Vaccination>? GetVaccines(string filterField, string? filterValue, string sortBy,
            bool isAscending, int pageNumber, int pageSize)
        {
            filterValue = filterValue?.ToLower();

            var vaccines = VaccineRepository.GetVaccines();

            // Применение фильтрации в зависимости от поля
            if (!string.IsNullOrEmpty(filterField) && !string.IsNullOrEmpty(filterValue))
            {
                switch (filterField.ToLower())
                {
                    case "animalname":
                        vaccines = vaccines.Where(v => v.Animal.AnimalName.ToLower().Contains(filterValue)).ToList();
                        break;
                    case "vaccinationdate":
                        vaccines = vaccines.Where(v => v.VaccinationDate.ToShortDateString().Contains(filterValue))
                            .ToList();
                        break;
                    case "veterinarianfullname":
                        vaccines = vaccines.Where(v => v.VeterinarianFullName.ToLower().Contains(filterValue)).ToList();
                        break;
                    case "orgname":
                        vaccines = vaccines.Where(v => v.Organisation.OrgName.ToLower().Contains(filterValue)).ToList();
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
                        vaccines = isAscending
                            ? vaccines.OrderBy(v => v.VaccineId).ToList()
                            : vaccines.OrderByDescending(v => v.VaccineId).ToList();
                        break;
                    case "AnimalName":
                        vaccines = isAscending
                            ? vaccines.OrderBy(v => v.Animal.AnimalName).ToList()
                            : vaccines.OrderByDescending(v => v.Animal.AnimalName).ToList();
                        break;
                    case "VaccinationDate":
                        vaccines = isAscending
                            ? vaccines.OrderBy(v => v.VaccinationDate).ToList()
                            : vaccines.OrderByDescending(v => v.VaccinationDate).ToList();
                        break;
                    case "VaccineType":
                        vaccines = isAscending
                            ? vaccines.OrderBy(v => v.VaccineType).ToList()
                            : vaccines.OrderByDescending(v => v.VaccineType).ToList();
                        break;
                    case "BatchNumber":
                        vaccines = isAscending
                            ? vaccines.OrderBy(v => v.BatchNumber).ToList()
                            : vaccines.OrderByDescending(v => v.BatchNumber).ToList();
                        break;
                    case "ValidUntil":
                        vaccines = isAscending
                            ? vaccines.OrderBy(v => v.ValidUntil).ToList()
                            : vaccines.OrderByDescending(v => v.ValidUntil).ToList();
                        break;
                    case "VeterinarianFullName":
                        vaccines = isAscending
                            ? vaccines.OrderBy(v => v.VeterinarianFullName).ToList()
                            : vaccines.OrderByDescending(v => v.VeterinarianFullName).ToList();
                        break;
                    case "VeterinarianPosition":
                        vaccines = isAscending
                            ? vaccines.OrderBy(v => v.VeterinarianPosition).ToList()
                            : vaccines.OrderByDescending(v => v.VeterinarianPosition).ToList();
                        break;
                    case "OrgName":
                        vaccines = isAscending
                            ? vaccines.OrderBy(v => v.Organisation.OrgName).ToList()
                            : vaccines.OrderByDescending(v => v.Organisation.OrgName).ToList();
                        break;
                }
            }

            // Пагинация
            vaccines = vaccines.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return vaccines;
        }

        public static int GetTotalVaccines(string? filterField, string? filterValue)
        {
            filterValue = filterValue?.ToLower();

            var vaccines = VaccineRepository.GetVaccines();

            // Применение фильтрации в зависимости от поля
            if (!string.IsNullOrEmpty(filterField) && !string.IsNullOrEmpty(filterValue))
            {
                switch (filterField.ToLower())
                {
                    case "animalname":
                        vaccines = vaccines.Where(v => v.Animal.AnimalName.ToLower().Contains(filterValue)).ToList();
                        break;
                    case "vaccinationdate":
                        vaccines = vaccines.Where(v => v.VaccinationDate.ToShortDateString().Contains(filterValue))
                            .ToList();
                        break;
                    case "veterinarianfullname":
                        vaccines = vaccines.Where(v => v.VeterinarianFullName.ToLower().Contains(filterValue)).ToList();
                        break;
                    case "orgname":
                        vaccines = vaccines.Where(v => v.Organisation.OrgName.ToLower().Contains(filterValue)).ToList();
                        break;
                    // Добавьте остальные варианты полей
                    default:
                        break;
                }
            }

            return vaccines.Count;
        }


        public static bool ChangeEntry(Vaccination vaccination)
        {
            bool status = VaccineRepository.ChangeEntry(vaccination);
            return status;
        }

        public static List<Vaccination> GetPreviousVaccinations(int registrationNumber)
        {
            // Получение вакцинаций из базы данных по регистрационному номеру животного
            var previousVaccinations = VaccineRepository.GetVaccines()
                .Where(v => v.Animal.RegistrationNumber == registrationNumber)
                .ToList();

            return previousVaccinations;
        }


        public VaccineService()
        {
        }
    }
}