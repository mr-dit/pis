using pis_web_api.Models.db;
using pis_web_api.Repositorys;

namespace pis.Repositorys
{
    public class AnimalCategoryRepository : Repository<AnimalCategory>
    {
        private Lazy<AnimalCategory> cat;
        public AnimalCategory CAT => cat.Value;

        private Lazy<AnimalCategory> dog;
        public AnimalCategory DOG => dog.Value;

        public AnimalCategoryRepository()
        {
            this.cat = new Lazy<AnimalCategory>(() => GetAnimalCategoryByName("Кот"));
            this.dog = new Lazy<AnimalCategory>(() => GetAnimalCategoryByName("Собака"));
        }

        public AnimalCategory GetAnimalCategoryByName(string name)
        {
            using (Context db = new Context())
            {
                var category = db.AnimalCategories.Where(category => category.NameAnimalCategory == name).Single();
                return category;
            }
        }

        public List<AnimalCategory> GetAnimalCategories()
        {
            return db.Set<AnimalCategory>().ToList();
        }
    }
}
