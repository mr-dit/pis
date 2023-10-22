using pis.Models;
using pis.Repositorys;

namespace pis_web_api.Services
{
    public class AnimalCategoryService
    {
        private AnimalCategoryRepository _animalCategoryRepository;

        public AnimalCategoryService()
        {
            _animalCategoryRepository = new AnimalCategoryRepository();
        }

        public bool FillData(AnimalCategory category)
        {
            bool status = _animalCategoryRepository.Add(category);
            return status;
        }

        public bool DeleteEntry(int id)
        {
            bool status = _animalCategoryRepository.Remove(_animalCategoryRepository.GetById(id));
            return status;
        }

        public AnimalCategory GetEntry(int id)
        {
            var entry = _animalCategoryRepository.GetById(id);
            return entry;
        }

        public List<AnimalCategory> GetAnimalsCategories()
        {
            return _animalCategoryRepository.GetAnimalCategories();
        }


        public bool ChangeEntry(AnimalCategory category)
        {
            bool status = _animalCategoryRepository.Update(category);
            return status;
        }
    }
}
