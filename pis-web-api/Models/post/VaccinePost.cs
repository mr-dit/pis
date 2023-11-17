using pis_web_api.Models.db;

namespace pis_web_api.Models.post
{
    public class VaccinePost
    {
        public string NameVaccine { get; set; }
        public int ValidDaysVaccine { get; set; }

        public Vaccine ConvertToVaccine()
        {
            return new Vaccine(NameVaccine, ValidDaysVaccine);
        }
    }
}
