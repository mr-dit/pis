using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace pis_web_api.Models.db
{
    public class StatisticaHolder : IEnumerable<StatisticItem>
    {
        [Key]
        public int Id { get; set; }

        public string LocalityName { get; set; }
        public List<StatisticItem> VaccinePrice { get; }

        public int ReportId { get; set; }
        public Report Report { get; set; }

        public StatisticaHolder()
        {
        }
        public StatisticaHolder(Locality locality)
        {
            LocalityName = locality.NameLocality;
            VaccinePrice = new List<StatisticItem>();
        }

        public void AddVaccinePrice(Vaccine vaccine, decimal price)
        {
            var statItem = new StatisticItem(vaccine, price);
            VaccinePrice.Add(statItem);
        }

        public IEnumerator<StatisticItem> GetEnumerator()
        {
            return VaccinePrice.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        //private List<StatisticItem> values;
        //public StatisticaHolder() 
        //{
        //    values = new List<StatisticItem>();
        //}

        //public List<StatisticItem> GetStatisticaItemsByLocalityName(string localityName)
        //{
        //    return values
        //        .Where(x => x.VaccinePrice
        //                    .Any(x => x.Item1 == localityName))
        //        .ToList();
        //}

        //public void Add(StatisticItem item)
        //{
        //    values.Add(item);
        //}
    }
}
