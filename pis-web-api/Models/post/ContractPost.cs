using pis_web_api.Models.db;

namespace pis_web_api.Models.post
{
    public class ContractPost
    {
        public DateOnly ConclusionDate { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public int PerformerId { get; set; }
        public int CustomerId { get; set; }
        public Dictionary<int, decimal> LocalitiesPriceList { get; set; }

        public Contract ConvertToContract()
        {
            var contract = new Contract(ConclusionDate, ExpirationDate, CustomerId, PerformerId);
            foreach (var priceLocalityPare in LocalitiesPriceList)
                contract.AddLocalitisList(priceLocalityPare.Key, priceLocalityPare.Value);
            return contract;
        }

        public Contract ConvertToContractWithId(int id)
        {
            var contract = this.ConvertToContract();
            contract.IdContract = id;
            return contract;
        }
    }
}
