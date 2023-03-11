using StudentApp.Contexts;
using StudentApp.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace StudentApp.Repositories
{    

    public class StudentProfileRepository : IRepository<StudentProfile>
    {
        private readonly ApplicationDbContext _db;

        public StudentProfileRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Add(StudentProfile item)
        {
            _db.StudentProfiles.Add(item); 
            _db.SaveChanges();
        }

        public void Delete(StudentProfile item)
        {
            _db.StudentProfiles.Remove(item);
            _db.SaveChanges();
        }

        public void DeleteRange(IEnumerable<StudentProfile> items)
        {
            _db.StudentProfiles.RemoveRange(items);
            _db.SaveChanges();
        }

        public StudentProfile Get(int id)
        {
            return _db.StudentProfiles.Find(id);

        }

        public IEnumerable<StudentProfile> GetAll()
        {

            return _db.StudentProfiles;
        }
        public void Edit(StudentProfile item)
        {
            var originalItem = Get(item.Id);
            originalItem.Class = item.Class;
            originalItem.Name = item.Name;
            originalItem.DOB = item.DOB.ToUniversalTime();            
            originalItem.EnrolmentDate = item.EnrolmentDate.ToUniversalTime();            
            originalItem.Parent1 = item.Parent1;
            originalItem.Parent2 = item.Parent2;
            originalItem.Parent1Phone = item.Parent1Phone;
            originalItem.Parent2Phone = item.Parent2Phone;
            originalItem.Address = item.Address;

            _db.SaveChanges();
        }

        public StudentProfile Get(string value1, string value2)
        {
            throw new NotImplementedException();
        }
    }
}

