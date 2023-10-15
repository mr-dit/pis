using pis.Models;

namespace pis.Repositorys
{
    public class GenderRepository
    {
        //private static List<Gender> genders = new List<Gender>()
        //{
        //    new Gender(1, "Муж"),
        //    new Gender(2, "Жен")
        //};

        public static void AddGender(Gender name)
        {
            using (var db = new Context())
            {
                db.Genders.Add(name);
                db.SaveChanges();
            }
        }

        public static Gender GetGenderByName(string name)
        {
            using (var db = new Context())
            {
                var gender = db.Genders.Where(gender => gender.NameGender == name).Single();
                if (gender is null)
                    throw new ArgumentException($"Нет пола с названием \"{name}\"");
                return gender;
            }
        }

        public static Gender MALE => GetGenderByName("Мужской");
        public static Gender FEMALE => GetGenderByName("Женский");
    }
}
