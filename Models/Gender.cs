using System.ComponentModel.DataAnnotations;

namespace pis.Models
{
    public class Gender
    {
        [Key]
        public int IdGender { get; set; }
        public string NameGender { get; set; }

        public Gender(int idGender, string nameGender) 
        {
            IdGender = idGender;
            NameGender = nameGender;
        }
    }
}
