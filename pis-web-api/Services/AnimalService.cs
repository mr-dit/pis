using System;
using System.Runtime.InteropServices;
using pis.Models;
using pis.Repositorys;
using pis_web_api.Repositorys;
using pis_web_api.Services;

namespace pis.Services
{
	public class AnimalService : Service<Animal>
	{
        private AnimalRepository _repositoryAnimal;
		
		public AnimalService()
		{
			_repositoryAnimal = new AnimalRepository();
			_repository = _repositoryAnimal;
		}

		public (List<Animal>, int) GetAnimals(string filterField, string filterValue, string sortBy, bool isAscending, int pageNumber, int pageSize)
		{
			var filterFields = new Dictionary<string, Func<string, int, int, string, bool, (List<Animal>, int)>>(StringComparer.InvariantCultureIgnoreCase)
			{
				[nameof(Animal.AnimalCategory)] = _repositoryAnimal.GetAnimalsByAnimalCategory, 

				[nameof(Animal.ElectronicChipNumber)] = _repositoryAnimal.GetAnimalsByChipNumber,

				[nameof(Animal.AnimalName)] = _repositoryAnimal.GetAnimalsByName,

				[nameof(Animal.Locality)] = _repositoryAnimal.GetAnimalsByLocality,

				[""] = _repositoryAnimal.GetAnimalsByDefault
            };
			return filterFields[filterField](filterValue, pageNumber, pageSize, sortBy, isAscending);
		}
	}
}

