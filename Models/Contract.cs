using System;
using pis.Services;

namespace pis.Models
{
    public class Contract
    {
        public int ContractsId { get; set; }
        public string NumberContract { get; set; }
        public DateTime ConclusionDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Organisation Performer { get; set; }
        public Organisation Customer { get; set; }
        public List<VaccinePriceListByLocality> VacinePriceByLocality { get; set; }

        public List<Vaccination> Vaccinations { get; set; }

        public Contract(int contractsId, string numberContract, DateTime conclusionDate,
            DateTime expirationDate, Organisation performer, Organisation customer,
            List<VaccinePriceListByLocality> vacinePriceByLocality, List<Vaccination> vaccinations)
        {
            ContractsId = contractsId;
            NumberContract = numberContract;
            ConclusionDate = conclusionDate;
            ExpirationDate = expirationDate;
            Performer = performer;
            Customer = customer;
            VacinePriceByLocality = vacinePriceByLocality;
            Vaccinations = vaccinations;
        }
    }
}