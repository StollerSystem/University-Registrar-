using Microsoft.AspNetCore.Mvc;
using University.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace University.Controllers
{
  public class DepartmentsController : Controller
  {
    private readonly UniversityContext _db;

    public DepartmentsController(UniversityContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Department.ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Department department)
    {
      _db.Department.Add(department);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Details(int id)
    {
      Department thisDepartment = _db.Department
      .Include(department => department.Courses)
      .Include(department => department.Students)
      .ThenInclude(student => student.Courses)
      // .Include(department => department.Complete)
      .FirstOrDefault(department => department.DepartmentId == id);
      return View(thisDepartment);
    }

    public ActionResult Edit(int id)
    {
      var thisDepartment = _db.Department.FirstOrDefault(departments => departments.DepartmentId == id);
      return View(thisDepartment);
    }

    [HttpPost]
    public ActionResult Edit(Department department)
    {
      _db.Entry(department).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Delete(int id)
    {
      var thisDepartment = _db.Department.FirstOrDefault(departments => departments.DepartmentId == id);
      return View(thisDepartment);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisDepartment = _db.Department.FirstOrDefault(departments => departments.DepartmentId == id);
      _db.Department.Remove(thisDepartment);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}