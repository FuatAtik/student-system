using Kusys.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Kusys.Data.Concrete.EntityFramework;

public class KusysContext:DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=DESKTOP-MPC9JL0;Database=Kusys;Trusted_Connection=true");
    }

    public DbSet<User> User { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<Student>Student { get; set; }
    public DbSet<Course> Course { get; set; }
    public DbSet<CourseCategory> CourseCategory { get; set; }
    public DbSet<StudentCourseMapping> StudentCourseMapping { get; set; }

}