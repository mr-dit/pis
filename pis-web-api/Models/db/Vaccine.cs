using System.ComponentModel.DataAnnotations;

namespace pis_web_api.Models.db
{
    public class Vaccine
    {
        [Key]
        public int IdVaccine { get; set; }
        public string NameVaccine { get; set; }
        public int ValidDaysVaccine { get; set; }

        public Vaccine(string nameVaccine, int validDaysVaccine)
        {
            NameVaccine = nameVaccine;
            ValidDaysVaccine = validDaysVaccine;
        }
    }
}
