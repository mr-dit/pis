using NUnit.Framework;
using pis_web_api.Models.db;
using pis_web_api.Repositorys;

namespace pis.Repositorys
{
    public class GenderRepository : Repository<Gender>
    {
        private Lazy<Gender> male;
        private Lazy<Gender> female;

        public Gender MALE => male.Value;
        public Gender FEMALE => female.Value;

        public GenderRepository() : base ()
        {
            male = new Lazy<Gender>(() => GetGenderByName("Мужской"));
            female = new Lazy<Gender>(() => GetGenderByName("Женский"));
        }

        private Gender GetGenderByName(string name)
        {
            using (var db = new Context())
            {
                var gender = db.Genders.Where(gender => gender.NameGender == name).Single();
                if (gender is null)
                    throw new ArgumentException($"Нет пола с названием \"{name}\"");
                return gender;
            }
        }

        public List<Gender> GetGenders() 
        {
            return new List<Gender>()
            {
                MALE,
                FEMALE
            };
        }
    }
}
