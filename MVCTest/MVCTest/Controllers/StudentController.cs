using MVCTest.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVCTest.Controllers
{
    public class StudentController : Controller
    {
        static List<student> list = new List<student>();
        // GET: Student
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(student s)
        {
            list.Add(s);
            /*ViewBag.Count = list.Count;*/
            /*return View("Add");*/
            return RedirectToAction("List");
        }

        public ActionResult Detail(int id)
        {
            ViewBag.message = "你要查询的学号" + id;
            student stu = list.Find(stud => stud.student_id == id);
            if (stu == null)
            {
                return View("Error");
            }
            return View(stu);
        }
        public ActionResult List()
        {
            return View(list);
        }
    }
}
