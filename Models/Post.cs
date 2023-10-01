namespace pis.Models
{
    public class Post
    {
        public int IdPost { get; set; }
        public string NamePost { get; set; }

        public Post(int idPost, string namePost)
        {
            IdPost = idPost;
            NamePost = namePost;
        }
    }
}
