using Microsoft.EntityFrameworkCore;
using pis.Models;
using pis_web_api.Models;
using System.Xml.Linq;

namespace pis.Repositorys
{
    public class VaccinePriceListRepository
    {
        public List<Locality> GetLocalitiesByContract(int contractId)
        {
            using (var db = new Context())
            {
                var vaccinePriceList = db.LocalitisListForContract
                    .Include(x => x.Locality)
                    .Where(x => x.ContractId == contractId)
                    .Select(x => x.Locality);
                return vaccinePriceList.ToList();
            }
        }

        //public static void AddVaccinePriceList(VaccinePriceListByLocality price)
        //{
        //    using (var db = new Context())
        //    {
        //        db.PriceList.Add(price);
        //        db.SaveChanges();
        //    }
        //}

        //public static void DeleteVaccinePriceList(VaccinePriceListByLocality price)
        //{
        //    using (var db = new Context())
        //    {
        //        db.PriceList.Remove(price);
        //        db.SaveChanges();
        //    }
        //}

        //public static void UpdateVaccinePriceList(VaccinePriceListByLocality price)
        //{
        //    using (var db = new Context())
        //    {
        //        db.PriceList.Update(price);
        //        db.SaveChanges();
        //    }
        //}
    }
}
