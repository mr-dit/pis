using System;
using System.Numerics;
using pis.Repositorys;
using pis_web_api.Models.db;
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
        
        // Для карточки животного
        public (List<Vaccination>, int) GetVaccinationsByAnimal
            (int animalId, int pageNumber, int pageSize) =>
             _vaccinationRepository.GetVaccinationsByAnimal(animalId, pageNumber, pageSize);

        // Для отчета
        public List<Vaccination> GetVaccinationsByDate (DateOnly dateStart, DateOnly dateEnd) =>
            _vaccinationRepository.GetVaccinationsByDate(dateStart, dateEnd);
    }
}