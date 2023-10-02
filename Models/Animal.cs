using System;
namespace pis.Models
{
	public class Animal
	{
        public int RegistrationNumber { get; set; }

        public Locality Locality { get; set; }

        public AnimalCategory AnimalCategory { get; set; }

        public Gender Gender { get; set; }

        public int YearOfBirth { get; set; }

        public string ElectronicChipNumber { get; set; }

        public string AnimalName { get; set; }

        public byte[] Photo { get; set; }

        public string SpecialSigns { get; set; }
        
        public List<Vaccination> Vaccinations { get; set; }


        public Animal(int id, Locality locality, AnimalCategory animalCategory, Gender gender,
            int yearOfBirth, string electronicChipNumber, string animalName, byte[] photo, string specialSigns)
		{
            RegistrationNumber = id;
            Locality = locality;
            AnimalCategory = animalCategory;
            Gender = gender;
            YearOfBirth = yearOfBirth;
            ElectronicChipNumber = electronicChipNumber;
            AnimalName = animalName;
            Photo = photo;
            SpecialSigns = specialSigns;
        }
    }
}

