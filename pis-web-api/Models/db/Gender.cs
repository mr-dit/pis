using System.ComponentModel.DataAnnotations;

namespace pis_web_api.Models.db
{
    public class Gender
    {
        [Key]
        public int IdGender { get; set; }
        public string NameGender { get; set; }

        public Gender(string nameGender)
        {
            NameGender = nameGender;
        }

        public Gender() { }
    }
}
