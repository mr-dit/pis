using Microsoft.EntityFrameworkCore;
using pis.Models;
using System.Xml.Linq;

namespace pis.Repositorys
{
    public class VaccinePriceListRepository
    {
        //private static List<VaccinePriceListByLocality> vaccinePriceLists = new List<VaccinePriceListByLocality>
        //{
        //    new VaccinePriceListByLocality(1, VaccineRepository.GetVaccineByName("Бешенный"), 
        //        LocalityRepository.GetLocalityByName("Тюмень"), DateTime.Today.AddDays(-2), 300),

        //    new VaccinePriceListByLocality(2, VaccineRepository.GetVaccineByName("Бешенный"), 
        //        LocalityRepository.GetLocalityByName("Зубарева"), DateTime.Today, 300),

        //    new VaccinePriceListByLocality(3, VaccineRepository.GetVaccineByName("Бешенный"), 
        //        LocalityRepository.GetLocalityByName("Тюмень"), DateTime.Today, 350),

        //    new VaccinePriceListByLocality(4, VaccineRepository.GetVaccineByName("Блошинка"), 
        //        LocalityRepository.GetLocalityByName("Тюмень"), DateTime.Today, 300),

        //    new VaccinePriceListByLocality(5, VaccineRepository.GetVaccineByName("Блошинка"), 
        //        LocalityRepository.GetLocalityByName("Патрушева"), DateTime.Today, 300)
        //};

        public static VaccinePriceListByLocality GetVaccinePriceList(string nameVaccine, string nameLocality)
        {
            using (var db = new Context())
            {
                var vaccinePriceList = db.PriceList
                    .Where(priceList => priceList.Vaccine.NameVaccine == nameVaccine && priceList.Locality.NameLocality == nameLocality)
                    .Include(x => x.Vaccine)
                    .Include(x => x.Locality)
                    .LastOrDefault();
                if (vaccinePriceList == null)
                    throw new ArgumentException($"Нет цены для вакцины \"{nameVaccine}\" и города \"{nameLocality}\"");
                return vaccinePriceList;
            }
        }

        public static void AddVaccinePriceList(VaccinePriceListByLocality price)
        {
            using (var db = new Context())
            {
                db.PriceList.Add(price);
                db.SaveChanges();
            }
        }

        public static void DeleteVaccinePriceList(VaccinePriceListByLocality price)
        {
            using (var db = new Context())
            {
                db.PriceList.Remove(price);
                db.SaveChanges();
            }
        }

        public static void UpdateVaccinePriceList(VaccinePriceListByLocality price)
        {
            using (var db = new Context())
            {
                db.PriceList.Update(price);
                db.SaveChanges();
            }
        }
    }
}
