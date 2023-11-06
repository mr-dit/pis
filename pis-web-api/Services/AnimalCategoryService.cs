using pis.Repositorys;
using pis_web_api.Models.db;

namespace pis_web_api.Services
{
    public class AnimalCategoryService : Service<AnimalCategory>
    {
        private AnimalCategoryRepository _animalCategoryRepository;

        public AnimalCategoryService()
        {
            _animalCategoryRepository = new AnimalCategoryRepository();
            _repository = _animalCategoryRepository;
        }

        public List<AnimalCategory> GetAnimalsCategories()
        {
            return _animalCategoryRepository.GetAnimalCategories();
        }
    }
}
