using pis.Models;

namespace pis.Repositorys
{
    public class AnimalCategoryRepository
    {
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
                db.SaveChangesAsync();
            }
        }
        public static AnimalCategory CAT() => GetAnimalCategoryByName("Кот");
        public static AnimalCategory DOG() => GetAnimalCategoryByName("Собака");
    }
}
