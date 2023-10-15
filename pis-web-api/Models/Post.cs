using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pis.Models
{
    public class Post
    {
        [Key]
        public int IdPost { get; set; }

        public string NamePost { get; set; }

        public Post (string namePost)
        {
            NamePost = namePost;
        }

        public Post() { }
    }
}
