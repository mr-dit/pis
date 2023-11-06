using pis_web_api.Models.db;

namespace pis_web_api.Models.post
{
    public class AnimalCategoryPost
    {
        public string NameAnimalCategory { get; set; } = "";

        public AnimalCategoryPost() { }

        public AnimalCategory ConvertToAnimalCategory()
        {
            return new AnimalCategory(NameAnimalCategory);
        }

        public AnimalCategory ConvertToAnimalCategoryWithId(int id)
        {
            var category = this.ConvertToAnimalCategory();
            category.IdAnimalCategory = id;
            return category;
        }
    }
}
