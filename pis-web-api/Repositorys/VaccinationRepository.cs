using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using Microsoft.EntityFrameworkCore;
using pis.Models;
using pis_web_api.Repositorys;

namespace pis.Repositorys
{
    public class VaccinationRepository : Repository<Vaccination>
    {
        public VaccinationRepository()  :base()
        { }

        private (List<Vaccination>, int) GetVaccinationsByValue(Func<Vaccination, bool> value, int pageNumber, int pageSize, string sortBy, bool isAscending)
        {
            using (Context db = new Context())
            {
                var allVaccinations = db.Vaccinations
                    .Include(x => x.Vaccine)
                    .Include(x => x.Animal)
                    .Include(x => x.Contract)
                    .Include(x => x.Doctor)
                    .Where(value)
                    .SortBy(sortBy, isAscending);
                var vaccinations = allVaccinations.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                return (vaccinations, allVaccinations.Count());
            }
        }

        public (List<Vaccination>, int) GetVaccinationsByAnimal
            (string animalId, int pageNumber, int pageSize, string sortBy, bool isAscending)
        => GetVaccinationsByValue(x => x.Animal.RegistrationNumber == int.Parse(animalId), 
            pageNumber, pageSize, sortBy, isAscending);

        public (List<Vaccination>, int) GetVaccinationsByDate
            (DateOnly dateStart, DateOnly dateEnd, int pageNumber,
            int pageSize, string sortBy, bool isAscending) =>
            GetVaccinationsByValue(x => x.VaccinationDate >= dateStart && x.VaccinationDate <= dateEnd,
                pageNumber, pageSize, sortBy, isAscending);

        public (List<Vaccination>, int) GetVaccinationsByDefault
            (int pageNumber, int pageSize, string sortBy, bool isAscending) =>
            GetVaccinationsByValue(x => { return true; }, pageNumber, pageSize, sortBy, isAscending);
    }

    static class VaccinationExtension
    {
        public static IEnumerable<Vaccination> SortBy(this IEnumerable<Vaccination> users, string sortBy, bool isAscending)
        {
            var sortingFields = new Dictionary<string, Func<IEnumerable<Vaccination>, bool, IOrderedEnumerable<Vaccination>>>(StringComparer.InvariantCultureIgnoreCase)
            {
                [nameof(Vaccination.Contract)] = (users, isAscending) =>
                    isAscending ? users.OrderBy(a => a.Contract.IdContract)
                    : users.OrderByDescending(a => a.Contract.IdContract),
                [nameof(Vaccination.VaccinationDate)] = (users, isAscending) =>
                    isAscending ? users.OrderBy(x => x.VaccinationDate) 
                    : users.OrderByDescending (x => x.VaccinationDate)
            };

            var sortingMethod = sortingFields[sortBy];

            return sortingMethod(users, isAscending);
        }
    }
}