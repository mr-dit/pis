using System;
using System.Drawing;
using System.IO;
using Microsoft.EntityFrameworkCore;
using pis.Models;
namespace pis.Repositorys
{
	public class AnimalRepository
	{
        public static bool CreateAnimal(Animal animal)
        {
            using (Context db = new Context())
            {
                try
                {
                    db.Animals.Add(animal);
                }
                catch (Exception)
                {

                    return false;
                }
                db.SaveChanges();
                return true;
            }
        }

        public static Animal GetAnimalById(int animalId) 
        {
            using (Context db = new Context())
            {
                var animal = db.Animals
                    .Where(animal => animal.RegistrationNumber == animalId)
                    .Include(x => x.Gender)
                    .Include(x => x.AnimalCategory)
                    .Include(x => x.Locality)
                    .Include(x => x.Vaccinations)
                    .Single();
                if (animal == null) 
                    throw new ArgumentException($"Не существует животного с Id {animalId}");
                return animal;
            }
        }

        public static Animal GetAnimalByChipNumber(string chipNumber)
        {
            using (Context db = new Context())
            {
                var animal = db
                    .Animals
                    .Where(animal => animal.ElectronicChipNumber == chipNumber)
                    .Include(x => x.Gender)
                    .Include (x => x.AnimalCategory)
                    .Include(x => x.Locality)
                    .Single();
                if (animal is null)
                    throw new ArgumentException($"Не существует животного с чипом {chipNumber}");
                return animal;
            }
        }

        public static List<Animal> GetAnimalsByName(string animalName)
        {
            using (Context db = new Context())
            {
                var animal = db.Animals.Where(animal => animal.AnimalName.Contains(animalName));
                if (animal.Count() == 0)
                    throw new ArgumentException($"Не существует животных с именем {animalName}");
                return animal.ToList();
            }
        }

        public static List<Animal> GetAnimalsByAnimalCategory(AnimalCategory animalCategory)
        {
            using (Context db = new Context())
            {
                var animal = db.Animals.Where(animal => animal.AnimalCategory.Equals(animalCategory));
                if (animal.Count() == 0)
                    throw new ArgumentException($"Не существует животного с именем {animalCategory}");
                return animal.ToList();
            }
        }

        public static List<Animal> GetAnimalsByChipNymber(string chipNumber)
        {
            using (Context db = new Context())
            {
                var animal = db.Animals.Where(animal => animal.ElectronicChipNumber == chipNumber);
                if (animal.Count() == 0)
                    throw new ArgumentException($"Не существует животного с чипом {chipNumber}");
                return animal.ToList();
            }
        }

        public static bool DeleteAnimal(Animal animal)
        {
            using (Context db = new Context())
            {
                try
                {
                    db.Animals.Remove(animal);
                }
                catch (Exception)
                {
                    return false;
                }
                db.SaveChanges();
                return true;
            }
        }

        public static void DeleteAnimal(int animalId)
        {
            using (Context db = new Context()) 
            {
                var animal = GetAnimalById(animalId);
                DeleteAnimal(animal);
            }
        }

        public static bool ChangeAnimal(Animal animal)
        {
            using (Context db = new Context()) 
            {
                try
                {
                    db.Animals.Update(animal);
                }
                catch (Exception)
                {
                    return false;
                }
                db.SaveChanges();
                return true;
            }
        }

        //public static bool NewEntry(Animal animal)
        //{
        //	int maxRegistrationNumber = animals.Max(a => a.RegistrationNumber);
        //	int nextRegistrationNumber = maxRegistrationNumber + 1;

        //	animal.RegistrationNumber = nextRegistrationNumber;

        //          animals.Add(animal);
        //	return true;
        //      }

        //      public static bool DeleteEntry(int id)
        //      {
        //          var foundAnimal = AnimalRepository.animals.FirstOrDefault(a => a.RegistrationNumber == id);
        //          if (foundAnimal != null)
        //          {
        //              AnimalRepository.animals.Remove(foundAnimal);
        //              Console.WriteLine("Объект Animal удален.");
        //              return true;
        //          } 
        //          Console.WriteLine("Объект Animal не найден.");
        //          return false;
        //      }

        //      public static Animal? GetEntry(int id)
        //      {
        //          var foundAnimal = AnimalRepository.animals.FirstOrDefault(a => a.RegistrationNumber == id);
        //          return foundAnimal;
        //      }

        //      public static List<Animal> GetAnimals()
        //      {
        //       var foundAnimal = animals;
        //       return foundAnimal;
        //      }

        //      public static bool ChangeEntry(Animal animal)
        //      {
        //       var foundAnimal = AnimalRepository.animals.FirstOrDefault(a => a.RegistrationNumber == animal.RegistrationNumber);
        //       if (foundAnimal != null)
        //       {
        //        foundAnimal.Locality = animal.Locality;
        //        foundAnimal.AnimalCategory = animal.AnimalCategory;
        //        foundAnimal.Gender = animal.Gender;
        //        foundAnimal.YearOfBirth = animal.YearOfBirth;
        //        foundAnimal.ElectronicChipNumber = animal.ElectronicChipNumber;
        //        foundAnimal.AnimalName = animal.AnimalName;
        //        foundAnimal.Photos = animal.Photos;
        //        foundAnimal.SpecialSigns = animal.SpecialSigns;
        //        return true;
        //       }
        //       return false;
        //      }


        //      public AnimalRepository()
        //{
        //}
    }
}

