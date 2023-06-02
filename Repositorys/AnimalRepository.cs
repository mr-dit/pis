using System;
using pis.Models;
namespace pis.Repositorys
{
	public class AnimalRepository
	{

		private static List<Animal> animals = new List<Animal>
		{
			new Animal(1, "Omsk", "Cat", "Boy", 2020, 1, "kot", "photo", "tail"),
            new Animal(2, "Tyumen", "Cat", "Girl", 2022, 2, "kot", "photo", "tail"),
            new Animal(3, "Omsk", "Dog", "Boy", 2021, 3, "kot", "photo", "tail")
        };


		public static bool NewEntry(Animal animal)
		{
            animals.Add(animal);
			return true;
        }

        public static bool DeleteEntry(int id)
        {
            var foundAnimal = AnimalRepository.animals.FirstOrDefault(a => a.RegistrationNumber == id);
            if (foundAnimal != null)
            {
                AnimalRepository.animals.Remove(foundAnimal);
                Console.WriteLine("Объект Animal удален.");
                return true;
            }
            else
            {
                Console.WriteLine("Объект Animal не найден.");
            }
            return false;
        }

        public static Animal? GetEntry(int id)
        {
            var foundAnimal = AnimalRepository.animals.FirstOrDefault(a => a.RegistrationNumber == id);
            return foundAnimal;
        }
        
        public static List<Animal> GetAnimals()
        {
	        var foundAnimal = animals;
	        return foundAnimal;
        }
        
        public static bool ChangeEntry(Animal animal)
        {
	        var foundAnimal = AnimalRepository.animals.FirstOrDefault(a => a.RegistrationNumber == animal.RegistrationNumber);
	        if (foundAnimal != null)
	        {
		        foundAnimal = animal;
		        
		        animals.RemoveAll(a => a.RegistrationNumber == animal.RegistrationNumber);
		        animals.Add(foundAnimal);
		        return true;
	        }
	        return false;
        }


        public AnimalRepository()
		{
		}
	}
}

