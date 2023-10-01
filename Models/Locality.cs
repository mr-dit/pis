namespace pis.Models
{
    public class Locality
    {
        public int IdLocality { get; set; }
        public string NameLocality { get; set; }

        public Locality(int idLocality, string nameLocality) 
        {
            IdLocality = idLocality;
            NameLocality = nameLocality;
        }
            
    }
}
