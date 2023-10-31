using System;
using System.Numerics;
using pis.Models;
using pis.Repositorys;
using pis_web_api.Services;

namespace pis.Services
{
    public class VaccinationService : Service<Vaccination>
    {
        private VaccinationRepository _vaccinationRepository;

        public VaccinationService() 
        {
            _vaccinationRepository = new VaccinationRepository();
            _repository = _vaccinationRepository;
        }

        public (List<Vaccination>, int) GetVaccinations
            (string filterField, string filterValue, DateOnly dateStart, DateOnly dateEnd,
            string sortBy, bool isAscending, int pageNumber, int pageSize)
        {
            switch (filterField)
            {
                case (nameof(Vaccination.Animal)):
                    return _vaccinationRepository.GetVaccinationsByAnimal
                        (filterValue, pageNumber, pageSize, sortBy, isAscending);

                case (nameof(Vaccination.VaccinationDate)):
                    return _vaccinationRepository.GetVaccinationsByDate
                        (dateStart, dateEnd, pageNumber, pageSize, sortBy, isAscending);
                default:
                    return _vaccinationRepository.GetVaccinationsByDefault
                        (pageNumber, pageSize, sortBy, isAscending);
            }
        }
    }
}