using System;
namespace pis.Models
{
	public class Animal
	{
        public int RegistrationNumber { get; set; }

        public string Locality { get; set; } = "";

        public string AnimalCategory { get; set; } = "";

        public string Gender { get; set; } = "";

        public int YearOfBirth { get; set; }

        public int ElectronicChipNumber { get; set; }

        public string AnimalName { get; set; } = "";

        public string Photos { get; set; } = "";

        public string SpecialSigns { get; set; } = "";


        public Animal(int id, string locality, string animalCategory, string gender, int yearOfBirth, int electronicChipNumber, string animalName, string photos, string specialSigns)
		{
            RegistrationNumber = id;
            Locality = locality;
            AnimalCategory = animalCategory;
            Gender = gender;
            YearOfBirth = yearOfBirth;
            ElectronicChipNumber = electronicChipNumber;
            AnimalName = animalName;
            Photos = photos;
            SpecialSigns = specialSigns;
        }

        public Animal()
        {
            
        }
    }
}

