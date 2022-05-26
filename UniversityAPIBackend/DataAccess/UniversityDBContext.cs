using Microsoft.EntityFrameworkCore;
using UniversityAPIBackend.Models.DataModels;

namespace UniversityAPIBackend.DataAccess
{
    public class UniversityDBContext: DbContext
    {

        public UniversityDBContext(DbContextOptions<UniversityDBContext> options): base(options)
        {

        }

        // Add DbSets
        public DbSet<User>? Users { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<Category>? Category { get; set; }
        public DbSet<Chapter>? Chapters { get; set; }
        public DbSet<Student>? Students { get; set; }
        

    }
}
