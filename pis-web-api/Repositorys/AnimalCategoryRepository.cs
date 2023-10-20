using pis.Models;

namespace pis.Repositorys
{
    public class AnimalCategoryRepository
    {
        private static Lazy<AnimalCategory> cat = new Lazy<AnimalCategory>(() => GetAnimalCategoryByName("Кот"));
        public static AnimalCategory CAT => cat.Value;

        private static Lazy<AnimalCategory> dog = new Lazy<AnimalCategory>(() => GetAnimalCategoryByName("Собака"));
        public static AnimalCategory DOG => dog.Value;

        public static AnimalCategory GetAnimalCategoryByName (string name)
        {
            using (Context db = new Context())
            {
                var category = db.AnimalCategories.Where(category => category.NameAnimalCategory == name).FirstOrDefault();
                if (category is null)
                    throw new ArgumentException($"Нет категории животного с названием \"{name}\"");
                return category;
            }
        }

        public static void AddAnimalCategory(AnimalCategory animalCategory)
        {
            using (Context db = new Context())
            {
                db.AnimalCategories.Add(animalCategory);
                db.SaveChanges();
            }
        }
    }
}
