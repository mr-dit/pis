using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NUnit.Framework;
using pis.Repositorys;
using pis.Services;
using pis_web_api.Services;

namespace pis_web_api.Models.db
{
    public class Contract
    {
        [Key]
        public int IdContract { get; set; }
        public DateOnly ConclusionDate { get; set; }
        public DateOnly ExpirationDate { get; set; }

        public int PerformerId { get; set; }
        public Organisation? Performer { get; }

        public int CustomerId { get; set; }
        public Organisation? Customer { get; }

        public List<LocalitisListForContract>? Localities { get; set; }
        public List<Vaccination>? Vaccinations { get; set; }

        public Contract() { }
        public Contract(DateTime expirationDate, Organisation customer, Organisation performer)
        {
            ConclusionDate = DateOnly.FromDateTime(DateTime.Today);
            ExpirationDate = DateOnly.FromDateTime(expirationDate);
            PerformerId = performer.OrgId;
            CustomerId = customer.OrgId;
        }

        public bool AddLocalitisList(Locality locality, decimal price)
        {
            var priceList = new LocalitisListForContract(this, locality, price);
            Localities ??= new List<LocalitisListForContract>();
            Localities.Add(priceList);
            var conRepository = new ContractService();
            conRepository.ChangeEntry(this);
            return true;
        }

        public bool HasLocality(int localityId)
        {
            var locality = new LocalityService().GetEntry(localityId);
            var localities = new VaccinePriceListRepository().GetLocalitiesByContract(IdContract);
            if (localities == null || localities.Count == 0)
                throw new Exception("В экземпляре контракта нет городов");
            return localities.Contains(locality);
        }

        public decimal GetPriceByLocality(Locality locality)
        {
            var a = Localities.Where(x => x.LocalityId == locality.IdLocality).Single();
            return a.Price;
        }
        //public Contract(int contractsId, string numberContract, DateTime conclusionDate,
        //    DateTime expirationDate, Organisation performer, Organisation customer,
        //    List<VaccinePriceListByLocality> vacinePriceByLocality, List<Vaccination> vaccinations)
        //{
        //    ContractsId = contractsId;
        //    NumberContract = numberContract;
        //    ConclusionDate = conclusionDate;
        //    ExpirationDate = expirationDate;
        //    Performer = performer;
        //    Customer = customer;
        //    VacinePriceByLocality = vacinePriceByLocality;
        //    Vaccinations = vaccinations;
        //}
    }
}