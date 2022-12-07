namespace StudentApp.Repositories
{    
        public interface IRepository<T>
        {
            IEnumerable<T> GetAll();
            T Get(int id);
            void Add(T item);
            void Edit(T item);
            void Delete(T item);
            void DeleteRange(IEnumerable<T> items);
        }
        
}
