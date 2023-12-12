using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace pis_web_api.Models.db
{
    public class Animal : IJurnable
    {
        [NotMapped]
        public int Id { get => RegistrationNumber; }
        [NotMapped]
        public static TableNames TableName { get => TableNames.Животные; }

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

        public List<Vaccination>? Vaccinations { get; set; }

        public AnimalStatus Status { get; set; }
        

        public Animal(string animalName, Locality locality, AnimalCategory animalCategory,
            Gender gender, int yearOfBirth, string electronicChipNumber)
        {
            AnimalName = animalName;
            Locality = locality;
            AnimalCategory = animalCategory;
            Gender = gender;
            YearOfBirth = yearOfBirth;
            ElectronicChipNumber = electronicChipNumber;
            Status = AnimalStatus.Не_проводилась;
        }

        public Animal(int regNumber, string animalName, int localityId, int animalCategoryId,
            int genderId, int yearOfBirth, string electronicChipNumber, string photoPath, string signs)
        {
            RegistrationNumber = regNumber;
            AnimalName = animalName;
            LocalityId = localityId;
            AnimalCategoryId = animalCategoryId;
            GenderId = genderId;
            YearOfBirth = yearOfBirth;
            ElectronicChipNumber = electronicChipNumber;
            PhotoPath = photoPath;
            SpecialSigns = signs;
            Status = AnimalStatus.Не_проводилась;
        }

        public Animal(string animalName, int localityId, int animalCategoryId,
            int genderId, int yearOfBirth, string electronicChipNumber, string photoPath, string signs)
        {
            AnimalName = animalName;
            LocalityId = localityId;
            AnimalCategoryId = animalCategoryId;
            GenderId = genderId;
            YearOfBirth = yearOfBirth;
            ElectronicChipNumber = electronicChipNumber;
            PhotoPath = photoPath;
            SpecialSigns = signs;
            Status = AnimalStatus.Не_проводилась;
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
            Status = AnimalStatus.Не_проводилась;
        }

        public Animal()
        {

        }

        public void AddVaccination(Vaccination vaccination)
        {
            Vaccinations.Add(vaccination);
        }

        public void ChangeStatus(AnimalStatus status)
        {
            Status = status;
        }

        public override string ToString()
        {
            string description = "";
            description += ElectronicChipNumber + ";";
            description += AnimalName + ";";
            description += YearOfBirth + ";";
            description += AnimalCategory.NameAnimalCategory + ";";
            description += Gender.NameGender + ";";
            description += SpecialSigns + ";";
            description += Locality.NameLocality + ";";
            return description;
        }
    }
}

