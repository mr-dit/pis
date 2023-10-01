namespace pis.Models
{
    public class VaccinePriceListByLocality
    {
        public int IdVaccinePriceListByLocality {get; set;}
        public Vaccine Vaccine { get; set; }
        public Locality Locality { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }

        public VaccinePriceListByLocality(int idVaccinePriceListByLocality, Vaccine vaccine, Locality locality, DateTime date, decimal price)
        {
            IdVaccinePriceListByLocality = idVaccinePriceListByLocality;
            Vaccine = vaccine;
            Locality = locality;
            Date = date;
            Price = price;
        }

    }
}
