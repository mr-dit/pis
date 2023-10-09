using System.ComponentModel.DataAnnotations;

namespace pis.Models
{
    public class Post
    {
        [Key]
        public int IdPost { get; set; }

        public string NamePost { get; set; }
    }
}
