namespace pis.Models
{
    public class Vaccine
    {
        public int IdVaccine { get; set; }
        public string NameVaccine { get; set; }
        public int ValidDaysVaccine { get; set; }

        public Vaccine(int idVaccine, string nameVaccine, int validDaysVaccine) 
        {
            IdVaccine = idVaccine;
            NameVaccine = nameVaccine;
            ValidDaysVaccine = validDaysVaccine;
        }
    }
}
