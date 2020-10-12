namespace ToDoList.Models
{
  public class EnrollmentItem
  {
    public int EnrollmentItemId { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public Student Student { get; set; }
    public Course Course { get; set; }
  }
}