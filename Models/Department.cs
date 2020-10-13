using System.Collections.Generic;

namespace University.Models
{

  public class Department
  {
    public Department()
    {
      this.Students = new HashSet<Student>();
      this.Courses = new HashSet<Course>();
      // this.Complete = new HashSet<Enrollment>();
    }

    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    // public virtual Enrollment Enrollment { get; set; }

    public virtual ICollection<Student> Students { get; set; }
    public virtual ICollection<Course> Courses { get; set; }
    // public virtual ICollection<Enrollment> Complete { get; set; }



  }
}