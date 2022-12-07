using StudentApp.Models;

namespace StudentApp.Repositories
{
    public interface IUnitOfWork
    {
        public IRepository<LogIn> Logins { get; set; }
        public IRepository<Student> Students { get; set; }

        public IRepository<StudentProfile> Profiles { get; set; }

        public IRepository<Subjects> Subjects { get; set; }

        int Complete();
    }
}
