using System;
using pis.Models;
using pis.Repositorys;

namespace pis.Services
{
	public class AnimalService
	{
		private static List<Animal> Animals { get; set; } = new List<Animal>();

		public static bool FillData(Animal animal)
		{
			bool status = AnimalRepository.CreateAnimal(animal);
			return status;
		}

		public static bool DeleteEntry(int id)
		{
			bool status = AnimalRepository.DeleteAnimal
				(AnimalRepository.GetAnimalById(id));
			return status;
		}

		public static Animal? GetEntry(int id)
		{
			var entry = AnimalRepository.GetAnimalById(id);
			return entry;
		}

		public static List<Animal> GetAnimals(string filterField, string? filterValue, string sortBy, bool isAscending, int pageNumber, int pageSize)
		{
			filterValue = filterValue?.ToLower();

			if (!string.IsNullOrEmpty(filterField) && !string.IsNullOrEmpty(filterValue))
			{
				switch (filterField.ToLower())
				{
					case "animalcategory":
						Animals = AnimalRepository.GetAnimalsByAnimalCategory(AnimalCategoryRepository.GetAnimalCategoryByName(filterValue)).ToList();
						break;
					case "electronicchipnumber":
                        Animals = AnimalRepository.GetAnimalByChipNymber(filterValue).ToList();
						break;
					case "animalname":
                        Animals = AnimalRepository.GetAnimalsByName(filterValue).ToList();
						break;
					// Добавьте остальные варианты полей
					default:
						break;
				}
			}

			// Сортировка
			if (!string.IsNullOrEmpty(sortBy))
			{
				switch (sortBy)
				{
					case "RegistrationNumber":
                        Animals = isAscending ? Animals.OrderBy(a => a.RegistrationNumber).ToList() : Animals.OrderByDescending(a => a.RegistrationNumber).ToList();
						break;
					case "Locality":
						Animals = isAscending ? Animals.OrderBy(a => a.Locality).ToList() : Animals.OrderByDescending(a => a.Locality).ToList();
						break;
					case "AnimalCategory":
                        Animals = isAscending ? Animals.OrderBy(a => a.AnimalCategory).ToList() : Animals.OrderByDescending(a => a.AnimalCategory).ToList();
						break;
					case "Gender":
                        Animals = isAscending ? Animals.OrderBy(a => a.Gender).ToList() : Animals.OrderByDescending(a => a.Gender).ToList();
						break;
					case "YearOfBirth":
                        Animals = isAscending ? Animals.OrderBy(a => a.YearOfBirth).ToList() : Animals.OrderByDescending(a => a.YearOfBirth).ToList();
						break;
					case "ElectronicChipNumber":
                        Animals = isAscending ? Animals.OrderBy(a => a.ElectronicChipNumber).ToList() : Animals.OrderByDescending(a => a.ElectronicChipNumber).ToList();
						break;
					case "AnimalName":
                        Animals = isAscending ? Animals.OrderBy(a => a.AnimalName).ToList() : Animals.OrderByDescending(a => a.AnimalName).ToList();
						break;
				}
			}

            // Пагинация
            var animalsPag = Animals.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

			return animalsPag;
		}


		public static bool ChangeEntry(Animal animal)
		{
			bool status = AnimalRepository.ChangeAnimal(animal);
			return status;
		}

		public static int GetTotalAnimals(string filterField, string? filterValue)
		{
			return Animals.Count;
		}

		//      public AnimalService()
		//{

		//}
	}
}

