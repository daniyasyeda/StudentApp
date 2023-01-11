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
        public DbSet<StudentGrade> StudentGrades { get; set; }


        public DbSet<StudentProfile> StudentProfiles { get; set; }
        public DbSet<RollCall> RollCalls { get; set; }


    }
}
