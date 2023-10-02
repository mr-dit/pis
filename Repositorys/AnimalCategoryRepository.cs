using pis.Models;

namespace pis.Repositorys
{
    public class AnimalCategoryRepository
    {
        public static List<AnimalCategory> animalCategories = new List<AnimalCategory>
        {
            new AnimalCategory(1, "Кошка"),
            new AnimalCategory(2, "Собака")
        };

        public static AnimalCategory GetAnimalCategoryByName (string name)
        {
            var category = animalCategories.Where(category => category.NameAnimalCategory == name).FirstOrDefault();
            if(category is null)
                throw new ArgumentException($"Нет категории животного с названием \"{name}\"");
            return category;
        }
    }
}
