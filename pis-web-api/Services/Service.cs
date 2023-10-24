using pis.Models;
using pis.Repositorys;
using pis_web_api.Repositorys;

namespace pis_web_api.Services
{
    public class Service<T> where T : class
    {
        protected Repository<T> _repository;
        public Service() 
        {
            _repository = new Repository<T>();
        }

        public bool AddEntry(T value)
        {
            bool status = _repository.Add(value);
            return status;
        }

        public bool DeleteEntry(int id)
        {
            bool status = _repository.Remove(_repository.GetById(id));
            return status;
        }

        public T GetEntry(int id)
        {
            var entry = _repository.GetById(id);
            return entry;
        }

        public bool ChangeEntry(T value)
        {
            bool status = _repository.Update(value);
            return status;
        }
    }
}
