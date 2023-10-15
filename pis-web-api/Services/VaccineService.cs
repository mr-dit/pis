using pis.Models;
using pis.Repositorys;

namespace pis_web_api.Services
{
    public class VaccineService
    {
        private static List<Vaccine> Vaccines { get; set; } = new List<Vaccine>();

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

        public static List<Vaccine> GetVaccines(string filterField, string filterValue, string sortBy, bool isAscending, int pageNumber, int pageSize)
        {
            switch (filterField)
            {
                case nameof(Vaccine.NameVaccine):
                    Vaccines = VaccineRepository.GetVaccinesByName(filterValue, pageNumber, pageSize);
                    break;

                case nameof(Vaccine.IdVaccine):
                    //Vaccines = VaccineRepository.GetVaccinesById(filterValue, pageNumber, pageSize);
                    //break;

                case "":

                    break;
                default:
                    throw new ArgumentException("Нет такого поля для фильтрации");
            }
            // Сортировка
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case nameof(Vaccine.NameVaccine):
                        Vaccines = isAscending ? Vaccines.OrderBy(a => a.NameVaccine).ToList() : Vaccines.OrderByDescending(a => a.NameVaccine).ToList();
                        break;
                }
            }

            // Пагинация
            var animalsPag = Vaccines.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return animalsPag;
        }


        public static bool ChangeEntry(Vaccine animal)
        {
            bool status = VaccineRepository.UpdateVaccine(animal);
            return status;
        }

        public static int GetTotalVaccines(string filterField, string? filterValue)
        {
            return Vaccines.Count;
        }
    }
}
