using System;
using pis_web_api.Models;
namespace pis_web_api.Repositorys
{
	public class AnimalRepository
	{

		private static List<Animal> animals = new List<Animal>
		{
            new Animal(1, "Омск", "Кот", "Мальчик", 2019, 12345, "Кот", "photo1.png", "очень маленькая"),
            new Animal(2, "Ишим", "Собака", "Девочка", 2020, 67890, "Лайка", "photo2.png", "нет особых примет"),
            new Animal(3, "Тобольск", "Кот", "Мальчик", 2018, 54321, "Мурзик","photo3.png", "большая лапа"),
            new Animal(4, "Тюмень", "Собака", "Мальчик", 2021, 98765, "Рекс", "photo4.png", "шрам на ухе"),
            new Animal(5, "Тюмень", "Кот", "Девочка", 2017, 24680, "Матильда", "photo5.png", "белые лапки"),
            new Animal(6, "Екатеринбург", "Собака", "Мальчик", 2022, 13579, "Шарик", "photo.png", "пятно на спине"),
            new Animal(7, "Омск", "Кот", "Девочка", 2016, 86420, "Мурка", "photo7.png", "на носу сердечко"),
            new Animal(8, "Омск", "Собака", "Девочка", 2023, 97531, "Бобик", "photo8.png", "длинный хвост"),
            new Animal(9, "Омск", "Кот", "Мальчик", 2020, 12345, "Вася", "photo9.png", "круглые глаза"),
            new Animal(10, "Тюмень", "Собака", "Девочка", 2019, 67890, "Белка", "photo10.png", "длинные уши"),
            new Animal(11, "Тюмень", "Кот", "Мальчик", 2019, 27990, "Пухляш", "photo11.png", "черная мордашка")

        };

        
		public static bool NewEntry(Animal animal)
		{
			int maxRegistrationNumber = animals.Max(a => a.RegistrationNumber);
			int nextRegistrationNumber = maxRegistrationNumber + 1;

			animal.RegistrationNumber = nextRegistrationNumber;
			
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
            Console.WriteLine("Объект Animal не найден.");
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
		        foundAnimal.Locality = animal.Locality;
		        foundAnimal.AnimalCategory = animal.AnimalCategory;
		        foundAnimal.Gender = animal.Gender;
		        foundAnimal.YearOfBirth = animal.YearOfBirth;
		        foundAnimal.ElectronicChipNumber = animal.ElectronicChipNumber;
		        foundAnimal.AnimalName = animal.AnimalName;
		        foundAnimal.Photos = animal.Photos;
		        foundAnimal.SpecialSigns = animal.SpecialSigns;
		        return true;
	        }
	        return false;
        }


        public AnimalRepository()
		{
		}
	}
}

