using System;
using pis.Models;
using pis.Repositorys;

namespace pis.Services
{
    public class VaccinationService
    {
        private static List<Vaccination> Vaccinations { get; set; } = new List<Vaccination>(); 

        // ??????????????????????????????????????????????????
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
                            ? Vaccinations.OrderBy(v => v.VaccinePriceListByLocality.Vaccine.NameVaccine).ToList()
                            : Vaccinations.OrderByDescending(v => v.VaccinePriceListByLocality.Vaccine.NameVaccine).ToList();
                        break;
                    //case "ValidUntil":
                    //    Vaccinations = isAscending
                    //        ? Vaccinations.OrderBy(v => v.ValidUntil).ToList()
                    //        : Vaccinations.OrderByDescending(v => v.ValidUntil).ToList();
                    //    break;
                    case "VeterinarianFullName":
                        Vaccinations = isAscending
                            ? Vaccinations.OrderBy(v => v.VeterinarianFullName).ToList()
                            : Vaccinations.OrderByDescending(v => v.VeterinarianFullName).ToList();
                        break;
                    case "VeterinarianPosition":
                        Vaccinations = isAscending
                            ? Vaccinations.OrderBy(v => v.VeterinarianPosition).ToList()
                            : Vaccinations.OrderByDescending(v => v.VeterinarianPosition).ToList();
                        break;
                    case "OrgName":
                        Vaccinations = isAscending
                            ? Vaccinations.OrderBy(v => v.Organisation.OrgName).ToList()
                            : Vaccinations.OrderByDescending(v => v.Organisation.OrgName).ToList();
                        break;
                }
            }

            // Пагинация
            vaccines = vaccines.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return vaccines;
        }

        public static int GetTotalVaccines(string filterField, string? filterValue)
        {
            filterValue = filterValue?.ToLower();

            var vaccines = VaccinationRepository.GetVaccines();

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
            bool status = VaccinationRepository.ChangeEntry(vaccination);
            return status;
        }

        public static List<Vaccination> GetPreviousVaccinations(int registrationNumber)
        {
            // Получение вакцинаций из базы данных по регистрационному номеру животного
            var previousVaccinations = VaccinationRepository.GetVaccines()
                .Where(v => v.Animal.RegistrationNumber == registrationNumber)
                .ToList();

            return previousVaccinations;
        }


        public VaccineService()
        {
        }
    }
}