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
            _db.Profiles.Add(item);
            _db.SaveChanges();
        }

        public void Delete(StudentProfile item)
        {
            _db.Profiles.Remove(item);
            _db.SaveChanges();
        }

        public void DeleteRange(IEnumerable<StudentProfile> items)
        {
            _db.Profiles.RemoveRange(items);
            _db.SaveChanges();
        }

        public StudentProfile Get(int id)
        {
            return _db.Profiles.Find(id);

        }

        public IEnumerable<StudentProfile> GetAll()
        {

            return _db.Profiles;
        }



        public void Edit(StudentProfile item)
        {
            var originalItem = Get(item.Id);
            originalItem.Class = item.Class;
            originalItem.Name = item.Name;
            originalItem.DOB = item.DOB;
            originalItem.EnrolmentDate = item.EnrolmentDate;
            originalItem.Parent1 = item.Parent1;
            originalItem.Parent2 = item.Parent2;
            originalItem.Parent1PN = item.Parent1PN;
            originalItem.Parent2PN = item.Parent2PN;
            originalItem.Address = item.Address;



            _db.SaveChanges();
        }
    }
}

