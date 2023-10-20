using pis_web_api.Models;
using System.ComponentModel.DataAnnotations;

namespace pis.Models
{
    public class Locality
    {
        [Key]
        public int IdLocality { get; set; }
        public string NameLocality { get; set; }

        public List<LocalitisListForContract> Contracts { get; set; }

        public Locality() { }
        public Locality(string name)
        {
            NameLocality = name;
        }
        //public Locality(int idLocality, string nameLocality) 
        //{
        //    IdLocality = idLocality;
        //    NameLocality = nameLocality;
        //}
            
    }
}
