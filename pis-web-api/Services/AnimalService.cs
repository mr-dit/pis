using System;
using System.Runtime.InteropServices;
using pis.Models;
using pis.Repositorys;
using pis_web_api.Repositorys;

namespace pis.Services
{
	public class AnimalService
	{
        private AnimalRepository repository;
		
		public AnimalService()
		{
			repository = new AnimalRepository();
		}

        public bool FillData(Animal animal)
		{
			bool status = repository.Add(animal);
			return status;
		}

		public bool DeleteEntry(int id)
		{
			bool status = repository.Remove
				(repository.GetById(id));
			return status;
		}

		public Animal? GetEntry(int id)
		{
			var entry = repository.GetById(id);
			return entry;
		}

		public (List<Animal>, int) GetAnimals(string filterField, string filterValue, string sortBy, bool isAscending, int pageNumber, int pageSize)
		{
			var filterFields = new Dictionary<string, Func<string, int, int, string, bool, (List<Animal>, int)>>(StringComparer.InvariantCultureIgnoreCase)
			{
				[nameof(Animal.AnimalCategory)] = repository.GetAnimalsByAnimalCategory, 

				[nameof(Animal.ElectronicChipNumber)] = repository.GetAnimalsByChipNumber,

				[nameof(Animal.AnimalName)] = repository.GetAnimalsByName,

				[nameof(Animal.Locality)] = repository.GetAnimalsByLocality,

				[""] = repository.GetAnimalsByDefault
            };
			return filterFields[filterField](filterValue, pageNumber, pageSize, sortBy, isAscending);
		}


		public bool ChangeEntry(Animal animal)
		{
			bool status = repository.Update(animal);
			return status;
		}
	}
}

