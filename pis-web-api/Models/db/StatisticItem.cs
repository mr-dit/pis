using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace pis_web_api.Models.db
{
    public class StatisticItem
    {
        [Key]
        public int Id { get; set; }

        public string VaccineName { get; set; }
        public decimal Price { get; set; }

        public int StatisticaHolderId { get; set; }
        public StatisticaHolder StatisticaHolder { get; set; }

        public StatisticItem() { }

        public StatisticItem(Vaccine vaccine, decimal price)
        {
            VaccineName = vaccine.NameVaccine;
            Price = price;
        }
    }
}
