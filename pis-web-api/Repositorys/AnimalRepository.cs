using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using pis.Models;
namespace pis.Repositorys
{
    public delegate void AnimalAction(Context db, Animal animal);

    public class AnimalRepository
    {
        private static bool DoWorkAnimal(Animal animal, AnimalAction action)
        {
            using (var db = new Context())
            {
                try
                {
                    action(db, animal);
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }

        public static bool CreateAnimal(Animal animal) =>
            DoWorkAnimal(animal, (db, a) => db.Animals.Add(a));

        public static bool DeleteAnimal(Animal animal) =>
            DoWorkAnimal(animal, (db, a) => db.Remove(a));

        public static bool DeleteAnimalById(int id) =>
            DeleteAnimal(GetAnimalById(id));

        public static bool UpdateAnimal(Animal animal) =>
            DoWorkAnimal(animal, (db, a) => db.Update(a));

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
                return animal;
            }
        }

        private static (List<Animal>, int) GetAnimalsByValue(Func<Animal, bool> value, int pageNumber, int pageSize, string sortBy, bool isAscending)
        {
            using (Context db = new Context())
            {
                var allAnimals = db.Animals
                    .Include(x => x.Locality)
                    .Include(x => x.AnimalCategory)
                    .Include(x => x.Gender)
                    .Where(value)
                    .SortBy(sortBy, isAscending);
                var animals = allAnimals.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                return (animals, allAnimals.Count());
            }
        }

        public static (List<Animal>, int) GetAnimalsByAnimalCategory(
            string category, int pageNumber,
            int pageSize, string sortBy, bool isAscending) =>
            GetAnimalsByValue(animal => animal.AnimalCategory.NameAnimalCategory.Contains(category),
                pageNumber, pageSize, sortBy, isAscending);

        public static (List<Animal>, int) GetAnimalsByChipNumber(
            string chipNumber, int pageNumber, int pageSize, string sortBy, bool isAscending) =>
            GetAnimalsByValue(animal => animal.ElectronicChipNumber.Contains(chipNumber),
                pageNumber, pageSize, sortBy, isAscending);

        public static (List<Animal>, int) GetAnimalsByName(
            string name, int pageNumber, int pageSize, string sortBy, bool isAscending) =>
            GetAnimalsByValue(animal => animal.AnimalName.Contains(name, StringComparison.InvariantCultureIgnoreCase),
                pageNumber, pageSize, sortBy, isAscending);

        public static (List<Animal>, int) GetAnimalsByDefault(
            int pageNumber, int pageSize, string sortBy, bool isAscending) =>
            GetAnimalsByValue(animal => { return true; }, pageNumber, pageSize, sortBy, isAscending);
    }

    static class SortingExtension
    {
        public static IEnumerable<Animal> SortBy(this IEnumerable<Animal> animals, string sortBy, bool isAscending)
        {
            var sortingFields = new Dictionary<string, Func<IEnumerable<Animal>, bool, IOrderedEnumerable<Animal>>>(StringComparer.InvariantCultureIgnoreCase)
            {
                [nameof(Animal.RegistrationNumber)] = (animals, isAscending) =>
                    isAscending ? animals.OrderBy(a => a.RegistrationNumber)
                    : animals.OrderByDescending(a => a.RegistrationNumber),

                [nameof(Animal.Locality)] = (animals, isAscending) =>
                    isAscending ? animals.OrderBy(a => a.Locality)
                    : animals.OrderByDescending(a => a.Locality),

                [nameof(Animal.AnimalCategory)] = (animals, isAscending) =>
                    isAscending ? animals.OrderBy(a => a.AnimalCategory.NameAnimalCategory)
                    : animals.OrderByDescending(a => a.AnimalCategory.NameAnimalCategory),

                [nameof(Animal.Gender)] = (animals, isAscending) =>
                    isAscending ? animals.OrderBy(a => a.Gender.NameGender)
                    : animals.OrderByDescending(a => a.Gender.NameGender),

                [nameof(Animal.YearOfBirth)] = (animals, isAscending) =>
                    isAscending ? animals.OrderBy(a => a.YearOfBirth)
                    : animals.OrderByDescending(a => a.YearOfBirth),

                [nameof(Animal.ElectronicChipNumber)] = (animals, isAscending) =>
                    isAscending ? animals.OrderBy(a => a.ElectronicChipNumber)
                    : animals.OrderByDescending(a => a.ElectronicChipNumber),

                [nameof(Animal.AnimalName)] = (animals, isAscending) =>
                    isAscending ? animals.OrderBy(a => a.AnimalName)
                    : animals.OrderByDescending(a => a.AnimalName)
            };

            var sortingMethod = sortingFields[sortBy];

            return sortingMethod(animals, isAscending);
        }
    }
}

