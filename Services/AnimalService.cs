using System;
using pis.Models;
using pis.Repositorys;

namespace pis.Services
{
	public class AnimalService
	{
        public static bool FillData(Animal animal)
		{
			bool status = AnimalRepository.NewEntry(animal);
			return status;
        }

        public static bool DeleteEntry(int id)
        {
	        bool status = AnimalRepository.DeleteEntry(id);
	        return status;
        }

        public static Animal? GetEntry(int id)
        {
            var entry = AnimalRepository.GetEntry(id);
            return entry;
        }

        public static List<Animal> GetAnimals(string filterField, string? filterValue, string sortBy, bool isAscending, int pageNumber, int pageSize)
        {
	        filterValue = filterValue?.ToLower();
	        
	        List<Animal> animals = AnimalRepository.GetAnimals();
        
	        if (!string.IsNullOrEmpty(filterField) && !string.IsNullOrEmpty(filterValue))
	        {
		        switch (filterField.ToLower())
		        {
			        case "animalcategory":
				        animals = animals.Where(a => a.AnimalCategory.ToLower().Contains(filterValue)).ToList();
				        break;
			        case "electronicchipnumber":
				        animals = animals.Where(a => a.ElectronicChipNumber.ToString().Contains(filterValue)).ToList();
				        break;
			        case "animalname":
				        animals = animals.Where(a => a.AnimalName.ToString().Contains(filterValue)).ToList();
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
				        animals = isAscending ? animals.OrderBy(a => a.RegistrationNumber).ToList() : animals.OrderByDescending(a => a.RegistrationNumber).ToList();
				        break;
			        case "Locality":
				        animals = isAscending ? animals.OrderBy(a => a.Locality).ToList() : animals.OrderByDescending(a => a.Locality).ToList();
				        break;
			        case "AnimalCategory":
				        animals = isAscending ? animals.OrderBy(a => a.AnimalCategory).ToList() : animals.OrderByDescending(a => a.AnimalCategory).ToList();
				        break;
			        case "Gender":
				        animals = isAscending ? animals.OrderBy(a => a.Gender).ToList() : animals.OrderByDescending(a => a.Gender).ToList();
				        break;
			        case "YearOfBirth":
				        animals = isAscending ? animals.OrderBy(a => a.YearOfBirth).ToList() : animals.OrderByDescending(a => a.YearOfBirth).ToList();
				        break;
			        case "ElectronicChipNumber":
				        animals = isAscending ? animals.OrderBy(a => a.ElectronicChipNumber).ToList() : animals.OrderByDescending(a => a.ElectronicChipNumber).ToList();
				        break;
			        case "AnimalName":
				        animals = isAscending ? animals.OrderBy(a => a.AnimalName).ToList() : animals.OrderByDescending(a => a.AnimalName).ToList();
				        break;
		        }
	        }
        
	        // Пагинация
	        animals = animals.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        
	        return animals;
        }
        
        
        public static bool ChangeEntry(Animal animal)
        {
	        bool status = AnimalRepository.ChangeEntry(animal);
	        return status;
        }
        
        public static int GetTotalAnimals(string filterField, string? filterValue)
        {
	        filterValue = filterValue?.ToLower();
			
	        var animals = AnimalRepository.GetAnimals();

	        if (!string.IsNullOrEmpty(filterField) && !string.IsNullOrEmpty(filterValue))
	        {
		        switch (filterField.ToLower())
		        {
			        case "AnimalCategory":
				        animals = animals.Where(a => a.AnimalCategory.ToLower().Contains(filterValue)).ToList();
				        break;
			        case "ElectronicChipNumber":
				        animals = animals.Where(a => a.ElectronicChipNumber.ToString().Contains(filterValue)).ToList();
				        break;
			        case "AnimalName":
				        animals = animals.Where(a => a.AnimalName.ToString().Contains(filterValue)).ToList();
				        break;
			        // Добавьте остальные варианты полей
			        default:
				        break;
		        }
	        }

	        return animals.Count;
        }

        public AnimalService()
		{
			
		}
	}
}

