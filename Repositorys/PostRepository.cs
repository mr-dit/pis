using Npgsql.PostgresTypes;
using pis.Models;

namespace pis.Repositorys
{
    public class PostRepository
    {
        public static Post GetPostByName(string name)
        {
            using (var db = new Context())
            {
                var post = db.Posts.Where(x => x.NamePost == name).Single();
                if (post == null)
                    throw new ArgumentException($"Не существует должности с названием {name}");
                return post;
            }
        }

        public static void AddPost(Post post)
        {
            using (var db = new Context())
            {
                db.Posts.Add(post);
                db.SaveChangesAsync();
            }
        }

        public static void DeletePost(Post post)
        {
            using (var db = new Context())
            {
                db.Posts.Remove(post);
                db.SaveChangesAsync();
            }
        }

        public static void UpdatePost(Post post)
        {
            using (var db = new Context())
            {
                db.Posts.Update(post);
                db.SaveChangesAsync();
            }
        }
    }
}
