using StudentApp.Contexts;
using StudentApp.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace StudentApp.Repositories
{

    public class SubjectRepository :  IRepository<Subjects>
    {
        private readonly ApplicationDbContext _db;

        public SubjectRepository(ApplicationDbContext db) 
        {
            _db = db;
        }
        public void Add(Subjects item)
        {
            _db.Subjects.Add(item);
            _db.SaveChanges();
        }

        public void Delete(Subjects item)
        {
            _db.Subjects.Remove(item);
            _db.SaveChanges();
        }

        public void DeleteRange(IEnumerable<Subjects> items)
        {
            _db.Subjects.RemoveRange(items);
            _db.SaveChanges();
        }

        public Subjects Get(int id)
        {
            return _db.Subjects.Find(id);

        }

        public IEnumerable<Subjects> GetAll()
        {

            return _db.Subjects;
        }



        public void Edit(Subjects item)
        {
            var originalItem = Get(item.Id);
            originalItem.MathMarks = item.MathMarks;
            originalItem.EnglishMarks = item.EnglishMarks;
            originalItem.TotalMathMarks = item.TotalMathMarks;
            originalItem.TotalEnglishMarks = item.TotalEnglishMarks;
            originalItem.PassEnglishMarks = item.PassEnglishMarks;
            originalItem.PassMathMarks = item.PassMathMarks;
        



            _db.SaveChanges();
        }
    }
}
