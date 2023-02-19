using Microsoft.AspNetCore.Mvc;
using StudentApp.Contexts;
using StudentApp.Models;
using StudentApp.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace StudentApp.Repositories
{

    public class LoginRepository : IRepository<LogIn>
    {
        private readonly ApplicationDbContext _db;

        public LoginRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(LogIn item)
        {
            _db.Logins.Add(item);
            _db.SaveChanges();
        }

        public void Delete(LogIn item)
        {
            _db.Logins.Remove(item);
            _db.SaveChanges();
        }

        public void DeleteRange(IEnumerable<LogIn> items)
        {
            _db.Logins.RemoveRange(items);
            _db.SaveChanges();
        }

        public LogIn Get(int id)
        {
            return _db.Logins.Find(id);
        }

        public IEnumerable<LogIn> GetAll()
        {
            return _db.Logins;
        }

        public void Edit(LogIn item)
        {
            var originalItem = Get(item.Id);
            originalItem.Password = item.Password;
         
            originalItem.Name = item.Name;
            originalItem.CreatedDate = item.CreatedDate;
            _db.SaveChanges();
        }

        public LogIn Get(string value1, string value2)
        {
            throw new NotImplementedException();
        }
    }
}

