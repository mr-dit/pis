using NUnit.Framework;
using pis.Models;
using pis.Repositorys;

namespace pis_web_api.Services
{
    public class VaccineService
    {
        public static bool FillData(Vaccine vaccine)
        {
            bool status = VaccineRepository.AddVaccine(vaccine);
            return status;
        }

        public static bool DeleteEntry(int id)
        {
            bool status = VaccineRepository.DeleteVaccine(VaccineRepository.GetVaccineById(id));
            return status;
        }

        public static Vaccine? GetEntry(int id)
        {
            var entry = VaccineRepository.GetVaccineById(id);
            return entry;
        }

        public static (List<Vaccine>, int) GetVaccines(string filterField, string filterValue, string sortBy, bool isAscending, int pageNumber, int pageSize)
        {
            List<Vaccine> vaccines;
            int count;
            switch (filterField)
            {
                case nameof(Vaccine.NameVaccine):
                    (vaccines, count) = VaccineRepository.GetVaccinesByName(filterValue, pageNumber, pageSize, sortBy, isAscending);
                    break;

                case "":
                    (vaccines, count) = VaccineRepository.GetVaccinesByName(filterValue, pageNumber, pageSize, sortBy, isAscending);
                    break;

                default:
                    throw new ArgumentException("Нет такого поля для фильтрации");
            }

            return (vaccines, count);
        }


        public static bool ChangeEntry(Vaccine animal)
        {
            bool status = VaccineRepository.UpdateVaccine(animal);
            return status;
        }
    }
}
