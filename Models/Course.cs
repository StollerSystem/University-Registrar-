using System.Collections.Generic;

namespace University.Models
{
  public class Course
  {
    public Course()
    {
      this.Students = new HashSet<Enrollment>();
    }

    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public string CourseNumber { get; set; }
    public int DepartmentId { get; set; }
    public virtual Department Department { get; set; }
    public virtual ICollection<Enrollment> Students { get; set; }

  }
}

