using pis.Models;
using System.Collections;

namespace pis_web_api.Models
{
    public class StatisticItem 
    {
        public string VaccineName { get; set; }
        public decimal Price { get; set; }

        public StatisticItem(Vaccine vaccine, decimal price)
        {
            VaccineName = vaccine.NameVaccine;
            Price = price;
        }
    }
}
