using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using pis_web_api.Models.db;
using pis_web_api.Models.post;
using pis_web_api.Repositorys;

namespace pis.Repositorys
{
    public class AnimalRepository : Repository<Animal>
    {
        public AnimalRepository() : base()
        {
        }

        private delegate void AnimalAction(Context db, Animal animal);

        private (List<Animal>, int) GetAnimalsByValue(Func<Animal, bool> value, int pageNumber, int pageSize, string sortBy, bool isAscending)
        {
            using (Context db = new Context())
            {
                var allAnimals = db.Animals
                    .Include(x => x.Locality)
                    .Include(x => x.AnimalCategory)
                    .Include(x => x.Gender)
                    .Include(x => x.Vaccinations)
                    .Where(value)
                    .SortBy(sortBy, isAscending);
                var animals = allAnimals.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                return (animals, allAnimals.Count());
            }
        }

        private (List<Animal>, int) GetAnimalsByValue(Func<Animal, bool> value, int pageNumber, int pageSize, 
            string sortBy, bool isAscending, UserPost user)
        {
            var animalIds = db.Vaccinations
                .Where(x => x.Contract.PerformerId == user.OrganisationId)
                .Where(x => x.Contract.CustomerId == user.OrganisationId)
                .Select(x => x.AnimalId);

            using (Context db = new Context())
            {
                var allAnimals = db.Animals
                    .Where(x => animalIds.Contains(x.RegistrationNumber))
                    .Include(x => x.Locality)
                    .Include(x => x.AnimalCategory)
                    .Include(x => x.Gender)
                    .Include(x => x.Vaccinations)
                    .Where(value)
                    .SortBy(sortBy, isAscending);
                var animals = allAnimals.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                return (animals, allAnimals.Count());
            }
        }

        public (List<Animal>, int) GetAnimalsByAnimalCategory(
            string category, int pageNumber,
            int pageSize, string sortBy, bool isAscending) =>
            GetAnimalsByValue(animal => animal.AnimalCategory
                                               .NameAnimalCategory
                                               .Contains(category, StringComparison.InvariantCultureIgnoreCase),
                pageNumber, pageSize, sortBy, isAscending);

        public (List<Animal>, int) GetAnimalsByAnimalCategory(
            string category, int pageNumber,
            int pageSize, string sortBy, bool isAscending, UserPost user) =>
            GetAnimalsByValue(animal => animal.AnimalCategory
                                               .NameAnimalCategory
                                               .Contains(category, StringComparison.InvariantCultureIgnoreCase),
                pageNumber, pageSize, sortBy, isAscending, user);

        public (List<Animal>, int) GetAnimalsByChipNumber(
            string chipNumber, int pageNumber, int pageSize, string sortBy, bool isAscending) =>
            GetAnimalsByValue(animal => animal.ElectronicChipNumber
                                              .Contains(chipNumber, StringComparison.InvariantCultureIgnoreCase),
                pageNumber, pageSize, sortBy, isAscending);

        public (List<Animal>, int) GetAnimalsByChipNumber(
            string chipNumber, int pageNumber, int pageSize, string sortBy, bool isAscending, UserPost user) =>
            GetAnimalsByValue(animal => animal.ElectronicChipNumber
                                              .Contains(chipNumber, StringComparison.InvariantCultureIgnoreCase),
                pageNumber, pageSize, sortBy, isAscending, user);

        public (List<Animal>, int) GetAnimalsByName(
            string name, int pageNumber, int pageSize, string sortBy, bool isAscending) =>
            GetAnimalsByValue(animal => animal.AnimalName
                                                .Contains(name, StringComparison.InvariantCultureIgnoreCase),
                pageNumber, pageSize, sortBy, isAscending);

        public (List<Animal>, int) GetAnimalsByName(
            string name, int pageNumber, int pageSize, string sortBy, bool isAscending, UserPost user) =>
            GetAnimalsByValue(animal => animal.AnimalName
                                                .Contains(name, StringComparison.InvariantCultureIgnoreCase),
                pageNumber, pageSize, sortBy, isAscending, user);

        public (List<Animal>, int) GetAnimalsByDefault(
            string uselessValue, int pageNumber, int pageSize, string sortBy, bool isAscending) =>
            GetAnimalsByValue(animal => { return true; }, pageNumber, pageSize, sortBy, isAscending);

        public (List<Animal>, int) GetAnimalsByDefault(
            string uselessValue, int pageNumber, int pageSize, string sortBy, bool isAscending, UserPost user) =>
            GetAnimalsByValue(animal => { return true; }, pageNumber, pageSize, sortBy, isAscending, user);

        public (List<Animal>, int) GetAnimalsByLocality(
            string locality, int pageNumber, int pageSize, string sortBy, bool isAscending) =>
            GetAnimalsByValue(animal => animal.Locality
                                              .NameLocality
                                              .Contains(locality, StringComparison.InvariantCultureIgnoreCase),
                pageNumber, pageSize, sortBy, isAscending);

        public (List<Animal>, int) GetAnimalsByLocality(
            string locality, int pageNumber, int pageSize, string sortBy, bool isAscending, UserPost user) =>
            GetAnimalsByValue(animal => animal.Locality
                                              .NameLocality
                                              .Contains(locality, StringComparison.InvariantCultureIgnoreCase),
                pageNumber, pageSize, sortBy, isAscending, user);

        public override Animal GetById(int id)
        {
            using (var db = new Context())
            {
                var animal = db.Animals
                    .Where(x => x.RegistrationNumber == id)
                    .Include(x => x.Gender)
                    .Include(x => x.AnimalCategory)
                    .Include(x => x.Locality)
                    .Include(x => x.Vaccinations)
                    .Single();
                return animal;
            }
        }
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
                    isAscending ? animals.OrderBy(a => a.Locality.NameLocality)
                    : animals.OrderByDescending(a => a.Locality.NameLocality),

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

