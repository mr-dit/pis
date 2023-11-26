using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using pis_web_api.Models;
using pis_web_api.Models.db;
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

        public List<Contract> GetContractsByLocality(int localityId, IEnumerable<int> contractsIds)
        {
            using (var db = new Context())
            {
                var vaccinePriceList = db.LocalitisListForContract
                    .Include(x => x.Contract)
                    .Where(x => contractsIds.Contains(x.ContractId))
                    .Where(x => x.LocalityId == localityId)
                    .Select (x => x.Contract);
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
