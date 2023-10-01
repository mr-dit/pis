namespace pis.Models
{
    public class AnimalCategory
    {
        public int IdAnimalCategory { get; set; }
        public string NameAnimalCategory { get; set; }
        
        public AnimalCategory(int idAnimalCategory, string name) 
        {
            IdAnimalCategory = idAnimalCategory;
            NameAnimalCategory = name;
        }
    }
}
