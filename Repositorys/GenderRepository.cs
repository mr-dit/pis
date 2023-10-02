using pis.Models;

namespace pis.Repositorys
{
    public class GenderRepository
    {
        private static List<Gender> genders = new List<Gender>()
        {
            new Gender(1, "Муж"),
            new Gender(2, "Жен")
        };

        public static Gender GetGenderByName(string name)
        {
            var gender = genders.Where(gender => gender.NameGender == name).FirstOrDefault();
            if (gender is null)
                throw new ArgumentException($"Нет пола с названием \"{name}\"");
            return gender;
        }
    }
}
