namespace StudentApp.Repositories
{    
        public interface IRepository<T>
        {
        IEnumerable<T> GetAll();

        T Get(int id);

        void Add(T entity);

        void Delete(T entity);

             
        void Edit(T item);

        void DeleteRange(IEnumerable<T> items);
    }
        
}
