using Microsoft.AspNetCore.Mvc;
using University.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace University.Controllers
{
  public class StudentsController : Controller
  {
    private readonly UniversityContext _db;

    public StudentsController(UniversityContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Student> model = _db.Students.Include(student => student.Department).ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.DepartmentId = new SelectList(_db.Department, "DepartmentId", "DepartmentName");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Student student, int StudentId)
    {
      _db.Students.Add(student);
      // if (StudentId != 0)
      // {
      //   _db.Enrollment.Add(new Enrollment() { StudentId = StudentId, CourseId = course.CourseId });
      // }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      // System.Console.WriteLine("STUDENT ID: {0}", id);
      var thisStudent = _db.Students
          .Include(student => student.Courses)
          .ThenInclude(join => join.Course)
          .FirstOrDefault(student => student.StudentId == id);

      ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "CourseName");

      return View(thisStudent);
    }

    [HttpPost]
    public ActionResult Details(Student student, int CourseId) //ADD cOURSE
    {
      if (CourseId != 0)
      {
        _db.Enrollment.Add(new Enrollment() { CourseId = CourseId, StudentId = student.StudentId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details");
    }

    public ActionResult Edit(int id)
    {
      var thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
      ViewBag.DepartmentId = new SelectList(_db.Department, "DepartmentId", "DepartmentName");
      return View(thisStudent);
    }

    [HttpPost]
    public ActionResult Edit(Student student)
    {
      _db.Entry(student).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
      return View(thisStudent);
    }
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
      _db.Students.Remove(thisStudent);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    [HttpPost]
    public ActionResult DeleteCourse(int joinId, int studentId)
    {
      // System.Console.WriteLine(" Delete Course STUDENT ID: {0}", studentId);
      var joinEntry = _db.Enrollment.FirstOrDefault(entry => entry.EnrollmentId == joinId);
      _db.Enrollment.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = studentId });
    }

    [HttpPost]
    public ActionResult CompleteCourse(int joinId, int studentId)
    {
      // System.Console.WriteLine(" Delete Course STUDENT ID: {0}", studentId);
      var joinEntry = _db.Enrollment.FirstOrDefault(entry => entry.EnrollmentId == joinId);
      joinEntry.Complete = true;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = studentId });
    }
  }
}
// new { id = studentId } anon object to match the needs of details. 