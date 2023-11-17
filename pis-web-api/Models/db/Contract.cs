using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NUnit.Framework;
using pis.Repositorys;
using pis.Services;
using pis_web_api.Models.post;
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
        public Contract(DateOnly conclusionDate, DateOnly expirationDate, Organisation customer, Organisation performer)
        {
            ConclusionDate = conclusionDate;
            ExpirationDate = expirationDate;
            PerformerId = performer.OrgId;
            CustomerId = customer.OrgId;
        }

        public Contract(DateOnly conclusionDate, DateOnly expirationDate, int customerId, int performerId)
        {
            ConclusionDate = conclusionDate;
            ExpirationDate = expirationDate;
            PerformerId = performerId;
            CustomerId = customerId;
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

        public void AddLocalitisList(int localityId, decimal price)
        {
            var locality = new LocalityService().GetEntry(localityId);
            AddLocalitisList(locality, price);
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

        public void Update(ContractPost conPost)
        {
            ConclusionDate = conPost.ConclusionDate;
            ExpirationDate = conPost.ExpirationDate;
            PerformerId = conPost.PerformerId;
            CustomerId = conPost.CustomerId;
            foreach (var localityPricePair in conPost.LocalitiesPriceList)
            {
                if(Localities.Select(x => x.LocalityId).Contains(localityPricePair.LocalityId))
                {
                    var localityPrice = Localities.Where(x => x.LocalityId == localityPricePair.LocalityId).Single();
                    localityPrice.LocalityId = localityPricePair.LocalityId;
                    localityPrice.Price = localityPricePair.Price;
                }
                else
                {
                    this.AddLocalitisList(localityPricePair.LocalityId, localityPricePair.Price);
                }
            }
        }
    }
}