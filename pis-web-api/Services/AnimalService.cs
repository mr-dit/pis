using System;
using System.Runtime.InteropServices;
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

		public static (List<Animal>, int) GetAnimals(string filterField, string filterValue, string sortBy, bool isAscending, int pageNumber, int pageSize)
		{
			var filterFields = new Dictionary<string, (List<Animal>, int)>(StringComparer.InvariantCultureIgnoreCase)
			{
				[nameof(Animal.AnimalCategory)] =
				AnimalRepository.GetAnimalsByAnimalCategory(
					filterValue,
					pageNumber, pageSize, sortBy, isAscending),

				[nameof(Animal.ElectronicChipNumber)] =
				AnimalRepository.GetAnimalsByChipNumber(filterValue, pageNumber, pageSize, sortBy, isAscending),

				[nameof(Animal.AnimalName)] =
				AnimalRepository.GetAnimalsByName(filterValue, pageNumber, pageSize, sortBy, isAscending),

				[""] = 
				AnimalRepository.GetAnimalsByDefault(pageNumber, pageSize, sortBy, isAscending)

            };


			return filterFields[filterField];
		}


		public static bool ChangeEntry(Animal animal)
		{
			bool status = AnimalRepository.UpdateAnimal(animal);
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

