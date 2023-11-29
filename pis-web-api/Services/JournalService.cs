using pis.Repositorys;
using pis.Services;
using pis_web_api.Models.db;
using pis_web_api.Models.get;
using pis_web_api.Repositorys;

namespace pis_web_api.Services
{
    public class JournalService
    {
        private Repository<Journal> _repository;
        private AnimalService _animalService;

        public JournalService() 
        {
            _repository = new Repository<Journal>();
            _animalService = new AnimalService();
        }

        public void JournalAddAnimal(int userId, int animalId)
        {
            Animal animal = _animalService.GetEntry(animalId);
            if (animal == null)
            {
                throw new Exception("Данного животного не существует");
            }
            string desctription = "";
            desctription += animal.RegistrationNumber + ";";
            desctription += animal.AnimalName + ";";
            desctription += animal.YearOfBirth + ";";
            desctription += animal.AnimalCategory.NameAnimalCategory + ";";
            desctription += animal.Gender.NameGender + ";";
            Journal journal = new Journal(userId, animalId, desctription, TableNames.Животные, JournalActionType.Add);
            _repository.Add(journal);
        }
    }
}
