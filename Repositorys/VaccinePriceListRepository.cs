using pis.Models;
using System.Xml.Linq;

namespace pis.Repositorys
{
    public class VaccinePriceListRepository
    {
        private static List<VaccinePriceListByLocality> vaccinePriceLists = new List<VaccinePriceListByLocality>
        {
            new VaccinePriceListByLocality(1, VaccineRepository.GetVaccineByName("Бешенный"), 
                LocalityRepository.GetLocalityByName("Тюмень"), DateTime.Today.AddDays(-2), 300),

            new VaccinePriceListByLocality(2, VaccineRepository.GetVaccineByName("Бешенный"), 
                LocalityRepository.GetLocalityByName("Зубарева"), DateTime.Today, 300),

            new VaccinePriceListByLocality(3, VaccineRepository.GetVaccineByName("Бешенный"), 
                LocalityRepository.GetLocalityByName("Тюмень"), DateTime.Today, 350),

            new VaccinePriceListByLocality(4, VaccineRepository.GetVaccineByName("Блошинка"), 
                LocalityRepository.GetLocalityByName("Тюмень"), DateTime.Today, 300),

            new VaccinePriceListByLocality(5, VaccineRepository.GetVaccineByName("Блошинка"), 
                LocalityRepository.GetLocalityByName("Патрушева"), DateTime.Today, 300)
        };

        public static VaccinePriceListByLocality GetVaccinePriceList(string nameVaccine, string nameLocality)
        {
            var vaccinePriceList = vaccinePriceLists.Where(priceList => priceList.Vaccine.NameVaccine == nameVaccine && priceList.Locality.NameLocality == nameLocality).LastOrDefault();
            if(vaccinePriceList == null)
                throw new ArgumentException($"Нет цены для вакцины \"{nameVaccine}\" и города \"{nameLocality}\"");
            return vaccinePriceList;
        }
    }
}
