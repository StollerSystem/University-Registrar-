using Microsoft.EntityFrameworkCore;

namespace University.Models
{
  public class UniversityContext : DbContext
  {
    public virtual DbSet<Student> Students { get; set; }
    public virtual DbSet<Course> Courses { get; set; }
    public virtual DbSet<Enrollment> Enrollment { get; set; }
    public virtual DbSet<Department> Department { get; set; }

    public UniversityContext(DbContextOptions options) : base(options) { }
  }
}