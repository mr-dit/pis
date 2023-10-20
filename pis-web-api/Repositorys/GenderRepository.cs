using pis.Models;

namespace pis.Repositorys
{
    public class GenderRepository
    {
        private static Lazy<Gender> male = new Lazy<Gender>(() => GetGenderByName("Мужской"));
        private static Lazy<Gender> female = new Lazy<Gender> (() => GetGenderByName("Женский"));

        public static Gender MALE => male.Value;
        public static Gender FEMALE => female.Value;

        public static void AddGender(Gender name)
        {
            using (var db = new Context())
            {
                db.Genders.Add(name);
                db.SaveChanges();
            }
        }

        private static Gender GetGenderByName(string name)
        {
            using (var db = new Context())
            {
                var gender = db.Genders.Where(gender => gender.NameGender == name).Single();
                if (gender is null)
                    throw new ArgumentException($"Нет пола с названием \"{name}\"");
                return gender;
            }
        }
    }
}
