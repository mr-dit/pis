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
                db.SaveChanges();
            }
        }

        public static void DeletePost(Post post)
        {
            using (var db = new Context())
            {
                db.Posts.Remove(post);
                db.SaveChanges();
            }
        }

        public static void UpdatePost(Post post)
        {
            using (var db = new Context())
            {
                db.Posts.Update(post);
                db.SaveChanges();
            }
        }

        public static Post KURATOR_VETSERVICE => GetPostByName("Куратор ВетСлужбы");
        public static Post KURATOR_TRAPPING => GetPostByName("Куратор по отлову");
        public static Post KURATOR_SHELTER => GetPostByName("Куратор приюта");
        public static Post OPERATOR_VETSERVICE => GetPostByName("Оператор ВетСлужбы");
        public static Post OPERATOR_TRAPPING => GetPostByName("Оператор по отлову");
        public static Post SIGNER_VETSERVICE => GetPostByName("Подписант ВетСлужбы");
        public static Post SIGNER_TRAPPING => GetPostByName("Подписант по отлову");
        public static Post SIGNER_SHELTER => GetPostByName("Подписант приюта");
        public static Post KURATOR_OMSU => GetPostByName("Куратор ОМСУ");
        public static Post OPERATOR_OMSU => GetPostByName("Оператор ОМСУ");
        public static Post SIGNER_OMSU => GetPostByName("Подписант ОМСУ");
        public static Post OPERATOR_SHELTER => GetPostByName("Оператор приюта");
        public static Post DOCTOR => GetPostByName("Ветврач");
        public static Post DOCTOR_SHELTER => GetPostByName("Ветврач приюта");
    }
}
