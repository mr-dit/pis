namespace pis_web_api.Repositorys
{
    public interface IRepository<T> where T : class
    {
        public bool Add(T entity);
        public bool Remove(T entity);
        public bool Update(T entity);
        public T GetById(int id);
    }
}
