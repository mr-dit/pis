using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using pis.Services;

namespace pis.Models
{
    public class Contract
    {
        [Key]
        public int IdContract { get; set; }
        public DateTime ConclusionDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public int PerformerId { get; set; }
        public Organisation? Performer { get; set; }

        public int CustomerId { get; set; }
        public Organisation? Customer { get; set; }

        //public List<Locality> Localities { get; set; }

        public List<Locality> Localities { get; set; }

        public List<Vaccination>? Vaccinations { get; set; }

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