using StudentApp.Contexts;
using StudentApp.Models;
namespace StudentApp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Students = new StudentRepository(db);
            Logins = new LoginRepository(db);
            Subjects = new SubjectRepository(db);
           Profiles = new StudentProfileRepository(db);
            
        }

        public IRepository<Student> Students { get; set; }

        public IRepository<LogIn> Logins { get; set; }

        public IRepository<StudentProfile> Profiles { get; set; }
        public IRepository<Subjects> Subjects{ get; set; }

        public int Complete()
        {
            return _db.SaveChanges();
        }

    }
}
