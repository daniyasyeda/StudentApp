using Microsoft.EntityFrameworkCore;
using StudentApp.Models;

namespace StudentApp.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<LogIn> Logins { get; set; }
        public DbSet<Student> Students { get; set; }


        public DbSet<StudentProfile> Profiles { get; set; }
        public DbSet<Subjects> Subjects { get; set; }


    }
}
