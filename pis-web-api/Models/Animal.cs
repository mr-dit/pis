using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace pis.Models
{
	public class Animal
	{
        [Key]
        public int RegistrationNumber { get; set; }

        public int LocalityId { get; set; }

        public Locality? Locality { get; set; }

        public int AnimalCategoryId { get; set; }

        public AnimalCategory? AnimalCategory { get; set; }

        public int GenderId { get; set; }

        public Gender? Gender { get; set; }

        public int YearOfBirth { get; set; }

        public string ElectronicChipNumber { get; set; }

        public string AnimalName { get; set; }

        public string? PhotoPath { get; set; }

        public string? SpecialSigns { get; set; }

        public List<Vaccination> Vaccinations { get; set; }

        public Animal(string animalName, Locality locality, AnimalCategory animalCategory, 
            Gender gender, int yearOfBirth, string electronicChipNumber)
        {
            AnimalName = animalName;
            Locality = locality;
            AnimalCategory = animalCategory;
            Gender = gender;
            YearOfBirth = yearOfBirth;
            ElectronicChipNumber = electronicChipNumber;
        }

        public Animal(string animalName, int localityId, int animalCategoryId,
            int genderId, int yearOfBirth, string electronicChipNumber)
        {
            AnimalName = animalName;
            LocalityId = localityId;
            AnimalCategoryId = animalCategoryId;
            GenderId = genderId;
            YearOfBirth = yearOfBirth;
            ElectronicChipNumber = electronicChipNumber;
        }

        public Animal()
        {

        }
    }
}

