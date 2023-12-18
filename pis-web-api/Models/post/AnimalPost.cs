using pis_web_api.Models.db;

namespace pis_web_api.Models.post
{
    public class AnimalPost
    {
        public int LocalityId { get; set; }

        public int AnimalCategoryId { get; set; }

        public int GenderId { get; set; }

        public int YearOfBirth { get; set; }

        public string ElectronicChipNumber { get; set; }

        public string AnimalName { get; set; }

        public string PhotoPath { get; set; } = "";

        public string SpecialSigns { get; set; } = "";

        public AnimalPost() { }

        public Animal ConvertToAnimal()
        {
            var animal = new Animal(AnimalName, LocalityId, AnimalCategoryId, GenderId, YearOfBirth, ElectronicChipNumber, PhotoPath, SpecialSigns);
            return animal;
        }

        public Animal ConvertToAnimalWithId(int id)
        {
            var animal = this.ConvertToAnimal();
            animal.RegistrationNumber = id;
            return animal;
        }
    }
}
