using System;
using System.Runtime.InteropServices;
using pis.Repositorys;
using pis_web_api.Models.db;
using pis_web_api.Models.post;
using pis_web_api.References;
using pis_web_api.Repositorys;
using pis_web_api.Services;

namespace pis.Services
{
    public class AnimalService : Service<Animal>
	{
        private AnimalRepository _repositoryAnimal;
		private VaccinationRepository _vaccinationRepository;
		
		public AnimalService()
		{
			_repositoryAnimal = new AnimalRepository();
			_repository = _repositoryAnimal;
            _vaccinationRepository = new VaccinationRepository();
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

		public (List<Animal>, int) GetAnimalsByOrg(string filterField, string filterValue, string sortBy, 
			bool isAscending, int pageNumber, int pageSize, UserPost user)
		{
            var filterFields = new Dictionary<string, Func<string, int, int, string, bool, UserPost, (List<Animal>, int)>>(StringComparer.InvariantCultureIgnoreCase)
            {
                [nameof(Animal.AnimalCategory)] = _repositoryAnimal.GetAnimalsByAnimalCategory,

                [nameof(Animal.ElectronicChipNumber)] = _repositoryAnimal.GetAnimalsByChipNumber,

                [nameof(Animal.AnimalName)] = _repositoryAnimal.GetAnimalsByName,

                [nameof(Animal.Locality)] = _repositoryAnimal.GetAnimalsByLocality,

                [""] = _repositoryAnimal.GetAnimalsByDefault
            };
            return filterFields[filterField](filterValue, pageNumber, pageSize, sortBy, isAscending, user);
        }

		public void CheckStatus(List<Animal> animals)
		{
			foreach (var animal in animals)
			{
				if(animal.Vaccinations?.Count() == 0)
				{
                    animal.Status = AnimalStatus.Не_проводилась;
					continue;
                }

				var span = animal.Vaccinations?.Last().VaccinationValidDate.ToDateTime(new TimeOnly()) - DateTime.Now;

                if (span > TimeSpan.FromDays(10))
				{
					animal.Status = AnimalStatus.Вакцинировано;
				}
				else if(span <= TimeSpan.FromDays(10) && span >= TimeSpan.Zero)
				{
					animal.Status = AnimalStatus.Приближается_срок_вакцинирования;
				}
				else
				{
					animal.Status = AnimalStatus.Не_проводилась;
				}
			}
			_repositoryAnimal.dbSet.UpdateRange(animals);
            _repositoryAnimal.db.SaveChanges();
        }

		public Animal GetAnimal(int id)
		{
			return _repositoryAnimal.GetById(id);
		}
    }
}

