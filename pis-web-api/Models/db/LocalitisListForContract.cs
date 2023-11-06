using System.ComponentModel.DataAnnotations;

namespace pis_web_api.Models.db
{
    public class LocalitisListForContract
    {
        public int Id { get; set; }

        public int ContractId { get; set; }
        public Contract Contract { get; set; }

        public int LocalityId { get; set; }
        public Locality Locality { get; set; }

        public decimal Price { get; set; }

        public LocalitisListForContract() { }
        public LocalitisListForContract(Contract contract, Locality locality, decimal price)
        {
            ContractId = contract.IdContract;
            LocalityId = locality.IdLocality;
            Price = price;
        }
    }
}
