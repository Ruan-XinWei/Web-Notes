using FirstAspNet.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FirstAspNet.Controllers
{
    public class TestController : Controller
    {
        static List<Student> list = new List<Student>();
        // GET: Test
        [HttpPost]
        public ActionResult Create(Student s)
        {
            list.Add(s); //集合添加对象
            /*ViewBag.Count = list.Count; // 获取集合元素个数*/
            /*return View("Add");*/
            /*return RedirectToAction("List");*/
            return RedirectToAction("List");
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            ViewBag.Message = "要查询的ID=" + id;
            Student stu = list.Find(s => s.id == id);
            if (stu != null)
                return View(stu);
            else
                return View("NoFound");
        }

        public ActionResult List()
        {
            return View(list);
        }
    }
}
