using pis.Repositorys;
using pis.Services;
using pis_web_api.Models.db;

namespace pis_web_api.Models.post
{
    public class ContractPost
    {
        public DateOnly ConclusionDate { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public int PerformerId { get; set; }
        public int CustomerId { get; set; }
        public List<LocalityPrice> LocalitiesPriceList { get; set; }

        public Contract ConvertToContract()
        {
            if (LocalitiesPriceList.Count() != LocalitiesPriceList.GroupBy(x => x.LocalityId).Count())
            {
                throw new Exception("Повторяются города в ценах");
            }

            var contract = new Contract(ConclusionDate, ExpirationDate, CustomerId, PerformerId);
            foreach (var priceLocalityPare in LocalitiesPriceList)
                contract.AddLocalitisList(priceLocalityPare.LocalityId, priceLocalityPare.Price);
            return contract;
        }

        public Contract ConvertToContractWithId(int id)
        {
            var contract = new ContractService().GetEntry(id);
            contract.Update(this);
            return contract;
        }
    }
}
