using System.Collections.Generic;

namespace University.Models
{
  public class Student
  {
    public Student()
    {
      this.Items = new HashSet<Enrollment>();
    }

    public int StudentId { get; set; }
    public string StudentName { get; set; }
    public string DateofEnroll { get; set; }
    public virtual ICollection<Enrollment> Courses { get; set; }
  }
}